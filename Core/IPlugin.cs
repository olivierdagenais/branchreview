using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// A Branch And Review Tools accessory that exposes services.
    /// </summary>
    /// 
    /// <remarks>
    /// Every BART plug-in DLL is expected to expose a single implementation of <see cref="IPlugin"/>.
    /// </remarks>
    public interface IPlugin
    {
        /// <summary>
        /// Obtains the possible menu actions that can be performed on the plugin.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list of <see cref="MenuAction"/> instances from which to build a menu.
        /// </returns>
        /// 
        /// <remarks>
        /// Examples of actions exposed by a plugin:
        /// Configure, Refresh, Register, etc.
        /// </remarks>
        IList<MenuAction> GetActions();

        /// <summary>
        /// Obtains the project services managed by this plugin.
        /// </summary>
        /// 
        /// <returns>
        /// An ordered list <see cref="IProjectService"/> instances from which the user can get to projects.
        /// </returns>
        IList<IProjectService> GetProjectServices();
    }
}
