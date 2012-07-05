using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.Grids;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class ShelvesetBrowser : AbstractHistoryComponent
    {
        private readonly ShelvesetInspector _shelvesetInspector;

        public ShelvesetBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository)
        {
            _shelvesetInspector = new ShelvesetInspector(taskRepository, sourceRepository, shelvesetRepository);

            InitializeComponent();
            shelvesetGrid.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        protected override Control ControlToFocus { get { return shelvesetGrid; } }
        
        #region Common

        private void SetCurrentShelveset(object shelvesetId, string shelvesetName)
        {
            _shelvesetInspector.ShelvesetId = shelvesetId;
            _shelvesetInspector.Title = shelvesetName;
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_shelvesetInspector);
        }

        private void SwitchCurrentTab(bool refresh)
        {
            if (shelvesetGrid.DataTable == null || refresh)
            {
                this.StartTask(() => _shelvesetRepository.LoadShelvesets(), LoadDataTable);
            }
        }

        private void LoadDataTable(Task<DataTable> task)
        {
            if (!task.IsFaulted)
            {
                shelvesetGrid.DataTable = task.Result;
                var shelvesetCount = shelvesetGrid.DataTable.Rows.Count;
                shelvesetGrid.Caption = "{0} shelveset{1}".FormatInvariant(shelvesetCount, shelvesetCount == 1 ? "" : "s");
                this.ExecuteLater(10, () => shelvesetGrid.Focus());
            }
        }

        #endregion

        #region Shelvesets

        private void AddShelvesetSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = shelvesetGrid.Grid.SelectedRows;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedItems[0].DataRow;
                var shelvesetId = row["ID"];
                var shelvesetName = (string) row["Name"];
                var builtInActions = new[]
                {
                    new MenuAction("defaultInspect", "&Inspect", true,
                                   () => SetCurrentShelveset(shelvesetId, shelvesetName) ),
                };
                items.AddActions(builtInActions);
                var specificActions = _shelvesetRepository.GetShelvesetActions(shelvesetId);
                if (specificActions.Count > 0)
                {
                    items.AddSeparator();
                    items.AddActions(specificActions);
                }
            }
        }

        private void shelvesetGrid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildShelvesetActionMenu();
            e.ContextMenu = menu;
        }

        private void shelvesetGrid_RowInvoked(object sender, EventArgs e)
        {
            InvokeDefaultShelvesetGridAction();
        }

        private ContextMenu BuildShelvesetActionMenu()
        {
            var menu = new ContextMenu();
            AddShelvesetSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void InvokeDefaultShelvesetGridAction()
        {
            var menu = BuildShelvesetActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        #endregion

        #region IHistoryItem Members

        public override string Title
        {
            get { return "Shelvesets"; } 
            set { throw new NotSupportedException(); }
        }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
