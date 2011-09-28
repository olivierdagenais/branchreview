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

            pendingChanges.ActionsForChangesFunction = _sourceRepository.GetActionsForPendingChanges;
            pendingChanges.ChangesFunction = _sourceRepository.GetPendingChanges;
            pendingChanges.ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences;
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
            // TODO: update branch revisions, etc.
        }

        // method can not be made static because the Form Designer re-writes the event wire-up with "this."
        private void Grid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.IsInputKey = true;
            }
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
                }
                controlToFocus = taskGrid;
            }
            else if (tabs.SelectedTab == branchesTab)
            {
                if (branchGrid.DataTable == null || refresh)
                {
                    branchGrid.DataTable = null;
                    branchGrid.DataTable = _sourceRepository.LoadBranches();
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

        private static object FindSelectedId(DataGridView dataGridView)
        {
            var selectedRows = dataGridView.SelectedRows;
            object id = null;
            if (selectedRows.Count > 0)
            {
                var row = selectedRows[0];
                id = row.Cells["ID"].Value;
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

        private void taskGrid_DoubleClick(object sender, EventArgs e)
        {
            InvokeDefaultTaskGridAction();
        }

        private void taskGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                InvokeDefaultTaskGridAction();
            }
        }

        private void taskGrid_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            var menu = BuildTaskActionMenu();
            e.ContextMenuStrip = menu;
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
            var selectedRows = branchGrid.Grid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedRows[0];
                var branchId = row.Cells["ID"].Value;
                var taskId = row.Cells["TaskID"].Value;
                var builtInActions = new[]
                {
                    new MenuAction("defaultOpen", "&Open", row.Cells["BasePath"].Value != DBNull.Value,
                                () => SetCurrentBranch(branchId, taskId) ),
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

        private void branchGrid_DoubleClick(object sender, EventArgs e)
        {
            InvokeDefaultBranchGridAction();
        }

        private void branchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                InvokeDefaultBranchGridAction();
            }
        }

        private void branchGrid_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            var menu = BuildBranchActionMenu();
            e.ContextMenuStrip = menu;
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

        #endregion

        #region Activity/Log
        #endregion

        #region Commit

        #endregion
    }
}
