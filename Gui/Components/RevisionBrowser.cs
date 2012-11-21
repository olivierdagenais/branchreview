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
    public partial class RevisionBrowser : AbstractHistoryComponent
    {
        private readonly RevisionInspector _revisionInspector;
        private object _branchId;

        public RevisionBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            _revisionInspector = new RevisionInspector(taskRepository, sourceRepository, shelvesetRepository, buildRepository);

            InitializeComponent();
            activityRevisions.Grid.MultiSelect = false;
        }

        protected override Control ControlToFocus { get { return activityRevisions; } }

        public object BranchId
        {
            get { return _branchId; }
            set 
            {
                _branchId = value;
                this.ExecuteLater(10, () => SwitchCurrentTab(true));
            }
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (activityRevisions.DataTable == null || refresh)
            {
                this.StartTask(() => _sourceRepository.LoadRevisions(_branchId), LoadDataTable);
            }
        }

        private void LoadDataTable(DataTable dataTable)
        {
            activityRevisions.DataTable = dataTable;
            var revisionCount = activityRevisions.DataTable.Rows.Count;
            activityRevisions.Caption = "{0} revision{1}".FormatInvariant(revisionCount, revisionCount == 1 ? "" : "s");
            this.ExecuteLater(10, () => activityRevisions.Focus());
        }

        #endregion

        #region Revisions

        private ContextMenu BuildRevisionActionMenu()
        {
            var menu = new ContextMenu();
            AddRevisionSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void AddRevisionSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = activityRevisions.Grid.SelectedRows;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedItems[0].DataRow;
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
            _revisionInspector.RevisionId = revisionId;
            _revisionInspector.Title = revisionId.ToString();
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_revisionInspector);
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

        #endregion

        #region IHistoryItem Members

        public override string Title { get; set; }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
