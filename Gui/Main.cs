using System;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.BranchAndReviewTools.Gui.Properties;
using SoftwareNinjas.Core;
using EnumExtensions = SoftwareNinjas.BranchAndReviewTools.Gui.Extensions.EnumExtensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form, ILog
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;

        private bool _canRestoreLayout;

        private object _currentBranchId;
        private object _currentTaskId;
        private object _currentRevision;
        private object _currentShelvesetId;

        public Main()
        {
            InitializeComponent();
            taskGrid.Grid.MultiSelect = false;
            branchGrid.Grid.MultiSelect = false;
            activityRevisions.Grid.MultiSelect = false;
            shelvesetGrid.Grid.MultiSelect = false;
            Load += Main_Load;
            FormClosing += Main_Closing;
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
                this.pendingChanges.ChangeLog.KeyDown += ChangeLog_KeyDown;

                activityChangeInspector.ChangeLog.IsReadOnly = true;
                activityChangeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
                activityChangeInspector.ActionsForChangesFunction = _sourceRepository.GetActionsForRevisionChanges;
                activityChangeInspector.ChangesFunction = _sourceRepository.GetRevisionChanges;
                activityChangeInspector.ComputeDifferencesFunction = _sourceRepository.ComputeRevisionDifferences;
                activityChangeInspector.MessageFunction = _sourceRepository.GetRevisionMessage;

                pendingChanges.ActionsForChangesFunction = _sourceRepository.GetActionsForPendingChanges;
                pendingChanges.ChangesFunction = _sourceRepository.GetPendingChanges;
                pendingChanges.ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences;
                pendingChanges.MessageFunction = null;
            }
            else
            {
                this.tabs.Controls.Remove(branchesTab);
                this.tabs.Controls.Remove(commitTab);
                this.viewMenu.MenuItems.Remove(this.goToBranchesMenuItem);
                this.viewMenu.MenuItems.Remove(this.goToPendingMenuItem);
                this.menuStrip.MenuItems.Remove(branchesMenu);
                this.menuStrip.MenuItems.Remove(commitMenu);
            }

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (_taskRepository != null)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                // TODO: wire up events, etc.
            }
            else
            {
                this.tabs.Controls.Remove(taskTab);
                this.viewMenu.MenuItems.Remove(this.goToTasksMenuItem);
                this.menuStrip.MenuItems.Remove(tasksMenu);
            }

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (_shelvesetRepository != null)
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                shelvesetChangeInspector.ChangeLog.IsReadOnly = true;
                shelvesetChangeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
                shelvesetChangeInspector.ActionsForChangesFunction = _shelvesetRepository.GetActionsForShelvesetChanges;
                shelvesetChangeInspector.ChangesFunction = _shelvesetRepository.GetShelvesetChanges;
                shelvesetChangeInspector.ComputeDifferencesFunction = _shelvesetRepository.ComputeShelvesetDifferences;
                shelvesetChangeInspector.MessageFunction = _shelvesetRepository.GetShelvesetMessage;

                this.shelvesetHistory.Push(this.shelvesetGrid);
            }
            else
            {
                this.tabs.Controls.Remove(shelvesetsTab);
                this.viewMenu.MenuItems.Remove(this.goToShelvesetsMenuItem);
                this.menuStrip.MenuItems.Remove(shelvesetsMenu);
            }

            // ReSharper restore HeuristicUnreachableCode
        }

        void Main_Load(object sender, EventArgs e)
        {
            Size = this.LoadSetting(() => Size, new Size(800, 600), SizeExtensions.Parse);
            Location = this.LoadSetting(() => Location, Point.Empty, PointExtensions.Parse);
            WindowState = 
                this.LoadSetting(() => WindowState, FormWindowState.Normal, EnumExtensions.Parse<FormWindowState>);
            SwitchCurrentTab(true);
            _canRestoreLayout = true;
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
                IHistoryContainer container = null;
                if (tabs.SelectedTab == taskTab)
                {
                    // TODO: implement
                }
                else if (tabs.SelectedTab == branchesTab)
                {
                    // TODO: implement
                }
                else if (tabs.SelectedTab == commitTab)
                {
                    // TODO: implement
                }
                else if (tabs.SelectedTab == shelvesetsTab)
                {
                    container = shelvesetHistory;
                }

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


        #region Menu items

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            SwitchCurrentTab(true);
        }

        #endregion

        #region Common

        private void SetCurrentBranch(object branchId, object taskId)
        {
            _currentBranchId = branchId;
            _currentTaskId = taskId;
            if (branchId != null)
            {
                activityRevisions.DataTable = _sourceRepository.LoadRevisions(branchId);
                var activityCount = activityRevisions.DataTable.Rows.Count;
                activityRevisions.Caption = "Activity for {0}: {1} entr{2}".FormatInvariant(
                    branchId, activityCount, activityCount == 1 ? "y" : "ies");
            }
            else
            {
                activityChangeInspector.Context = null;
                activityRevisions.DataTable = null;
                activityRevisions.Caption = String.Empty;
            }
        }

        private void SetCurrentShelveset(object shelvesetId)
        {
            _currentShelvesetId = shelvesetId;
            if (shelvesetId != null)
            {
                shelvesetChangeInspector.Context = shelvesetId;
                shelvesetChangeInspector.Title = shelvesetId.ToString();
                shelvesetHistory.Push(shelvesetChangeInspector);
            }
            else
            {
                shelvesetChangeInspector.Context = null;
            }
        }

        private void StartWorkOnBranch(object branchId, object taskId)
        {
            SetCurrentBranch(branchId, taskId);
            tabs.SelectedTab = commitTab;
            Text = "{0} - Branch and Review Tools".FormatCurrentCulture(branchId);
        }

        private void tabs_Selected(object sender, TabControlEventArgs e)
        {
            ExecuteLater(10, () => SwitchCurrentTab(false));
        }

        private void goToTasksMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = taskTab;
        }

        private void goToBranchesMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = branchesTab;
        }

        private void goToPendingMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = commitTab;
        }

        private void goToShelvesetsMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = shelvesetsTab;
        }

        private void SwitchCurrentTab(bool refresh)
        {
            Control controlToFocus = null;
            if (tabs.SelectedTab == taskTab)
            {
                if (taskGrid.DataTable == null || refresh)
                {
                    taskGrid.DataTable = _taskRepository.LoadTasks();
                    var taskCount = taskGrid.DataTable.Rows.Count;
                    taskGrid.Caption = "{0} task{1}".FormatInvariant(taskCount, taskCount == 1 ? "" : "s");
                }
                controlToFocus = taskGrid;
            }
            else if (tabs.SelectedTab == branchesTab)
            {
                if (branchGrid.DataTable == null || refresh)
                {
                    branchGrid.DataTable = _sourceRepository.LoadBranches();
                    var branchCount = branchGrid.DataTable.Rows.Count;
                    branchGrid.Caption = "{0} branch{1}".FormatInvariant(branchCount, branchCount == 1 ? "" : "es");
                }
                controlToFocus = branchGrid;
            }
            else if (tabs.SelectedTab == commitTab)
            {
                pendingChanges.Context = _currentBranchId;
                controlToFocus = pendingChanges;
            }
            else if (tabs.SelectedTab == shelvesetsTab)
            {
                if (shelvesetGrid.DataTable == null || refresh)
                {
                    shelvesetGrid.DataTable = _shelvesetRepository.LoadShelvesets();
                    var shelvesetCount = shelvesetGrid.DataTable.Rows.Count;
                    shelvesetGrid.Caption = "{0} shelveset{1}".FormatInvariant(shelvesetCount, shelvesetCount == 1 ? "" : "s");
                }

                controlToFocus = shelvesetGrid;
            }

            if (controlToFocus != null)
            {
                ExecuteLater(10, () => controlToFocus.Focus());
            }
        }

        // TODO: turn into extension method on Control
        internal void ExecuteLater(int milliseconds, Action action)
        {
            var delayedWorker = new Timer {Interval = milliseconds};
            delayedWorker.Tick += (s, ea) =>
            {
                delayedWorker.Stop();
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(action));
                }
                else
                {
                    action();
                }
            };
            delayedWorker.Start();
        }

        private static object FindSelectedId(IAccessibleGrid accessibleGrid)
        {
            var selectedRows = accessibleGrid.SelectedRows;
            object id = null;
            if (selectedRows.Count > 0)
            {
                var row = selectedRows[0];
                id = row.DataRow["ID"];
            }
            return id;
        }

        private void searchMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send(/* Ctrl+F */ "^f");
        }

        #endregion

        #region Tasks

        private void tasksMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = tasksMenu.MenuItems;
            items.Clear();

            var generalActions = _taskRepository.GetTaskActions();
            items.AddActions(generalActions);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddTaskSpecificActions(items, needsLeadingSeparator);
        }

        private void AddTaskSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var taskId = FindSelectedId(taskGrid.Grid);
            if (taskId != null)
            {
                var specificActions = _taskRepository.GetTaskActions(taskId);
                if (specificActions.Count > 0)
                {
                    if (needsLeadingSeparator)
                    {
                        items.AddSeparator();
                    }
                    items.AddActions(specificActions);
                }
                if (_sourceRepository != null)
                {
                    if (specificActions.Count > 0)
                    {
                        items.AddSeparator();
                    }
                    items.AddActions(
                        new MenuAction("createBranch", "Create branch for task {0}".FormatInvariant(taskId), true, 
                            () => CreateBranch(taskId)),
                        new MenuAction("goToBranch", "Go to branch for task {0}".FormatInvariant(taskId), true,
                            () => GoToBranchFor(taskId))
                    );
                }
            }
        }

        private void CreateBranch(object taskId)
        {
            Debug.Assert(_sourceRepository != null);
            _sourceRepository.CreateBranch(taskId);
        }

        private void GoToBranchFor(object taskId)
        {
            Debug.Assert(_sourceRepository != null);
            if (branchGrid.DataTable == null)
            {
                branchGrid.DataTable = _sourceRepository.LoadBranches();
            }
            var dataRow = branchGrid.DataTable.FindFirst("TaskID", taskId);
            SetCurrentBranch(dataRow["ID"], taskId);

            var gridItem =
                branchGrid.Grid.Rows.First(item => item.DataRow == dataRow);
            gridItem.Selected = true;
            tabs.SelectedTab = branchesTab;
        }

        private void taskGrid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildTaskActionMenu();
            e.ContextMenu = menu;
        }

        private void taskGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultTaskGridAction();
        }

        private ContextMenu BuildTaskActionMenu()
        {
            var menu = new ContextMenu();
            AddTaskSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void InvokeDefaultTaskGridAction()
        {
            var menu = BuildTaskActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        #endregion

        #region Branches

        private void branchesTab_Layout(object sender, LayoutEventArgs e)
        {
            if (_canRestoreLayout && e.AffectedProperty == "Visible" && branchesTab.Tag == null)
            {
                branchGridAndRestDivider.SplitterDistance =
                    this.LoadSetting(() => branchGridAndRestDivider.SplitterDistance, 115);
                activityTopBottomPanel.SplitterDistance =
                    this.LoadSetting(() => activityTopBottomPanel.SplitterDistance, 115);
                activityChangeInspector.HorizontalDividerSplitterDistance =
                    this.LoadSetting(() => activityChangeInspector.HorizontalDividerSplitterDistance, 85);
                activityChangeInspector.VerticalDividerSplitterDistance =
                    this.LoadSetting(() => activityChangeInspector.VerticalDividerSplitterDistance, 242);
                branchesTab.Tag = "done";
            }
        }

        private void shelvesetsTab_Layout(object sender, LayoutEventArgs e)
        {
            if (_canRestoreLayout && e.AffectedProperty == "Visible" && shelvesetsTab.Tag == null)
            {
                shelvesetsTab.Tag = "done";
            }
        }

        private void branchesMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = branchesMenu.MenuItems;
            items.Clear();

            var generalActions = _sourceRepository.GetBranchActions();
            items.AddActions(generalActions);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddBranchSpecificActions(items, needsLeadingSeparator);
        }

        private void shelvesetsMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = shelvesetsMenu.MenuItems;
            items.Clear();

            var generalActions = _shelvesetRepository.GetShelvesetActions();
            items.AddActions(generalActions);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddShelvesetSpecificActions(items, needsLeadingSeparator);
        }

        private void AddBranchSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedRows = branchGrid.Grid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedRows[0].DataRow;
                var branchId = row["ID"];
                var taskId = row["TaskID"];
                var builtInActions = new[]
                {
                    new MenuAction("defaultInspect", "&Inspect", true,
                                () => SetCurrentBranch(branchId, taskId) ),
                    new MenuAction("defaultOpen", "&Work on this", row["BasePath"] != DBNull.Value,
                                () => StartWorkOnBranch(branchId, taskId) ),
                };
                items.AddActions(builtInActions);
                var specificActions = _sourceRepository.GetBranchActions(branchId);
                if (specificActions.Count > 0)
                {
                    items.AddSeparator();
                    items.AddActions(specificActions);
                }
            }
        }

        private void AddShelvesetSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = shelvesetGrid.Grid.SelectedRows;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedItems[0].DataRow;
                var shelvesetId = row["ID"];
                var builtInActions = new[]
                {
                    new MenuAction("defaultInspect", "&Inspect", true,
                                () => SetCurrentShelveset(shelvesetId) ),
                };
                items.AddActions(builtInActions);
                var specificActions = _shelvesetRepository.GetShelvesetActions(shelvesetId);
                if (specificActions.Count > 0)
                {
                    items.AddSeparator();
                    items.AddActions(specificActions);
                }
            }
        }

        private void branchGrid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildBranchActionMenu();
            e.ContextMenu = menu;
        }

        private void shelvesetGrid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildShelvesetActionMenu();
            e.ContextMenu = menu;
        }

        private void branchGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultBranchGridAction();
        }

        private void shelvesetGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultShelvesetGridAction();
        }

        private ContextMenu BuildBranchActionMenu()
        {
            var menu = new ContextMenu();
            AddBranchSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private ContextMenu BuildShelvesetActionMenu()
        {
            var menu = new ContextMenu();
            AddShelvesetSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void InvokeDefaultBranchGridAction()
        {
            var menu = BuildBranchActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        private void InvokeDefaultShelvesetGridAction()
        {
            var menu = BuildShelvesetActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        private ContextMenu BuildRevisionActionMenu()
        {
            var menu = new ContextMenu();
            AddRevisionSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void AddRevisionSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = activityRevisions.Grid.SelectedRows;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedItems[0].DataRow;
                var revisionId = row["ID"];
                var builtInActions = new[]
                {
                    new MenuAction("defaultOpen", "&Open", true,
                                () => SetCurrentRevision(revisionId) ),
                };
                items.AddActions(builtInActions);
                // TODO: Should there be specific actions per revision?  Maybe an external view, like TFS has...
            }
        }

        private void SetCurrentRevision(object revisionId)
        {
            _currentRevision = revisionId;
            activityChangeInspector.Context = revisionId;
        }

        private void activityRevisions_RowInvoked(object sender, EventArgs e)
        {
            var menu = BuildRevisionActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        private void activityRevisions_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildRevisionActionMenu();
            e.ContextMenu = menu;
        }

        private void branchGridAndRestDivider_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (branchesTab.Tag != null)
            {
                branchGridAndRestDivider.SaveSetting(() => branchGridAndRestDivider.SplitterDistance);
            }
        }

        private void activityTopBottomPanel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (branchesTab.Tag != null)
            {
                activityTopBottomPanel.SaveSetting(() => activityTopBottomPanel.SplitterDistance);
            }
        }

        #endregion

        #region Commit

        private void commitTab_Layout(object sender, LayoutEventArgs e)
        {
            if (_canRestoreLayout && e.AffectedProperty == "Visible" && commitTab.Tag == null)
            {
                pendingChanges.HorizontalDividerSplitterDistance =
                    pendingChanges.LoadSetting(() => pendingChanges.HorizontalDividerSplitterDistance, 70);
                pendingChanges.VerticalDividerSplitterDistance =
                    pendingChanges.LoadSetting(() => pendingChanges.VerticalDividerSplitterDistance, 273);
                commitTab.Tag = "done";
            }
        }

        private void commitMenu_DropDownOpening(object sender, EventArgs e)
        {
            // TODO: contribute actions from the ISourceRepository

            var dataSource = pendingChanges.FileGrid.Grid.DataSource;
            // TODO: We should consider checking _currentBranch, too
            commitMenuItem.Enabled = dataSource != null;
        }

        private void commitMenuItem_Click(object sender, EventArgs e)
        {
            if (tabs.SelectedTab != commitTab)
            {
                tabs.SelectedTab = commitTab;
                var dataSource = pendingChanges.FileGrid.Grid.DataSource;
                var itemCount = dataSource.Rows.Count;
                var result = MessageBox.Show(
                    "Are you sure you want to commit {0} item{1} to {2}?".FormatInvariant(
                        itemCount,
                        itemCount == 1 ? "" : "s",
                        _currentBranchId
                    ),
                    "Please confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                );
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            DoCommit();
        }

        void ChangeLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode && e.Control)
            {
                DoCommit();
                e.SuppressKeyPress = true;
            }
        }

        private void DoCommit()
        {
            var message = pendingChanges.ChangeLog.Text;
            _sourceRepository.Commit(_currentBranchId, message);
            pendingChanges.ChangeLog.Text = String.Empty;
            SwitchCurrentTab(true);
        }

        #endregion

        #region Implementation of ILog

        // TODO: light-up on Windows 7 by displaying progress in the task bar and changing the back colour
        // depending on the log type
        public void Info(string message)
        {
            statusBarText.Image = Resources.dialog_information;
            statusBarText.Text = message;
            statusBarProgress.Value = 0;
        }

        public void Info(string message, int progressValue, int progressMaximum)
        {
            statusBarText.Image = Resources.dialog_information;
            statusBarText.Text = message;
            if (progressMaximum == 0)
            {
                statusBarProgress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                statusBarProgress.Style = ProgressBarStyle.Continuous;
                statusBarProgress.Maximum = progressMaximum;
                statusBarProgress.Value = progressValue;
            }
        }

        public void Warning(string message)
        {
            statusBarText.Image = Resources.dialog_warning;
            statusBarText.Text = message;
            statusBarProgress.Value = 0;
        }

        public void Error(string message)
        {
            statusBarText.Image = Resources.dialog_error;
            statusBarText.Text = message;
            statusBarProgress.Value = 0;
        }

        #endregion
    }
}
