using System;
using System.Data;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class BranchBrowser : AbstractHistoryComponent
    {
        private readonly ChangeCommitter _changeCommitter;
        private readonly RevisionBrowser _revisionBrowser;

        public BranchBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository)
        {
            _revisionBrowser = new RevisionBrowser(_taskRepository, _sourceRepository, _shelvesetRepository);
            _changeCommitter = new ChangeCommitter(_taskRepository, _sourceRepository, _shelvesetRepository);

            InitializeComponent();
            branchGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        protected override Control ControlToFocus { get { return branchGrid; } }

        #region Common

        private void SetCurrentBranch(object branchId, object taskId)
        {
            var branchTitle = branchId.ToString();
            this.ToDo("Determine a better title than {0}", branchTitle);
            _revisionBrowser.Title = branchTitle;
            _revisionBrowser.BranchId = branchId;
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_revisionBrowser);
        }

        private void StartWorkOnBranch(object branchId, object taskId)
        {
            _changeCommitter.BranchId = branchId;
            _changeCommitter.Title = branchId.ToString();
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_changeCommitter);
        }

        private void SwitchCurrentTab(bool refresh)
        {
            if (branchGrid.DataTable == null || refresh)
            {
                this.StartTask(() => _sourceRepository.LoadBranches(), LoadDataTable);
            }
        }

        private void LoadDataTable(DataTable dataTable)
        {
            branchGrid.DataTable = dataTable;
            var branchCount = branchGrid.DataTable.Rows.Count;
            branchGrid.Caption = "{0} branch{1}".FormatInvariant(branchCount, branchCount == 1 ? "" : "es");
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

        public override string Title
        {
            get { return "Branches"; }
            set { throw new NotSupportedException(); }
        }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
