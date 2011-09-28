using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents a system that keeps track of work items, cases, bugs, features, tasks, user stories, etc.
    /// </summary>
    public interface ITaskRepository
    {
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
        DataTable LoadTasks();
    }
}
