using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;
using SoftwareNinjas.BranchAndReviewTools.Gui.Components;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.BranchAndReviewTools.Gui.WeifenLuo.WinFormsUI.Docking;
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

        public TabbedMain()
        {
            InitializeComponent();
            mainPanel.ActiveContentChanged += mainPanel_ActiveContentChanged;
            mainPanel.ContentRemoved += new EventHandler<DockContentEventArgs>(mainPanel_ContentRemoved);
            Load += Main_Load;
            FormClosing += Main_Closing;
            _statusThrottle = new Throttler(100, UpdateStatusBar);
            #if DEBUG
            _taskRepository = new Core.Mock.TaskRepository();
            _sourceRepository = new Core.Mock.SourceRepository();
            _shelvesetRepository = new Core.Mock.ShelvesetRepository();
            #else
            var catalog = new DirectoryCatalog("Repositories");
            var container = new CompositionContainer(catalog);
            _taskRepository = container.GetExportedValueOrDefault<ITaskRepository>();
            _sourceRepository = container.GetExportedValueOrDefault<ISourceRepository>();
            _shelvesetRepository = container.GetExportedValueOrDefault<IShelvesetRepository>();
            #endif

            // ReSharper disable HeuristicUnreachableCode
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (_sourceRepository != null)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                _sourceRepository.Log = this;
            }

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (_taskRepository != null)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                _taskRepository.Log = this;
            }

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (_shelvesetRepository != null)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                _shelvesetRepository.Log = this;
            }

            // ReSharper restore HeuristicUnreachableCode

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
            newMenuItem.MenuItems.AddAction(
                new MenuAction("newTaskBrowser", "&Task Browser", true, () =>
                    {
                        AddComponent((tr, sor, shr) => new TaskBrowser(tr, sor, shr));
                    }
                )
            );
            newMenuItem.MenuItems.AddAction(
                new MenuAction("newBranchBrowser", "&Branch Browser", true, () =>
                    {
                        AddComponent((tr, sor, shr) => new BranchBrowser(tr, sor, shr));
                    }
                )
            );
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

            Start(Environment.GetCommandLineArgs());
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

            var keyData = keyEventArgs.KeyData;
            if (keyData == (Keys.Control | Keys.PageUp))
            {
                dockContent = mainPanel.GetPreviousDocument() ?? mainPanel.GetLastDocument();
            }
            else if (keyData == (Keys.Control | Keys.PageDown) || keyData == (Keys.Control | Keys.F6))
            {
                dockContent = mainPanel.GetNextDocument() ?? mainPanel.GetFirstDocument();
            }
            else if (keyData == (Keys.Control | Keys.Tab))
            {
                dockContent = _activationOrder.Penultimate;
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
        }

        public void Start(IEnumerable<string> arguments)
        {
            this.ToDo("Parse arguments and do something with them");
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
