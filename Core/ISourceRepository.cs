using System.Collections.Generic;
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

        /// <summary>
        /// Given a <paramref name="branchId"/> representing the selected branch (or <see langword="null" /> if no
        /// task was selected), obtains the possible <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of the branch, as obtained from the <c>ID</c> column of the <see cref="DataTable"/>;
        /// or <see langword="null" /> if no branch was selected.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        IList<MenuAction> GetActionsForBranch(object branchId);
    }
}
