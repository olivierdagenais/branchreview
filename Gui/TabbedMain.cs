using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NDesk.Options;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;
using SoftwareNinjas.BranchAndReviewTools.Gui.Components;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.BranchAndReviewTools.Gui.WeifenLuo.WinFormsUI.Docking;
using SoftwareNinjas.Core;
using EnumExtensions = SoftwareNinjas.BranchAndReviewTools.Gui.Extensions.EnumExtensions;
using Resources = SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Resources;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class TabbedMain : Form, ILog
    {
        private const int StatusMessageCapacity = 512;
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;
        private readonly IBuildRepository _buildRepository;
        private readonly LinkedList<StatusMessage> _statusMessages = new LinkedList<StatusMessage>();
        private readonly Throttler _statusThrottle;
        private readonly Timer _statusCleaner = new Timer { Interval = 2000 };
        private readonly MostRecentlyUsedCollection<IDockContent> _activationOrder =
            new MostRecentlyUsedCollection<IDockContent>();
        private IDockContent _toActivate;
        private bool _controlKeyDown = false;
        private bool _handledControlTab = false;

        public TabbedMain()
        {
            InitializeComponent();
            mainPanel.ActiveContentChanged += mainPanel_ActiveContentChanged;
            mainPanel.ContentRemoved += mainPanel_ContentRemoved;
            Load += Main_Load;
            FormClosing += Main_Closing;
            _statusThrottle = new Throttler(this, 100, UpdateStatusBar);
            _statusCleaner.Tick += statusCleaner_Tick;
            var repositoriesFolder = Environment.CurrentDirectory.CombinePath("Repositories");
            if (Directory.Exists(repositoriesFolder))
            {
                var catalog = new DirectoryCatalog(repositoriesFolder);
                var container = new CompositionContainer(catalog);
                _taskRepository = container.GetExportedValueOrDefault<ITaskRepository>();
                _sourceRepository = container.GetExportedValueOrDefault<ISourceRepository>();
                _shelvesetRepository = container.GetExportedValueOrDefault<IShelvesetRepository>();
                _buildRepository = container.GetExportedValueOrDefault<IBuildRepository>();
            }
            #if DEBUG
            _taskRepository = _taskRepository ?? new Core.Mock.TaskRepository();
            _sourceRepository = _sourceRepository ?? new Core.Mock.SourceRepository();
            _shelvesetRepository = _shelvesetRepository ?? new Core.Mock.ShelvesetRepository();
            _buildRepository = _buildRepository ?? new Core.Mock.BuildRepository();
            #endif

            if (_sourceRepository != null)
            {
                _sourceRepository.Log = this;
            }

            if (_taskRepository != null)
            {
                _taskRepository.Log = this;
            }

            if (_shelvesetRepository != null)
            {
                _shelvesetRepository.Log = this;
            }

            if (_buildRepository != null)
            {
                _buildRepository.Log = this;
            }

            RegisterComponents();
            mainPanel.DocumentStyle = DocumentStyle.DockingWindow;
        }

        void mainPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (e.Content != null)
            {
                _activationOrder.Remove(e.Content);
            }
        }

        void mainPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            if (mainPanel.ActiveContent != null)
            {
                _activationOrder.Add(mainPanel.ActiveContent);
            }
        }

        private void RegisterComponents()
        {
            if (_taskRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newTaskBrowser", "&Task Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr, br) => new TaskBrowser(tr, sor, shr, br));
                        }
                    )
                );
            }
            if (_sourceRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newBranchBrowser", "&Branch Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr, br) => new BranchBrowser(tr, sor, shr, br));
                        }
                    )
                );
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newChangeCommitter", "&Change Committer", true, () =>
                        {
                            AddComponent((tr, sor, shr, br) => new ChangeCommitter(tr, sor, shr, br) { Title = "Commit" });
                        }
                    )
                );
            }
            if (_shelvesetRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newShelvesetBrowser", "&Shelveset Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr, br) => new ShelvesetBrowser(tr, sor, shr, br));
                        }
                    )
                );
            }
            if (_buildRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newBuildBrowser", "B&uild Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr, br) => new BuildBrowser(tr, sor, shr, br));
                        }
                    )
                );
            }
        }

        internal void AddComponent(Func<ITaskRepository, ISourceRepository, IShelvesetRepository, IBuildRepository, IHistoryItem> factory)
        {
            IHistoryItem historyItem;
            try
            {
                historyItem = factory(_taskRepository, _sourceRepository, _shelvesetRepository, _buildRepository);
            }
            catch (Exception)
            {
                this.ToDo("We may want to advise the user that the operation could not be completed");
                return;
            }
            var historyWindow = new HistoryWindow(historyItem);
            historyWindow.Show(mainPanel);
        }

        void Main_Load(object sender, EventArgs e)
        {
            Size = this.LoadSetting(() => Size, new Size(800, 600), SizeExtensions.Parse);
            Location = this.LoadSetting(() => Location, Point.Empty, PointExtensions.Parse);
            WindowState = 
                this.LoadSetting(() => WindowState, FormWindowState.Normal, EnumExtensions.Parse<FormWindowState>);

            Start(Environment.GetCommandLineArgs().Skip(1));
        }

        void Main_Closing(object sender, CancelEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Location = RestoreBounds.Location;
                this.Size = RestoreBounds.Size;
            }
            this.SaveSetting(() => WindowState);
            this.SaveSetting(() => Location);
            this.SaveSetting(() => Size);
        }

        void Main_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            IDockContent dockContent = null;
            _handledControlTab = false;

            var keyData = keyEventArgs.KeyData;
            if (keyData == (Keys.Control | Keys.PageUp))
            {
                dockContent = mainPanel.GetPreviousDocument() ?? mainPanel.GetLastDocument();
            }
            else if (keyData == (Keys.Control | Keys.PageDown) || keyData == (Keys.Control | Keys.F6))
            {
                dockContent = mainPanel.GetNextDocument() ?? mainPanel.GetFirstDocument();
            }
            else if (keyData == (Keys.ControlKey | Keys.Control))
            {
                _controlKeyDown = true;
                keyEventArgs.Handled = true;
            }
            else if (_controlKeyDown && keyData == (Keys.Control | Keys.Tab))
            {
                SwitchWindow();
                _handledControlTab = true;
            }
            else if (keyData == (Keys.Control | Keys.F4) || keyData == (Keys.Control | Keys.W))
            {
                mainPanel.ActiveDocument.DockHandler.Close();
            }

            if (dockContent != null)
            {
                dockContent.DockHandler.Activate();
                keyEventArgs.Handled = true;
            }            
        }

        void Main_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            Func<IHistoryContainer, bool> canMove = null;
            Action<IHistoryContainer> move = null;

            var keyData = keyEventArgs.KeyData;
            if (keyData == Keys.BrowserBack || keyData == (Keys.Alt | Keys.Left))
            {
                canMove = ihc => ihc.CanGoBack;
                move = ihc => ihc.GoBack();
            }
            else if (keyData == Keys.BrowserForward || keyData == (Keys.Alt | Keys.Right))
            {
                canMove = ihc => ihc.CanGoForward;
                move = ihc => ihc.GoForward();
            }
            else if (_controlKeyDown && keyData == (Keys.Control | Keys.Tab))
            {
                if (!_handledControlTab)
                {
                    SwitchWindow();
                }
                this.ToDo("Pop window list up and highlight the {0} window", _toActivate);
                keyEventArgs.Handled = true;
            }
            else if (keyData == Keys.ControlKey)
            {
                if (_toActivate != null)
                {
                    _toActivate.DockHandler.Activate();
                    keyEventArgs.Handled = true;
                    _toActivate = null;
                }
                _controlKeyDown = false;
            }

            if (canMove != null && move != null)
            {
                var pane = mainPanel.ActivePane;
                var historyWindow = (HistoryWindow) pane.ActiveContent;
                var container = historyWindow.HistoryContainer;

                if (container != null)
                {
                    if (canMove(container))
                    {
                        move(container);
                    }
                }
                keyEventArgs.Handled = true;
            }
            _handledControlTab = false;
        }

        private void SwitchWindow()
        {
            if (_toActivate == null)
            {
                _toActivate = _activationOrder.Penultimate;
            }
            else
            {
                var e = _activationOrder.GetEnumerator();
                var useCurrent = false;
                var usedCurrent = false;
                while (e.MoveNext())
                {
                    if (useCurrent)
                    {
                        _toActivate = e.Current;
                        usedCurrent = true;
                        break;
                    }
                    if (e.Current == _toActivate)
                    {
                        useCurrent = true;
                    }
                }
                if (!usedCurrent /* There was nothing after _toActivate, let's pick the first one */)
                {
                    e.Reset();
                    e.MoveNext();
                    _toActivate = e.Current;
                }
            }
        }

        public void Start(IEnumerable<string> arguments)
        {
            var p = new OptionSet
            {
                {"BrowseTasks", "Opens a task browser", v =>
                    {
                        AddComponent((tr, sor, shr, br) => new TaskBrowser(tr, sor, shr, br));
                    }
                },
                /*
                {"InspectTask={}", "Opens a task inspector with the specified task ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) =>
                        {
                            var result = new TaskInspector(tr, sor, shr, br)
                            {
                                TaskId = v,
                            };
                            return result;
                        });
                    }
                },
                 */
                {"BrowseBranches", "Opens a branch browser", v =>
                    {
                        AddComponent((tr, sor, shr, br) => new BranchBrowser(tr, sor, shr, br));
                    }
                },
                {"BrowseBranchRevisions=", "Opens a branch revision browser with the specified branch ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) => 
                        {
                            var result = new RevisionBrowser(tr, sor, shr, br)
                            {
                                BranchId = v,
                                Title = v,
                            };
                            return result;
                        });
                    }
                },
                {"InspectRevision=", "Opens a revision inspector with the specified revision ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) => 
                        {
                            var result = new RevisionInspector(tr, sor, shr, br)
                            {
                                RevisionId = v,
                                Title = v,
                            };
                            return result;
                        });
                    }
                },
                {"Commit=", "Opens a change committer with the specified branch ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) => 
                        {
                            var result = new ChangeCommitter(tr, sor, shr, br)
                            {
                                BranchId = v,
                                Title = v,
                            };
                            return result;
                        });
                    }
                },
                {"BrowseShelvesets", "Opens a shelveset browser", v =>
                    {
                        AddComponent((tr, sor, shr, br) => new ShelvesetBrowser(tr, sor, shr, br));
                    }
                },
                {"InspectShelveset=", "Opens a shelveset inspector with the specified shelveset ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) =>
                        {
                            var result = new ShelvesetInspector(tr, sor, shr, br)
                            {
                                ShelvesetId = v,
                                Title = v,
                            };
                            return result;
                        });
                    }
                },
                {"BrowseBuilds", "Opens a build browser", v =>
                    {
                        AddComponent((tr, sor, shr, br) => new BuildBrowser(tr, sor, shr, br));
                    }
                },
                {"InspectBuild=", "Opens a build inspector with the specified build ID", v =>
                    {
                        AddComponent((tr, sor, shr, br) =>
                        {
                            var result = new BuildInspector(tr, sor, shr, br)
                            {
                                BuildId = v,
                                Title = v,
                            };
                            return result;
                        });
                    }
                },
            };
            var extra = p.Parse(arguments);
            if (extra.Count > 0)
            {
                using (var sw = new StringWriter())
                {
                    p.WriteOptionDescriptions(sw);
                    MessageBox.Show(
                        this,
                        sw.ToString(),
                        "BART command-line parameters",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        #region Menu items

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            var historyWindow = mainPanel.ActiveDocument as HistoryWindow;
            if (historyWindow != null)
            {
                var historyContainer = historyWindow.HistoryContainer;
                var currentHistoryItem = historyContainer.Current;
                currentHistoryItem.Reload();
            }
        }

        #endregion

        #region Common

        private void searchMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send(/* Ctrl+F */ "^f");
        }

        #endregion

        #region Implementation of ILog

        public void Info(string message)
        {
            Enqueue(StatusKind.Info, message);
        }

        public void Info(string message, int progressValue, int progressMaximum)
        {
            Enqueue(StatusKind.Info, message, progressValue, progressMaximum);
        }

        public void Warning(string message)
        {
            Enqueue(StatusKind.Warning, message);
        }

        public void Error(string message)
        {
            Enqueue(StatusKind.Error, message);
        }

        private void Enqueue(StatusKind statusKind, string message)
        {
            Enqueue(new StatusMessage(statusKind, message));
        }

        private void Enqueue(StatusKind statusKind, string message, int progressValue, int progressMaximumValue)
        {
            Enqueue(new StatusMessage(statusKind, message, progressValue, progressMaximumValue));
        }

        private void Enqueue(StatusMessage statusMessage)
        {
            lock (_statusMessages)
            {
                _statusMessages.AddLast(statusMessage);
                if (_statusMessages.Count > StatusMessageCapacity)
                {
                    _statusMessages.RemoveFirst();
                }
            }
            _statusThrottle.Fire();
        }

        // TODO: light-up on Windows 7 by displaying progress in the task bar and changing the back colour
        // depending on the log type
        private void UpdateStatusBar()
        {
            _statusCleaner.Stop();
            StatusMessage last;
            lock (_statusMessages)
            {
                last = _statusMessages.Last.Value;
            }
            statusBarText.Text = last.Message;
            switch (last.StatusKind)
            {
                case StatusKind.Info:
                    statusBarText.Image = Resources.dialog_information;
                    if (last.ProgressValue.HasValue && last.ProgressMaximumValue.HasValue)
                    {
                        if (0 == last.ProgressMaximumValue.Value)
                        {
                            statusBarProgress.Style = ProgressBarStyle.Marquee;
                        }
                        else
                        {
                            statusBarProgress.Style = ProgressBarStyle.Continuous;
                            statusBarProgress.Maximum = last.ProgressMaximumValue.Value;
                            statusBarProgress.Value = last.ProgressValue.Value;
                            if (last.ProgressMaximumValue.Value == last.ProgressValue.Value)
                            {
                                // An operation completed entirely, let's schedule a clean-up of the status area...
                                _statusCleaner.Start();
                            }
                        }
                    }
                    break;
                case StatusKind.Warning:
                    statusBarText.Image = Resources.dialog_warning;
                    statusBarProgress.Style = ProgressBarStyle.Continuous;
                    statusBarProgress.Value = 0;
                    break;
                case StatusKind.Error:
                    statusBarText.Image = Resources.dialog_error;
                    statusBarProgress.Style = ProgressBarStyle.Continuous;
                    statusBarProgress.Value = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void statusCleaner_Tick(object sender, EventArgs e)
        {
            _statusCleaner.Stop();
            statusBarText.Text = "Ready";
            statusBarProgress.Value = 0;
        }

        #endregion
    }
}
