using System;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class RevisionBrowser : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;
        private readonly object _branchId;

        public RevisionBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository,
            object branchId)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;
            _branchId = branchId;

            InitializeComponent();
            activityRevisions.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (activityRevisions.DataTable == null || refresh)
            {	
                activityRevisions.DataTable = _sourceRepository.LoadRevisions(_branchId);
                var revisionCount = activityRevisions.DataTable.Rows.Count;
                activityRevisions.Caption = "{0} revision{1}".FormatInvariant(revisionCount, revisionCount == 1 ? "" : "s");
            }												

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
            this.ToDo("Create a ChangeInspector, initialize it with {0} and associated call-backs, push", revisionId);
            this.ToDo("Determine the revision name from {0} to initialize ChangeInspector.Title with", revisionId);
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

        IHistoryContainer IHistoryItem.Container { get; set; }

        public Control Control { get { return this; } }

        public string Title { get; set; }

        public void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
