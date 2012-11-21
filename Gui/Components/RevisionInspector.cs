using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class RevisionInspector : AbstractHistoryComponent
    {
        private object _revisionId;

        public RevisionInspector
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            InitializeComponent();

            changeInspector.ChangeLog.IsReadOnly = true;
            changeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
            changeInspector.ActionsForChangesFunction = (branchId, changeIds) => 
                GetActionsForChanges(branchId, changeIds, _sourceRepository.GetActionsForRevisionChanges);
            changeInspector.ChangesFunction = _sourceRepository.GetRevisionChanges;
            changeInspector.ComputeDifferencesFunction = _sourceRepository.ComputeRevisionDifferences;
            changeInspector.MessageFunction = _sourceRepository.GetRevisionMessage;
        }

        protected override Control ControlToFocus { get { return changeInspector; } }

        public object RevisionId
        {
            get { return _revisionId; }
            set
            {
                _revisionId = value;
                this.ExecuteLater(10, () => SwitchCurrentTab(true));
            }
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (changeInspector.Context == null || refresh)
            {
                changeInspector.Context = _revisionId;
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
