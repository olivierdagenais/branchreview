using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
        private readonly ReadOnlyCollection<SearchableDataGridView> _searchableGrids;

        private object _currentBranchId;
        private object _currentTaskId;

        public Main()
        {
            InitializeComponent();
            ConfigureDataGridView(taskGrid, false);
            ConfigureDataGridView(branchGrid, false);
            ConfigureDataGridView(changedFiles, true);
            changeLog.InitializeDefaults();
            patchText.InitializeDefaults();
            patchText.InitializeDiff();
            // one per tab
            _searchableGrids = new ReadOnlyCollection<SearchableDataGridView>(new[]
            {
                taskGrid,
                branchGrid,
                null, // TODO: activity
                null,
            });
            Load += Main_Load;
            FormClosing += Main_Closing;
            _taskRepository = new Mock.TaskRepository();
            _sourceRepository = new Mock.SourceRepository();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (searchTextBox.Focused)
                {
                    SwitchCurrentTab(false);
                    return true;
                }
            }
            var processCmdKey = base.ProcessCmdKey(ref msg, keyData);
            return processCmdKey;
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

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var grid = _searchableGrids[tabs.SelectedIndex];
            if (grid != null)
            {
                grid.Filter = searchTextBox.Text;
            }
        }

        #endregion

        #region Common

        private static void ConfigureDataGridView(DataGridView gridView, bool multiSelect)
        {
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToDeleteRows = false;
            gridView.AllowUserToResizeRows = false;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridView.BackgroundColor = SystemColors.Window;
            gridView.BorderStyle = BorderStyle.None;
            gridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridView.MultiSelect = multiSelect;
            gridView.ReadOnly = true;
            gridView.RowHeadersVisible = false;
            gridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            gridView.RowTemplate.Height = 23;
            gridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridView.ShowCellErrors = false;
            gridView.ShowEditingIcon = false;
            gridView.ShowRowErrors = false;
            gridView.StandardTab = true;
        }

        // TODO: make this an extension method on ToolStipItemCollection
        private static void AddSeparator(ToolStripItemCollection items)
        {
            var item = new ToolStripSeparator();
            items.Add(item);
        }

        private static void BuildActionMenu(IEnumerable<MenuAction> actions, ToolStripItemCollection items)
        {
            foreach (var menuAction in actions)
            {
                // to avoid "access to modified closure" warning
                var action = menuAction;

                ToolStripItem item;
                if (action.IsSeparator)
                {
                    AddSeparator(items);
                }
                else
                {
                    item = new ToolStripMenuItem
                    {
                        Name = action.Name,
                        Text = action.Caption,
                        Enabled = action.Enabled,
                        Image = action.Image,
                    };
                    item.Click += (clickSender, eventArgs) => action.Execute();
                    items.Add(item);
                }
            }
        }

        private void SetCurrentBranch(object branchId, object taskId)
        {
            _currentBranchId = branchId;
            _currentTaskId = taskId;
            activityTab.Text = "Log for {0}".FormatInvariant(branchId);
            commitTab.Text = "Changes in {0}".FormatInvariant(branchId);
            tabs.SelectedTab = activityTab;
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

        private void logMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = activityTab;
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
                searchTextBox.Text = taskGrid.Filter;
                controlToFocus = taskGrid;
            }
            else if (tabs.SelectedTab == branchesTab)
            {
                if (branchGrid.DataTable == null || refresh)
                {
                    branchGrid.DataTable = null;
                    branchGrid.DataTable = _sourceRepository.LoadBranches();
                }
                searchTextBox.Text = branchGrid.Filter;
                controlToFocus = branchGrid;
            }
            else if (tabs.SelectedTab == activityTab)
            {
                // TODO: set focus to the first thing
            }
            else if (tabs.SelectedTab == commitTab)
            {
                if (_currentBranchId != null)
                {
                    RefreshChangedFiles();
                }
                controlToFocus = changeLog;
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
            searchTextBox.Focus();
        }

        private void searchLabel_Click(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        internal static void InvokeFirstMenuItem(ContextMenuStrip menu)
        {
            if (menu.Items.Count > 0)
            {
                menu.Items[0].PerformClick();
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            searchTextBox.SelectAll();
        }

        #endregion

        #region Tasks

        private void tasksMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = tasksMenu.DropDownItems;
            items.Clear();

            var generalActions = _taskRepository.GetTaskActions();
            BuildActionMenu(generalActions, items);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddTaskSpecificActions(items, needsLeadingSeparator);
        }

        private void AddTaskSpecificActions(ToolStripItemCollection items, bool needsLeadingSeparator)
        {
            var taskId = FindSelectedId(taskGrid);
            if (taskId != null)
            {
                var specificActions = _taskRepository.GetTaskActions(taskId);
                if (specificActions.Count > 0)
                {
                    if (needsLeadingSeparator)
                    {
                        AddSeparator(items);
                    }
                    BuildActionMenu(specificActions, items);
                    AddSeparator(items);
                }
                var createBranchAction = new MenuAction("createBranch", "Create Branch for task", true, 
                    () => CreateBranch(taskId));
                BuildActionMenu(new[] { createBranchAction }, items);
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
            InvokeFirstMenuItem(menu);
        }

        #endregion

        #region Branches

        private void branchesMenu_DropDownOpening(object sender, EventArgs e)
        {
            var items = branchesMenu.DropDownItems;
            items.Clear();

            var generalActions = _sourceRepository.GetBranchActions();
            BuildActionMenu(generalActions, items);

            var needsLeadingSeparator = generalActions.Count > 0;
            AddBranchSpecificActions(items, needsLeadingSeparator);
        }

        private void AddBranchSpecificActions(ToolStripItemCollection items, bool needsLeadingSeparator)
        {
            var selectedRows = branchGrid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    AddSeparator(items);
                }
                var row = selectedRows[0];
                var branchId = row.Cells["ID"].Value;
                var taskId = row.Cells["TaskID"].Value;
                var builtInActions = new[]
                {
                    new MenuAction("defaultOpen", "&Open", row.Cells["BasePath"].Value != DBNull.Value,
                                () => SetCurrentBranch(branchId, taskId) ),
                };
                BuildActionMenu(builtInActions, items);
                var specificActions = _sourceRepository.GetBranchActions(branchId);
                if (specificActions.Count > 0)
                {
                    AddSeparator(items);
                    BuildActionMenu(specificActions, items);
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
            InvokeFirstMenuItem(menu);
        }

        #endregion

        #region Activity/Log
        #endregion

        #region Commit

        void changedFiles_DoubleClick(object sender, EventArgs e)
        {
            InvokeDefaultChangedFilesAction();
        }

        void changedFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                InvokeDefaultChangedFilesAction();
            }
        }

        void changedFiles_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            var menu = BuildChangedFilesActionMenu();
            e.ContextMenuStrip = menu;
        }

        void changedFiles_SelectionChanged(object sender, EventArgs e)
        {
            var selectedIds = FindSelectedIds();
            var patch = _sourceRepository.ComputeDifferences(selectedIds);
            patchText.SetReadOnlyText(patch);
        }

        private ContextMenuStrip BuildChangedFilesActionMenu()
        {
            var selectedIds = FindSelectedIds();
            var actions = _sourceRepository.GetActionsForPendingChanges(selectedIds);
            var menu = new ContextMenuStrip();
            BuildActionMenu(actions, menu.Items);
            return menu;
        }

        private IEnumerable<object> FindSelectedIds()
        {
            var selectedRows = changedFiles.SelectedRows.Cast<DataGridViewRow>();
            return selectedRows.Map(row => row.Cells["ID"].Value);
        }

        private void InvokeDefaultChangedFilesAction()
        {
            var menu = BuildChangedFilesActionMenu();
            InvokeFirstMenuItem(menu);
        }

        private void RefreshChangedFiles()
        {
            // TODO: also preserve focused item(s)?
            var oldSelection = FindSelectedIds().ToDictionary(o => o);

            changedFiles.DataTable = null;
            var pendingChanges = _sourceRepository.GetPendingChanges(_currentBranchId);
            changedFiles.DataTable = pendingChanges;
            if (0 == changedFiles.Rows.Count)
            {
                patchText.SetReadOnlyText(String.Empty);
            }
            else
            {
                changedFiles.SelectionChanged -= changedFiles_SelectionChanged;
                if (0 == oldSelection.Count)
                {
                    // if nothing was selected, select the first one
                    changedFiles.Rows[0].Selected = true;
                }
                else
                {
                    foreach (DataGridViewRow row in changedFiles.Rows)
                    {
                        var id = row.Cells["ID"].Value;
                        if (oldSelection.ContainsKey(id))
                        {
                            row.Selected = true;
                            oldSelection.Remove(id);
                        }
                    }
                }
                changedFiles_SelectionChanged(this, null);
                changedFiles.SelectionChanged += changedFiles_SelectionChanged;
            }
        }

        #endregion
    }
}
