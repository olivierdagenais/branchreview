using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// An implementation of <see cref="ISourceRepository"/> that does nothing useful.
    /// </summary>
    public class SourceRepository : ISourceRepository
    {
        /// <summary>
        /// Gets or sets the <see cref="ILog"/> instance used for reporting status and progress.
        /// </summary>
        public virtual ILog Log { get; set; }

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
        public virtual DataTable LoadBranches()
        {
            return new DataTable();
        }

        /// <summary>
        /// Obtains the possible <see cref="MenuAction"/> instances that can be performed regardless of branch
        /// selection.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetBranchActions()
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Given a <paramref name="branchId"/> representing the selected branch, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of the branch, as obtained from the <c>ID</c> column of the <see cref="DataTable"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetBranchActions(object branchId)
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Creates a branch associated with the specified <paramref name="taskId"/>, prompting the user for more
        /// information if necessary.
        /// </summary>
        /// 
        /// <param name="taskId">
        /// The ID of the task with which the new branch is to be associated.
        /// </param>
        public virtual void CreateBranch(object taskId)
        {
        }

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the changes that have not yet been committed in the branch
        /// identified by <paramref name="branchId"/>.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of a branch, obtained from the <c>ID</c> column of a table returned by <see cref="ISourceRepository.LoadBranches"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the file or folder</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable GetPendingChanges(object branchId)
        {
            return new DataTable();
        }

        /// <summary>
        /// Generates a text-based patch; the list of additions, modifications and removals to apply to each file
        /// represented by the <paramref name="pendingChangeIds"/>.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of a branch, obtained from the <c>ID</c> column of a table returned by <see cref="LoadBranches"/>.
        /// </param>
        /// 
        /// <param name="pendingChangeIds">
        /// A sequence of file/folder IDs obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned
        /// by calling <see cref="ISourceRepository.GetPendingChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// A single string representing the selected pending differences to commit.
        /// </returns>
        public virtual string ComputePendingDifferences(object branchId, IEnumerable<object> pendingChangeIds)
        {
            return String.Empty;
        }

        /// <summary>
        /// Given a sequence of change IDs representing selected files and/or folders in the pending changes list,
        /// obtains the possible <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of a branch, obtained from the <c>ID</c> column of a table returned by <see cref="LoadBranches"/>.
        /// </param>
        /// 
        /// <param name="pendingChangeIds">
        /// One or more IDs representing the selected changes, as obtained from the <c>ID</c> column of the
        /// <see cref="DataTable"/> generated by calling <see cref="ISourceRepository.GetPendingChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetActionsForPendingChanges(object branchId, IEnumerable<object> pendingChangeIds)
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Records all the pending changes in the branch specified by <paramref name="branchId"/>, along with the
        /// specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of the branch which identifies the pending changes to submit, as obtained from the <c>ID</c> column
        /// in the <see cref="DataTable"/> returned from <see cref="ISourceRepository.LoadBranches"/>.
        /// </param>
        /// 
        /// <param name="message">
        /// The user-supplied notes that explain the changes, mostly why they were done.
        /// </param>
        public virtual void Commit(object branchId, string message)
        {
        }

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the revisions associated with the project, in both the
        /// mainline and its various branches.
        /// </summary>
        /// 
        /// <param name="branchId">
        /// The ID of the branch for which revisions are to be loaded, as obtained from the <c>ID</c> column in the
        /// <see cref="DataTable"/> returned from <see cref="ISourceRepository.LoadBranches"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the revision</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable LoadRevisions(object branchId)
        {
            return new DataTable();
        }

        /// <summary>
        /// Given a <paramref name="revisionId"/> representing the selected revision, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="revisionId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadRevisions"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetRevisionActions(object revisionId)
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the changes associated with the context identified by
        /// <paramref name="revisionId"/>.
        /// </summary>
        /// 
        /// <param name="revisionId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="ISourceRepository.LoadRevisions"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the file or folder</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable GetRevisionChanges(object revisionId)
        {
            return new DataTable();
        }

        /// <summary>
        /// Retrieves the message (i.e. change log) associated with a revision, identified by
        /// <paramref name="revisionId"/>.
        /// </summary>
        /// 
        /// <param name="revisionId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="ISourceRepository.LoadRevisions"/>.
        /// </param>
        /// 
        /// <returns>
        /// A string representation of the message for the specified revision.
        /// </returns>
        public virtual string GetRevisionMessage(object revisionId)
        {
            return String.Empty;
        }

        /// <summary>
        /// Generates a text-based patch; the list of additions, modifications and removals to apply to each file
        /// represented by the <paramref name="changeIds"/>.
        /// </summary>
        /// 
        /// <param name="revisionId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadRevisions"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// A sequence of file/folder IDs obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned
        /// by calling <see cref="ISourceRepository.GetRevisionChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// A single string representing the selected pending differences to commit.
        /// </returns>
        public virtual string ComputeRevisionDifferences(object revisionId, IEnumerable<object> changeIds)
        {
            return String.Empty;
        }

        /// <summary>
        /// Given a sequence of change IDs representing selected files and/or folders in the changes list,
        /// obtains the possible <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="revisionId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadRevisions"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// One or more IDs representing the selected changes, as obtained from the <c>ID</c> column of the
        /// <see cref="DataTable"/> generated by calling <see cref="ISourceRepository.GetRevisionChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetActionsForRevisionChanges(object revisionId, IEnumerable<object> changeIds)
        {
            return MenuAction.EmptyList;
        }
    }
}
