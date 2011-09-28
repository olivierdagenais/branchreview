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
        ///     <item><term>BasePath</term><description>The location of the branch's working copy, if available.</description></item>
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

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the changes that have not yet been committed in the branch
        /// identified by <paramref name="branchId"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the file or folder</description></item>
        /// </list>
        /// </returns>
        DataTable GetPendingChanges(object branchId);

        /// <summary>
        /// Generates a text-based patch; the list of additions, modifications and removals to apply to each file
        /// represented by the <paramref name="pendingChangeIds"/>.
        /// </summary>
        /// 
        /// <param name="pendingChangeIds">
        /// A sequence of file/folder IDs obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned
        /// by calling <see cref="GetPendingChanges(object)"/>.
        /// </param>
        /// 
        /// <returns>
        /// A single string representing the selected pending differences to commit.
        /// </returns>
        string ComputeDifferences(IEnumerable<object> pendingChangeIds);
    }
}
