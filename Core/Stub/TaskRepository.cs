using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// An implementation of <see cref="ITaskRepository"/> that does nothing useful.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// Gets or sets the <see cref="ILog"/> instance used for reporting status and progress.
        /// </summary>
        public virtual ILog Log { get; set; }

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the available tasks.  Pre-filtering is assumed to have taken
        /// place.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for tasks</description></item>
        ///     <item><term>Title</term><description>A summary of the work to be performed</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable LoadTasks()
        {
            return new DataTable();
        }

        /// <summary>
        /// Obtains the possible <see cref="MenuAction"/> instances that can be performed regardless of task selection.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetTaskActions()
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Given a <paramref name="taskId"/> representing the selected task, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="taskId">
        /// The ID of the task, as obtained from the <c>ID</c> column of the <see cref="DataTable"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetTaskActions(object taskId)
        {
            return MenuAction.EmptyList;
        }
    }
}
