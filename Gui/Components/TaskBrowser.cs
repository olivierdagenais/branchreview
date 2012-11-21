using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class TaskBrowser : AbstractHistoryComponent
    {
        public TaskBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            InitializeComponent();
            taskGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        protected override Control ControlToFocus { get { return taskGrid; } }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (taskGrid.DataTable == null || refresh)
            {
                this.StartTask(() => _taskRepository.LoadTasks(), LoadDataTable);
            }
        }

        private void LoadDataTable(DataTable dataTable)
        {
            taskGrid.DataTable = dataTable;
            var taskCount = taskGrid.DataTable.Rows.Count;
            taskGrid.Caption = "{0} task{1}".FormatInvariant(taskCount, taskCount == 1 ? "" : "s");
            this.ExecuteLater(10, () => taskGrid.Focus());
        }

        #endregion

        #region Tasks

        private void AddTaskSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var taskId = taskGrid.FindSelectedId();
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
            var branches = _sourceRepository.LoadBranches();
            var dataRow = branches.FindFirst("TaskID", taskId);
            this.ToDo("create BranchBrowser, initialize it with {0} and push it to the container", dataRow["ID"]);
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

        #region IHistoryItem Members

        public override string Title
        {
            get { return "Tasks"; } 
            set { throw new NotSupportedException(); }
        }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
