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
        private static readonly DataGridViewCellStyle AlternatingRowStyle =
            new DataGridViewCellStyle { BackColor = Color.WhiteSmoke };
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

        void Main_Load(object sender, EventArgs e)
        {
            var settings = Settings.Default;
            ClientSize = settings.WindowSize;
            WindowState = settings.WindowState;
            menuStrip.Location = settings.MenuLocation;
            searchStrip.Location = settings.SearchLocation;
            SwitchCurrentTab(true);
        }

        void Main_Closing(object sender, CancelEventArgs e)
        {
            var settings = Settings.Default;
            settings.MenuLocation = new Point(menuStrip.Location.X - 3, menuStrip.Location.Y);
            settings.SearchLocation = searchStrip.Location;
            settings.WindowSize = ClientSize;
            settings.WindowState = WindowState;
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
            gridView.AlternatingRowsDefaultCellStyle = AlternatingRowStyle;
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

        private static ContextMenuStrip BuildActionMenu(IEnumerable<MenuAction> actions)
        {
            var menu = new ContextMenuStrip();
            foreach (var menuAction in actions)
            {
                // to avoid "access to modified closure" warning
                var action = menuAction;

                ToolStripItem item;
                if (action.Caption == MenuAction.Separator)
                {
                    item = new ToolStripSeparator();
                }
                else
                {
                    item = new ToolStripButton(action.Caption, action.Image, 
                        (clickSender, eventArgs) => action.Execute(), action.Name);
                }
                item.Enabled = action.Enabled;
                menu.Items.Add(item);
            }
            return menu;
        }

        private void SetCurrentBranch(object branchId, object taskId)
        {
            _currentBranchId = branchId;
            _currentTaskId = taskId;
            activityTab.Text = "Log for {0}".FormatInvariant(branchId);
            commitTab.Text = "Commit to {0}".FormatInvariant(branchId);
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

        #endregion

        #region Tasks

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
            var row = taskGrid.Rows[e.RowIndex];
            var taskId = row.Cells["ID"].Value;
            var actions = _taskRepository.GetActionsForTask(taskId);
            var menu = BuildActionMenu(actions);
            e.ContextMenuStrip = menu;
        }

        private ContextMenuStrip BuildTaskActionMenuForRow(int rowIndex)
        {
            var row = taskGrid.Rows[rowIndex];
            var taskId = row.Cells["ID"].Value;
            var actions = _taskRepository.GetActionsForTask(taskId);
            return BuildActionMenu(actions);
        }

        private void InvokeDefaultTaskGridAction()
        {
            var selectdRow = taskGrid.SelectedRows[0].Index;
            var menu = BuildTaskActionMenuForRow(selectdRow);
            menu.Items[0].PerformClick();
        }

        #endregion

        #region Branches

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
            var menu = BuildBranchActionMenuForRow(e.RowIndex);
            e.ContextMenuStrip = menu;
        }

        private ContextMenuStrip BuildBranchActionMenuForRow(int rowIndex)
        {
            var row = branchGrid.Rows[rowIndex];
            var branchId = row.Cells["ID"].Value;
            var taskId = row.Cells["TaskID"].Value;
            var builtInActions = new[]
            {
                new MenuAction("defaultOpen", "Open", row.Cells["BasePath"].Value != DBNull.Value,
                            () => SetCurrentBranch(branchId, taskId) ),
                new MenuAction(null, MenuAction.Separator, false, null),
            };
            var actions = _sourceRepository.GetActionsForBranch(branchId);
            return BuildActionMenu(builtInActions.Compose(actions));
        }

        private void InvokeDefaultBranchGridAction()
        {
            var selectdRow = branchGrid.SelectedRows[0].Index;
            var menu = BuildBranchActionMenuForRow(selectdRow);
            menu.Items[0].PerformClick();
        }

        #endregion

        #region Activity/Log
        #endregion

        #region Commit

        void changedFiles_SelectionChanged(object sender, EventArgs e)
        {
            var rows = changedFiles.Rows.Cast<DataGridViewRow>();
            var selectedRows = rows.Filter(row => row.Selected);
            var selectedRowIds = selectedRows.Map(row => row.Cells["ID"].Value);
            var patch = _sourceRepository.ComputeDifferences(selectedRowIds);
            patchText.SetReadOnlyText(patch);
        }

        private void RefreshChangedFiles()
        {
            // TODO: also preserve focused item(s)?
            var oldSelection = new HashSet<object>();
            foreach (DataGridViewRow row in changedFiles.SelectedRows)
            {
                oldSelection.Add(row.Cells["ID"].Value);
            }

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
                        if (oldSelection.Contains(id))
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
