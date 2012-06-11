using System;
using System.Diagnostics;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class ObjectExtensions
    {
        public static void ToDo(this object o, string message, params object[] args)
        {
            var line = String.Format(message, args);
            Debug.WriteLine(line, "TODO");
        }
    }
}
