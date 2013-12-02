using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// The collection of source code (and related resources) required to build and release an artifact.
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Obtains the possible menu actions that can be performed on the project.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        /// 
        /// <remarks>
        /// Examples of actions exposed by a project:
        /// Configure, Refresh, Delete, Open website, etc.
        /// </remarks>
        IList<MenuAction> GetActions();

        /// <summary>
        /// Obtains the available repositories that can be performed on the project.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        /// 
        /// <remarks>
        /// Examples of actions exposed by a project:
        /// Configure, Refresh, Delete, Open website, etc.
        /// </remarks>
        IList<IRepository> GetRepositories();
    }
}
