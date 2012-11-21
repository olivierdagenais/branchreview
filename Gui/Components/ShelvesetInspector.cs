using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class ShelvesetInspector : AbstractHistoryComponent
    {
        private object _shelvesetId;

        public ShelvesetInspector
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            InitializeComponent();

            changeInspector.ChangeLog.IsReadOnly = true;
            changeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
            changeInspector.ActionsForChangesFunction = (branchId, changeIds) => 
                GetActionsForChanges(branchId, changeIds, _shelvesetRepository.GetActionsForShelvesetChanges);
            changeInspector.ChangesFunction = _shelvesetRepository.GetShelvesetChanges;
            changeInspector.ComputeDifferencesFunction = _shelvesetRepository.ComputeShelvesetDifferences;
            changeInspector.MessageFunction = _shelvesetRepository.GetShelvesetMessage;
        }

        protected override Control ControlToFocus { get { return changeInspector; } }

        public object ShelvesetId
        {
            get { return _shelvesetId; }
            set
            {
                _shelvesetId = value;
                this.ExecuteLater(10, () => SwitchCurrentTab(true));
            }
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (changeInspector.Context == null || refresh)
            {
                changeInspector.Context = _shelvesetId;
            }												

            this.ExecuteLater(10, () => changeInspector.Focus());
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
