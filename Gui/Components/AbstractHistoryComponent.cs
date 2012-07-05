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

        protected AbstractHistoryComponent
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository)
        {
            _taskRepository = taskRepository;
            _sourceRepository = sourceRepository;
            _shelvesetRepository = shelvesetRepository;
        }

        #region IHistoryItem Members

        IHistoryContainer IHistoryItem.Container { get; set; }

        Control IHistoryItem.Control { get { return this; } }

        public abstract void Reload();

        public abstract string Title { get; set; }

        #endregion
    }
}
