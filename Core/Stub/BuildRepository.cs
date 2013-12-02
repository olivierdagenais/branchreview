using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// An implementation of <see cref="IBuildRepository"/> that does nothing useful.
    /// </summary>
    public class BuildRepository : StubRepository, IBuildRepository
    {
        /// <summary>
        /// Loads a <see cref="DataTable"/> representing the builds.  Pre-filtering is assumed to have
        /// taken place.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="DataTable"/> with at least the following columns:
        /// <list type="table">
        ///     <listheader><term>Column name</term><description>Description</description></listheader>
        ///     <item><term>ID</term><description>A unique identifier for builds.</description></item>
        ///     <item><term>Name</term><description>The display name of the build.</description></item>
        /// </list>
        /// </returns>
        public virtual DataTable LoadBuilds()
        {
            return new DataTable();
        }

        /// <summary>
        /// Given a <paramref name="buildId"/> representing the selected build, obtains the possible
        /// <see cref="MenuAction"/> instances that can be performed.
        /// </summary>
        /// 
        /// <param name="buildId">
        /// The ID of the build, as obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadBuilds"/>.
        /// </param>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        public virtual IList<MenuAction> GetBuildActions(object buildId)
        {
            return MenuAction.EmptyList;
        }

        /// <summary>
        /// Retrieves the log associated with a build, identified by
        /// <paramref name="buildId"/>.
        /// </summary>
        /// 
        /// <param name="buildId">
        /// A build ID obtained from the <c>ID</c> column of the <see cref="DataTable"/> returned by
        /// <see cref="LoadBuilds"/>.
        /// </param>
        /// 
        /// <returns>
        /// A string representation of the log for the specified build.
        /// </returns>
        public virtual string GetBuildLog(object buildId)
        {
            return String.Empty;
        }
    }
}
