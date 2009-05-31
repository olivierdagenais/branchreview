using System;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents all the sub-commands that can be invoked as a consumer of Subversion services.
    /// </summary>
    public interface ISubversionClient
    {
        /// <summary>
        /// Retrieves information about a local item.
        /// </summary>
        /// 
        /// <param name="workingCopyPath">
        /// The path to an item in a working copy.
        /// </param>
        /// 
        /// <returns>
        /// An <see cref="SvnInfo"/> structure representing the information discovered.
        /// </returns>
        SvnInfo Info(string workingCopyPath);
    }
}
