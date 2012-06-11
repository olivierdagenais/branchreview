using System;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class BranchBrowser : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;

        public BranchBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;

            InitializeComponent();
            branchGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        #region Common

        private void SetCurrentBranch(object branchId, object taskId)
        {
            var branchTitle = branchId.ToString();
            this.ToDo("Determine a better title than {0}", branchTitle);
            var revisionBrowser = 
                new RevisionBrowser(_taskRepository, _sourceRepository, _shelvesetRepository, branchId)
                {
                    Title = branchTitle,
                };
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(revisionBrowser);
        }

        private void StartWorkOnBranch(object branchId, object taskId)
        {
            this.ToDo("Create a ChangeInspector, initialize it for branch {0} in commit mode and push it", branchId);
        }

        private void SwitchCurrentTab(bool refresh)
        {
            if (branchGrid.DataTable == null || refresh)
            {
                branchGrid.DataTable = _sourceRepository.LoadBranches();
                var branchCount = branchGrid.DataTable.Rows.Count;
                branchGrid.Caption = "{0} branch{1}".FormatInvariant(branchCount, branchCount == 1 ? "" : "es");
            }												

            this.ExecuteLater(10, () => branchGrid.Focus());
        }

        #endregion

        #region Branches

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

        #endregion

        #region IHistoryItem Members

        IHistoryContainer IHistoryItem.Container { get; set; }

        public Control Control { get { return this; } }

        public string Title { get { return "Branches"; } }

        public void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
