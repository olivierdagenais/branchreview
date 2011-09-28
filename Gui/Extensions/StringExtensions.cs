using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsInvariantIgnoreCase(this string hayStack, string needle)
        {
            return hayStack.IndexOf (needle, StringComparison.InvariantCultureIgnoreCase) != -1;
        }
    }
}
