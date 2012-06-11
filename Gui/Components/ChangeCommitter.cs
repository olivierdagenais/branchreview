using System;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class ChangeCommitter : Control, IHistoryItem
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly IShelvesetRepository _shelvesetRepository;
        private object _branchId;

        public ChangeCommitter
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;

            InitializeComponent();

            changeInspector.ActionsForChangesFunction = _sourceRepository.GetActionsForPendingChanges;
            changeInspector.ChangesFunction = _sourceRepository.GetPendingChanges;
            changeInspector.ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences;
            changeInspector.MessageFunction = null;
            changeInspector.ChangeLog.KeyDown += ChangeLog_KeyDown;
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
            var message = changeInspector.ChangeLog.Text;
            _sourceRepository.Commit(changeInspector.Context, message);
            changeInspector.ChangeLog.Text = String.Empty;
            changeInspector.Reload();
        }

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
            if (changeInspector.Context == null || refresh)
            {
                changeInspector.Context = _branchId;
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
