using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class ShelvesetInspector : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;
        private object _shelvesetId;

        public ShelvesetInspector
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;

            InitializeComponent();

            changeInspector.ChangeLog.IsReadOnly = true;
            changeInspector.ChangeLog.LongLines.EdgeMode = EdgeMode.None;
            changeInspector.ActionsForChangesFunction = _shelvesetRepository.GetActionsForShelvesetChanges;
            changeInspector.ChangesFunction = _shelvesetRepository.GetShelvesetChanges;
            changeInspector.ComputeDifferencesFunction = _shelvesetRepository.ComputeShelvesetDifferences;
            changeInspector.MessageFunction = _shelvesetRepository.GetShelvesetMessage;
        }

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
