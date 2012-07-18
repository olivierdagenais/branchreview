using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class ChangeCommitter : AbstractHistoryComponent
    {
        private object _branchId;

        public ChangeCommitter
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository)
        {
            InitializeComponent();

            changeInspector.ActionsForChangesFunction = (branchId, changeIds) => 
                GetActionsForChanges(branchId, changeIds, _sourceRepository.GetActionsForPendingChanges);
            changeInspector.ChangesFunction = _sourceRepository.GetPendingChanges;
            changeInspector.ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences;
            changeInspector.MessageFunction = null;
            changeInspector.ChangeLog.KeyDown += ChangeLog_KeyDown;
        }

        protected override Control ControlToFocus { get { return changeInspector; } }

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
            this.StartTask(() => _sourceRepository.Commit(changeInspector.Context, message), PostCommit);
        }

        private void PostCommit(Task task)
        {
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
