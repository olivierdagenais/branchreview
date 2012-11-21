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
    public partial class BuildBrowser : AbstractHistoryComponent
    {
        private readonly BuildInspector _buildInspector;

        public BuildBrowser
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            _buildInspector = new BuildInspector(taskRepository, sourceRepository, shelvesetRepository, buildRepository);

            InitializeComponent();
            builds.Grid.MultiSelect = false;
            this.ExecuteLater(10, () => SwitchCurrentTab(true));
        }

        protected override Control ControlToFocus { get { return builds; } }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (builds.DataTable == null || refresh)
            {
                this.StartTask(() => _buildRepository.LoadBuilds(), LoadDataTable);
            }
        }

        private void LoadDataTable(DataTable dataTable)
        {
            builds.DataTable = dataTable;
            var buildCount = builds.DataTable.Rows.Count;
            builds.Caption = "{0} build{1}".FormatInvariant(buildCount, buildCount == 1 ? "" : "s");
            this.ExecuteLater(10, () => builds.Focus());
        }

        #endregion

        #region Builds

        private ContextMenu BuildBuildActionMenu()
        {
            var menu = new ContextMenu();
            AddBuildSpecificActions(menu.MenuItems, false);
            return menu;
        }

        private void AddBuildSpecificActions(Menu.MenuItemCollection items, bool needsLeadingSeparator)
        {
            var selectedItems = builds.Grid.SelectedRows;
            if (selectedItems.Count > 0)
            {
                if (needsLeadingSeparator)
                {
                    items.AddSeparator();
                }
                var row = selectedItems[0].DataRow;
                var buildId = row["ID"];
                var buildName = row["Name"];
                var builtInActions = new[]
                {
                    new MenuAction("defaultOpen", "&Open", true,
                                () => SetCurrentBuild(buildId, buildName) ),
                };
                items.AddActions(builtInActions);
                var specificActions = _buildRepository.GetBuildActions(buildId);
                if (specificActions.Count > 0)
                {
                    items.AddSeparator();
                    items.AddActions(specificActions);
                }
            }
        }

        private void SetCurrentBuild(object buildId, object buildName)
        {
            _buildInspector.BuildId = buildId;
            _buildInspector.Title = buildName.ToString();
            var historyItem = (IHistoryItem) this;
            historyItem.Container.Push(_buildInspector);
        }

        private void builds_RowInvoked(object sender, EventArgs e)
        {
            var menu = BuildBuildActionMenu();
            menu.MenuItems.InvokeFirstMenuItem();
        }

        private void builds_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            var menu = BuildBuildActionMenu();
            e.ContextMenu = menu;
        }

        #endregion

        #region IHistoryItem Members

        public override string Title
        {
            get { return "Builds"; }
            set { throw new NotSupportedException(); }
        }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
