using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public abstract class AbstractHistoryComponent : Control, IHistoryItem
    {
        protected readonly ITaskRepository _taskRepository;
        protected readonly ISourceRepository _sourceRepository;
        protected readonly IShelvesetRepository _shelvesetRepository;
        protected readonly IBuildRepository _buildRepository;

        protected AbstractHistoryComponent
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;
            _buildRepository = buildRepository;

            this.GotFocus += AbstractHistoryComponent_GotFocus;
        }

        void AbstractHistoryComponent_GotFocus(object sender, System.EventArgs e)
        {
            ControlToFocus.Focus();
        }

        protected abstract Control ControlToFocus { get; }

        #region IHistoryItem Members

        IHistoryContainer IHistoryItem.Container { get; set; }

        Control IHistoryItem.Control { get { return this; } }

        public abstract void Reload();

        public abstract string Title { get; set; }

        #endregion

        protected IList<MenuAction> GetActionsForChanges(object branchId, IEnumerable<object> changeIds,
            Func<object, IEnumerable<object>, IList<MenuAction>> repositoryActionsForChanges)
        {
            var changeIdList = changeIds.ToList();
            var repositoryActions = repositoryActionsForChanges(branchId, changeIdList);
            var actions = new List<MenuAction>(repositoryActions)
            {
                new MenuAction("history", "&History", changeIdList.Count > 0, 
                    () => LaunchHistory(changeIdList)),
            };
            return actions;
        }

        private void LaunchHistory(IList<object> changeIds)
        {
            if (changeIds.Count < 1)
            {
                return;
            }

            var historyWindow = (HistoryWindow) this.FindForm();
            if (historyWindow != null)
            {
                var ancestor = (TabbedMain) historyWindow.ParentForm;
                if (ancestor != null)
                {
                    foreach (var changeId in changeIds)
                    {
                        var id = changeId;
                        ancestor.AddComponent((tr, sor, shr, br) => 
                        {
                            var result = new RevisionBrowser(tr, sor, shr, br)
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
    }
}
