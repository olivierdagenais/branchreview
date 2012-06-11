using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class RevisionInspector : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;
        private object _revisionId;

        public RevisionInspector
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;

            InitializeComponent();

            changeInspector.ChangeLog.IsReadOnly = true;
            changeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
            changeInspector.ActionsForChangesFunction = _sourceRepository.GetActionsForRevisionChanges;
            changeInspector.ChangesFunction = _sourceRepository.GetRevisionChanges;
            changeInspector.ComputeDifferencesFunction = _sourceRepository.ComputeRevisionDifferences;
            changeInspector.MessageFunction = _sourceRepository.GetRevisionMessage;
        }

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
                changeInspector.Reload();
            }												

            this.ExecuteLater(10, () => changeInspector.Focus());
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
