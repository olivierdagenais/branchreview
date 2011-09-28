using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Properties;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form
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
            _taskRepository = new Mock.TaskRepository();
            _sourceRepository = new Mock.SourceRepository();

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
            settings.WindowState = WindowState;
            if (WindowState != FormWindowState.Normal)
            {
                settings.WindowLocation = RestoreBounds.Location;
                settings.WindowSize = RestoreBounds.Size;
            }
            else
            {
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

        private void tasksMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = taskTab;
        }

        private void branchesMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = branchesTab;
        }

        private void commitMenuItem_Click(object sender, EventArgs e)
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
            // TODO: invoke the search of the focused grid
        }

        #endregion

        #region Tasks

        private void tasksMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = tasksMenu.DropDownItems;
            items.Clear();

            var generalActions = _taskRepository.GetTaskActions();
            items.AddActions(generalActions);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddTaskSpecificActions(items, needsLeadingSeparator);
        }

        private void AddTaskSpecificActions(ToolStripItemCollection items, bool needsLeadingSeparator)
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
                var createBranchAction = new MenuAction("createBranch", "Create Branch for task", true, 
                    () => CreateBranch(taskId));
                items.AddAction(createBranchAction);
            }
        }

        private void CreateBranch(object taskId)
        {
            _sourceRepository.CreateBranch(taskId);
        }

        private void taskGrid_ContextMenuStripNeeded(object sender, ContextMenuStripNeededEventArgs e)
        {
            var menu = BuildTaskActionMenu();
            e.ContextMenuStrip = menu;
        }

        private void taskGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultTaskGridAction();
        }

        private ContextMenuStrip BuildTaskActionMenu()
        {
            var menu = new ContextMenuStrip();
            AddTaskSpecificActions(menu.Items, false);
            return menu;
        }

        private void InvokeDefaultTaskGridAction()
        {
            var menu = BuildTaskActionMenu();
            menu.Items.InvokeFirstMenuItem();
        }

        #endregion

        #region Branches

        private void branchesMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = branchesMenu.DropDownItems;
            items.Clear();

            var generalActions = _sourceRepository.GetBranchActions();
            items.AddActions(generalActions);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddBranchSpecificActions(items, needsLeadingSeparator);
        }

        private void AddBranchSpecificActions(ToolStripItemCollection items, bool needsLeadingSeparator)
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

        private void branchGrid_ContextMenuStripNeeded(object sender, ContextMenuStripNeededEventArgs e)
        {
            var menu = BuildBranchActionMenu();
            e.ContextMenuStrip = menu;
        }

        private void branchGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultBranchGridAction();
        }

        private ContextMenuStrip BuildBranchActionMenu()
        {
            var menu = new ContextMenuStrip();
            AddBranchSpecificActions(menu.Items, false);
            return menu;
        }

        private void InvokeDefaultBranchGridAction()
        {
            var menu = BuildBranchActionMenu();
            menu.Items.InvokeFirstMenuItem();
        }

        private ContextMenuStrip BuildRevisionActionMenu()
        {
            var menu = new ContextMenuStrip();
            AddRevisionSpecificActions(menu.Items, false);
            return menu;
        }

        private void AddRevisionSpecificActions(ToolStripItemCollection items, bool needsLeadingSeparator)
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
            menu.Items.InvokeFirstMenuItem();
        }

        private void activityRevisions_ContextMenuStripNeeded(object sender, ContextMenuStripNeededEventArgs e)
        {
            var menu = BuildRevisionActionMenu();
            e.ContextMenuStrip = menu;
        }

        #endregion

        #region Commit

        #endregion
    }
}
