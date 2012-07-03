using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            changeInspector.ActionsForChangesFunction = GetActionsForPendingChanges;
            changeInspector.ChangesFunction = _sourceRepository.GetPendingChanges;
            changeInspector.ComputeDifferencesFunction = _sourceRepository.ComputePendingDifferences;
            changeInspector.MessageFunction = null;
            changeInspector.ChangeLog.KeyDown += ChangeLog_KeyDown;
        }

        private IList<MenuAction> GetActionsForPendingChanges(object branchId, IEnumerable<object> pendingChangeIds)
        {
            var pendingChangeIdList = pendingChangeIds.ToList();
            var repositoryActions = _sourceRepository.GetActionsForPendingChanges(branchId, pendingChangeIdList);
            var actions = new List<MenuAction>(repositoryActions)
            {
                new MenuAction("history", "&History", pendingChangeIdList.Count > 0, 
                    () => LaunchHistory(pendingChangeIdList)),
            };
            return actions;
        }

        private void LaunchHistory(IList<object> pendingChangeIds)
        {
            if (pendingChangeIds.Count < 1)
            {
                return;
            }

            var historyWindow = (HistoryWindow) this.FindForm();
            if (historyWindow != null)
            {
                var ancestor = (TabbedMain) historyWindow.ParentForm;
                if (ancestor != null)
                {
                    foreach (var pendingChangeId in pendingChangeIds)
                    {
                        var id = pendingChangeId;
                        ancestor.AddComponent((tr, sor, shr) => 
                        {
                            var result = new RevisionBrowser(tr, sor, shr)
                            {
                                BranchId = id,
                                Title = id.ToString(),
                            };
                            return result;
                        });
                    }
                }
            }
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
