using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents a system that keeps track of files, folders and their versions.
    /// </summary>
    public interface ISourceRepository
    {
        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the available branches.  Pre-filtering is assumed to have
        /// taken place.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for branches</description></item>
        ///     <item><term>TaskID</term><description>The ID of the associated task.</description></item>
        /// </list>
        /// </returns>
        DataTable LoadBranches();
    }
}
