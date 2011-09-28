﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
            ConfigureDataGridView(taskGrid);
            ConfigureDataGridView(branchGrid);
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
            taskGrid.DataTable = _taskRepository.LoadTasks();
            _sourceRepository = new Mock.SourceRepository();
            branchGrid.DataTable = _sourceRepository.LoadBranches();
        }

        private static void ConfigureDataGridView(DataGridView gridView)
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
            gridView.MultiSelect = false;
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

        void Main_Load(object sender, System.EventArgs e)
        {
            var settings = Settings.Default;
            ClientSize = settings.WindowSize;
            WindowState = settings.WindowState;
            menuStrip.Location = settings.MenuLocation;
            searchStrip.Location = settings.SearchLocation;
        }

        void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var settings = Settings.Default;
            settings.MenuLocation = new Point(menuStrip.Location.X - 3, menuStrip.Location.Y);
            settings.SearchLocation = searchStrip.Location;
            settings.WindowSize = ClientSize;
            settings.WindowState = WindowState;
            settings.Save();
        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void searchTextBox_TextChanged(object sender, System.EventArgs e)
        {
            var grid = _searchableGrids[tabs.SelectedIndex];
            if (grid != null)
            {
                grid.Filter = searchTextBox.Text;
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

        private void branchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                InvokeDefaultBranchGridAction();
            }
        }

        private void InvokeDefaultBranchGridAction()
        {
            var selectdRow = branchGrid.SelectedRows[0].Index;
            var menu = BuildBranchActionMenuForRow(selectdRow);
            menu.Items[0].PerformClick();
        }

        private void branchGrid_DoubleClick(object sender, EventArgs e)
        {
            InvokeDefaultBranchGridAction();
        }
    }
}
