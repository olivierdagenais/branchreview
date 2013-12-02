using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// A grouping of projects, often maps to a user account on a server,
    /// although could also represent local clones of remote repositories.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// The name of the service, which could include the user name to help disambiguate instances.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Obtains the possible menu actions that can be performed on the project service.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        /// 
        /// <remarks>
        /// Examples of actions exposed by a project service:
        /// Add Project, Configure, Refresh, Toggle Online Status, Delete, etc.
        /// </remarks>
        IList<MenuAction> GetActions();

        /// <summary>
        /// Retrieves the projects available from this project service.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="IProject"/> instances.
        /// </returns>
        IList<IProject> GetProjects(); 

        // TODO: argument could probably be generalized to any string (a UUID, a short name, etc.),
        // which would also allow partial matching.

        /// <summary>
        /// Adds a project by a path or URL (such as one that was dragged-and-dropped onto the GUI),
        /// which was previously indicated as supported by <see cref="SupportsProject(string)"/>.
        /// </summary>
        /// 
        /// <param name="pathOrUrl">
        /// A string that represents a location hint for a project, such as a file system path or a URL.
        /// </param>
        /// 
        /// <returns>
        /// <see langword="true"/> if the project service was able to add a <see cref="IProject"/>
        /// from the supplied <paramref name="pathOrUrl"/>.
        /// </returns>
        bool AddProject(string pathOrUrl);

        /// <summary>
        /// Determines whether a path or URL (such as one that was dragged-and-dropped onto the GUI)
        /// represents a project compatible with this project service.
        /// </summary>
        /// 
        /// <param name="pathOrUrl">
        /// A string that represents a location hint for a project, such as a file system path or a URL.
        /// </param>
        /// 
        /// <returns>
        /// <see langword="true"/> if the project service understands the <paramref name="pathOrUrl"/>
        /// enough to create an <see cref="IProject"/> out of it.
        /// </returns>
        bool SupportsProject(string pathOrUrl);
    }
}
