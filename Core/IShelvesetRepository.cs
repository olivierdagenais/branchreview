using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents a system that keeps track of shelved changes.
    /// </summary>
    public interface IShelvesetRepository : IRepository
    {
        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the available shelvesets.  Pre-filtering is assumed to have
        /// taken place.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for shelvesets.</description></item>
        ///     <item><term>Name</term><description>The display name of the shelveset.</description></item>
        /// </list>
        /// </returns>
        DataTable LoadShelvesets();

        /// <summary>
        /// Obtains the possible <see cref="MenuAction"/> instances that can be performed regardless of shelveset
        /// selection.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        IList<MenuAction> GetShelvesetActions();

        /// <summary>
        /// Given a <paramref name="shelvesetId"/> representing the selected shelveset, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// The ID of the shelveset, as obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        IList<MenuAction> GetShelvesetActions(object shelvesetId);

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the changes associated with the context identified by
        /// <paramref name="shelvesetId"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the file or folder.</description></item>
        /// </list>
        /// </returns>
        DataTable GetShelvesetChanges(object shelvesetId);

        /// <summary>
        /// Retrieves the message (i.e. change log) associated with a shelveset, identified by
        /// <paramref name="shelvesetId"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// A string representation of the message for the specified shelveset.
        /// </returns>
        string GetShelvesetMessage(object shelvesetId);

        /// <summary>
        /// Generates a text-based patch; the list of additions, modifications and removals to apply to each file
        /// represented by the <paramref name="changeIds"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadShelvesets"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// A sequence of file/folder IDs obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned
        /// by calling <see cref="GetShelvesetChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// A single string representing the selected differences.
        /// </returns>
        string ComputeShelvesetDifferences(object shelvesetId, IEnumerable<object> changeIds);

        /// <summary>
        /// Given a sequence of change IDs representing selected files and/or folders in the changes list,
        /// obtains the possible <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadShelvesets"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// One or more IDs representing the selected changes, as obtained from the <c>ID</c> column of the
        /// <see cref="DataTable"/> generated by calling <see cref="GetShelvesetChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        IList<MenuAction> GetActionsForShelvesetChanges(object shelvesetId, IEnumerable<object> changeIds);

    }
}
