using System;
using System.Diagnostics;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Extensions for instances of type <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// A description of work still to be done, which will be emitted to the Debug Listeners.
        /// </summary>
        /// 
        /// <param name="o">
        /// Unimportant parameter, used for the extension method.
        /// </param>
        /// 
        /// <param name="message">
        /// The contents of the TODO itself, optionally formatted with arguments.
        /// </param>
        /// 
        /// <param name="args">
        /// An object array containing zero or more objects to format.
        /// </param>
        public static void ToDo(this object o, string message, params object[] args)
        {
            var line = String.Format(message, args);
            Debug.WriteLine(line, "TODO");
        }
    }
}
