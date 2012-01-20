using System;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Properties;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form, ILog
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;

        private object _currentBranchId;
        private object _currentTaskId;
        private object _currentRevision;

        public Main()
        {
            InitializeComponent();
            taskGrid.Grid.MultiSelect = false;
            branchGrid.Grid.MultiSelect = false;
            activityRevisions.Grid.MultiSelect = false;
            Load += Main_Load;
            FormClosing += Main_Closing;
            #if DEBUG
            _taskRepository = new Core.Mock.TaskRepository();
            _sourceRepository = new Core.Mock.SourceRepository();
            #else
            var catalog = new DirectoryCatalog("Repositories");
            var container = new CompositionContainer(catalog);
            _taskRepository = container.GetExportedValue<ITaskRepository>();
            _sourceRepository = container.GetExportedValue<ISourceRepository>();
            #endif
            this.pendingChanges.ChangeLog.KeyDown += ChangeLog_KeyDown;

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

        void Main_Load(object sender, EventArgs e)
        {
            var settings = Settings.Default;
            Size = settings.WindowSize;
            WindowState = settings.WindowState;
            SwitchCurrentTab(true);
        }

        void Main_Closing(object sender, CancelEventArgs e)
        {
            var settings = Settings.Default;
            if (WindowState == FormWindowState.Minimized)
            {
                settings.WindowState = FormWindowState.Normal;
                settings.WindowLocation = RestoreBounds.Location;
                settings.WindowSize = RestoreBounds.Size;
            }
            else
            {
                settings.WindowState = WindowState;
                settings.WindowLocation = Location;
                settings.WindowSize = Size;
            }
            settings.Save();
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
            activityRevisions.DataTable = null;
            activityChangeInspector.Context = null;
            if (branchId != null)
            {
                activityRevisions.DataTable = _sourceRepository.LoadRevisions(branchId);
                var activityCount = activityRevisions.DataTable.Rows.Count;
                activityRevisions.Caption = "Activity for {0}: {1} entr{2}".FormatInvariant(
                    branchId, activityCount, activityCount == 1 ? "y" : "ies");
            }
            else
            {
                activityRevisions.Caption = String.Empty;
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
            SwitchCurrentTab(false);
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

        private void SwitchCurrentTab(bool refresh)
        {
            Control controlToFocus = null;
            if (tabs.SelectedTab == taskTab)
            {
                if (taskGrid.DataTable == null || refresh)
                {
                    taskGrid.DataTable = null;
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
                    branchGrid.DataTable = null;
                    branchGrid.DataTable = _sourceRepository.LoadBranches();
                    var branchCount = branchGrid.DataTable.Rows.Count;
                    branchGrid.Caption = "{0} branch{1}".FormatInvariant(branchCount, branchCount == 1 ? "" : "es");
                    SetCurrentBranch(null, null);
                }
                controlToFocus = branchGrid;
            }
            else if (tabs.SelectedTab == commitTab)
            {
                pendingChanges.Context = _currentBranchId;
                controlToFocus = pendingChanges;
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

        private static object FindSelectedId(ListView listView)
        {
            var selectedItems = listView.SelectedItems;
            object id = null;
            if (selectedItems.Count > 0)
            {
                var item = selectedItems[0];
                id = item.GetRow()["ID"];
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

        private void CreateBranch(object taskId)
        {
            _sourceRepository.CreateBranch(taskId);
        }

        private void GoToBranchFor(object taskId)
        {
            if (branchGrid.DataTable == null)
            {
                branchGrid.DataTable = _sourceRepository.LoadBranches();
            }
            var dataRow = branchGrid.DataTable.FindFirst("TaskID", taskId);
            SetCurrentBranch(dataRow["ID"], taskId);
            var listViewItem = branchGrid.Grid.Items.Cast<ListViewItem>().First(lvi => lvi.GetRow() == dataRow);
            listViewItem.Selected = true;
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
            if (e.AffectedProperty == "Visible" && branchesTab.Tag == null)
            {
                var settings = Settings.Default;
                branchGridAndRestDivider.SplitterDistance = settings.branchGridAndRestDividerSplitterDistance;
                activityTopBottomPanel.SplitterDistance = settings.activityTopBottomPanelSplitterDistance;
                activityChangeInspector.HorizontalDividerSplitterDistance =
                    settings.activityChangeInspectorHorizontalDividerSplitterDistance;
                activityChangeInspector.VerticalDividerSplitterDistance =
                    settings.activityChangeInspectorVerticalDividerSplitterDistance;
                branchesTab.Tag = "done";
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

        private void AddBranchSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = branchGrid.Grid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var item = selectedItems[0];
                var row = item.GetRow();
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

        private void branchGrid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildBranchActionMenu();
            e.ContextMenu = menu;
        }

        private void branchGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultBranchGridAction();
        }

        private ContextMenu BuildBranchActionMenu()
        {
            var menu = new ContextMenu();
            AddBranchSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void InvokeDefaultBranchGridAction()
        {
            var menu = BuildBranchActionMenu();
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
            var selectedItems = activityRevisions.Grid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var item = selectedItems[0];
                var row = item.GetRow();
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
                Settings.Default.branchGridAndRestDividerSplitterDistance = branchGridAndRestDivider.SplitterDistance;
            }
        }

        private void activityTopBottomPanel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (branchesTab.Tag != null)
            {
                Settings.Default.activityTopBottomPanelSplitterDistance = activityTopBottomPanel.SplitterDistance;
            }
        }

        private void activityChangeInspector_HorizontalDividerSplitterMoved(object sender, SplitterEventArgs e)
        {
            if (branchesTab.Tag != null)
            {
                Settings.Default.activityChangeInspectorHorizontalDividerSplitterDistance =
                    activityChangeInspector.HorizontalDividerSplitterDistance;
            }
        }

        private void activityChangeInspector_VerticalDividerSplitterMoved(object sender, SplitterEventArgs e)
        {
            if (branchesTab.Tag != null)
            {
                Settings.Default.activityChangeInspectorVerticalDividerSplitterDistance =
                    activityChangeInspector.VerticalDividerSplitterDistance;
            }
        }

        #endregion

        #region Commit

        private void commitTab_Layout(object sender, LayoutEventArgs e)
        {
            if (e.AffectedProperty == "Visible" && commitTab.Tag == null)
            {
                var settings = Settings.Default;
                pendingChanges.HorizontalDividerSplitterDistance =
                    settings.pendingChangesHorizontalDividerSplitterDistance;
                pendingChanges.VerticalDividerSplitterDistance =
                    settings.pendingChangesVerticalDividerSplitterDistance;
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

        private void pendingChanges_HorizontalDividerSplitterMoved(object sender, SplitterEventArgs e)
        {
            if (commitTab.Tag != null)
            {
                Settings.Default.pendingChangesHorizontalDividerSplitterDistance =
                    pendingChanges.HorizontalDividerSplitterDistance;
            }
        }

        private void pendingChanges_VerticalDividerSplitterMoved(object sender, SplitterEventArgs e)
        {
            if (commitTab.Tag != null)
            {
                Settings.Default.pendingChangesVerticalDividerSplitterDistance =
                    pendingChanges.VerticalDividerSplitterDistance;
            }
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
