using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// An implementation of <see cref="IShelvesetRepository"/> that does nothing useful.
    /// </summary>
    public class ShelvesetRepository : StubRepository, IShelvesetRepository
    {
        #region Implementation of IShelvesetRepository

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
        /// </list>
        /// </returns>
        public virtual DataTable LoadShelvesets()
        {
            return new DataTable();
        }

        /// <summary>
        /// Obtains the possible <see cref="MenuAction"/> instances that can be performed regardless of shelveset
        /// selection.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetShelvesetActions()
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Given a <paramref name="shelvesetId"/> representing the selected shelveset, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// The ID of the shelveset, as obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="IShelvesetRepository.LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetShelvesetActions(object shelvesetId)
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the changes associated with the context identified by
        /// <paramref name="shelvesetId"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="IShelvesetRepository.LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for the file or folder.</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable GetShelvesetChanges(object shelvesetId)
        {
            return new DataTable();
        }

        /// <summary>
        /// Retrieves the message (i.e. change log) associated with a shelveset, identified by
        /// <paramref name="shelvesetId"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="IShelvesetRepository.LoadShelvesets"/>.
        /// </param>
        /// 
        /// <returns>
        /// A string representation of the message for the specified shelveset.
        /// </returns>
        public virtual string GetShelvesetMessage(object shelvesetId)
        {
            return String.Empty;
        }

        /// <summary>
        /// Generates a text-based patch; the list of additions, modifications and removals to apply to each file
        /// represented by the <paramref name="changeIds"/>.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A shelveset ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="IShelvesetRepository.LoadShelvesets"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// A sequence of file/folder IDs obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned
        /// by calling <see cref="IShelvesetRepository.GetShelvesetChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// A single string representing the selected differences.
        /// </returns>
        public virtual string ComputeShelvesetDifferences(object shelvesetId, IEnumerable<object> changeIds)
        {
            return String.Empty;
        }

        /// <summary>
        /// Given a sequence of change IDs representing selected files and/or folders in the changes list,
        /// obtains the possible <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="shelvesetId">
        /// A revision ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="IShelvesetRepository.LoadShelvesets"/>.
        /// </param>
        /// 
        /// <param name="changeIds">
        /// One or more IDs representing the selected changes, as obtained from the <c>ID</c> column of the
        /// <see cref="DataTable"/> generated by calling <see cref="IShelvesetRepository.GetShelvesetChanges"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetActionsForShelvesetChanges(object shelvesetId, IEnumerable<object> changeIds)
        {
            return MenuAction.EmptyList;
        }

        #endregion
    }
}
