using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class TaskBrowser : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;

        public TaskBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;

            InitializeComponent();
            taskGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (taskGrid.DataTable == null || refresh)
            {
                this.StartTask(() => _taskRepository.LoadTasks(), LoadDataTable);
            }
        }

        private void LoadDataTable(Task<DataTable> task)
        {
            if (!task.IsFaulted)
            {
                taskGrid.DataTable = task.Result;
                var taskCount = taskGrid.DataTable.Rows.Count;
                taskGrid.Caption = "{0} task{1}".FormatInvariant(taskCount, taskCount == 1 ? "" : "s");
                this.ExecuteLater(10, () => taskGrid.Focus());
            }
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

        IHistoryContainer IHistoryItem.Container { get; set; }

        public Control Control { get { return this; } }

        public string Title { get { return "Tasks"; } }

        public void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
