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
        private readonly LinkedList<StatusMessage> _statusMessages = new LinkedList<StatusMessage>();
        private readonly Throttler _statusThrottle;
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
            _statusThrottle = new Throttler(100, UpdateStatusBar);
            var repositoriesFolder = Environment.CurrentDirectory.CombinePath("Repositories");
            if (Directory.Exists(repositoriesFolder))
            {
                var catalog = new DirectoryCatalog(repositoriesFolder);
                var container = new CompositionContainer(catalog);
                _taskRepository = container.GetExportedValueOrDefault<ITaskRepository>();
                _sourceRepository = container.GetExportedValueOrDefault<ISourceRepository>();
                _shelvesetRepository = container.GetExportedValueOrDefault<IShelvesetRepository>();
            }
            #if DEBUG
            _taskRepository = _taskRepository ?? new Core.Mock.TaskRepository();
            _sourceRepository = _sourceRepository ?? new Core.Mock.SourceRepository();
            _shelvesetRepository = _shelvesetRepository ?? new Core.Mock.ShelvesetRepository();
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
                            AddComponent((tr, sor, shr) => new TaskBrowser(tr, sor, shr));
                        }
                    )
                );
            }
            if (_sourceRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newBranchBrowser", "&Branch Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr) => new BranchBrowser(tr, sor, shr));
                        }
                    )
                );
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newChangeCommitter", "&Change Committer", true, () =>
                        {
                            AddComponent((tr, sor, shr) => new ChangeCommitter(tr, sor, shr) { Title = "Commit" });
                        }
                    )
                );
            }
            if (_shelvesetRepository != null)
            {
                newMenuItem.MenuItems.AddAction(
                    new MenuAction("newShelvesetBrowser", "&Shelveset Browser", true, () =>
                        {
                            AddComponent((tr, sor, shr) => new ShelvesetBrowser(tr, sor, shr));
                        }
                    )
                );
            }
        }

        private void AddComponent(Func<ITaskRepository, ISourceRepository, IShelvesetRepository, IHistoryItem> factory)
        {
            var historyItem = factory(_taskRepository, _sourceRepository, _shelvesetRepository);
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
            var action = ProgramAction.None;
            var p = new OptionSet
            {
                {"action=", v => action = EnumExtensions.Parse<ProgramAction>(v)},
            };
            var extra = p.Parse(arguments);
            switch (action)
            {
                case ProgramAction.InspectTask:
                    if (_taskRepository != null)
                    {
                        if (extra.Count == 0)
                        {
                            AddComponent((tr, sor, shr) => new TaskBrowser(tr, sor, shr));
                        }
                        else
                        {
                            this.ToDo("Launch a TaskInspector for each of the {0} that are integers", extra.Count);
                        }
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.BrowseBranches:
                    if (_sourceRepository != null)
                    {
                        AddComponent((tr, sor, shr) => new BranchBrowser(tr, sor, shr));
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.BrowseBranchRevisions:
                    if (_sourceRepository != null)
                    {
                        var branchesTable = _sourceRepository.LoadBranches();
                        foreach (var potentialBranchId in extra)
                        {
                            var dataRow = branchesTable.FindFirstOrDefault("ID", potentialBranchId);
                            if (dataRow != null)
                            {
                                var branchId = potentialBranchId;
                                AddComponent((tr, sor, shr) =>
                                {
                                    var result = new RevisionBrowser(tr, sor, shr)
                                    {
                                        BranchId = branchId,
                                        Title = branchId,
                                    };
                                    return result;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.InspectRevision:
                    if (_sourceRepository != null)
                    {
                        foreach (var potentialRevisionId in extra)
                        {
                            Int32 revisionId;
                            if (Int32.TryParse(potentialRevisionId, out revisionId))
                            {
                                AddComponent((tr, sor, shr) =>
                                {
                                    var result = new RevisionInspector(tr, sor, shr)
                                    {
                                        RevisionId = revisionId,
                                        Title = Convert.ToString(revisionId, 10),
                                    };
                                    return result;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.Commit:
                    if (_sourceRepository != null)
                    {
                        AddComponent((tr, sor, shr) => new ChangeCommitter(tr, sor, shr) { Title = "Commit" });
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.BrowseShelvesets:
                    if (_shelvesetRepository != null)
                    {
                        AddComponent((tr, sor, shr) => new ShelvesetBrowser(tr, sor, shr));
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
                case ProgramAction.InspectShelveset:
                    if (_shelvesetRepository != null)
                    {
                        var shelvesetTable = _shelvesetRepository.LoadShelvesets();
                        foreach (var potentialShelvesetId in extra)
                        {
                            var dataRow = shelvesetTable.FindFirstOrDefault("ID", potentialShelvesetId);
                            var shelvesetId = potentialShelvesetId;
                            if (dataRow != null)
                            {
                                AddComponent((tr, sor, shr) =>
                                {
                                    var result = new ShelvesetInspector(tr, sor, shr)
                                    {
                                        ShelvesetId = shelvesetId,
                                        Title = (string) dataRow["Name"],
                                    };
                                    return result;
                                });
                            }
                        }
                    }
                    else
                    {
                        this.ToDo("We may want to advise the user that the operation could not be completed");
                    }
                    break;
            }
        }

        #region Menu items

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            this.ToDo("Push refresh event to current tab");
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
                        }
                    }
                    break;
                case StatusKind.Warning:
                    statusBarText.Image = Resources.dialog_warning;
                    statusBarProgress.Value = 0;
                    break;
                case StatusKind.Error:
                    statusBarText.Image = Resources.dialog_error;
                    statusBarProgress.Value = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
