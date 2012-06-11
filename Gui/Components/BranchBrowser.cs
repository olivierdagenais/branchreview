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
        private readonly ChangeInspector _pendingChanges;

        public BranchBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;
            _pendingChanges = new ChangeInspector
            {
                ActionsForChangesFunction = _sourceRepository.GetActionsForPendingChanges,
                ChangesFunction = _sourceRepository.GetPendingChanges,
                ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences,
                MessageFunction = null,
            };
            _pendingChanges.ChangeLog.KeyDown += ChangeLog_KeyDown;

            InitializeComponent();
            branchGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        void ChangeLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode && e.Control)
            {
                DoCommit();
                e.SuppressKeyPress = true;
            }
        }

        private void DoCommit()
        {
            var message = _pendingChanges.ChangeLog.Text;
            _sourceRepository.Commit(_pendingChanges.Context, message);
            _pendingChanges.ChangeLog.Text = String.Empty;
            _pendingChanges.Reload();
        }

        #region Common

        private void SetCurrentBranch(object branchId, object taskId)
        {
            this.ToDo("Create a single instance of RevisionBrowser and re-use it with different context");
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
            _pendingChanges.Context = branchId;
            _pendingChanges.Title = branchId.ToString();
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_pendingChanges);
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
