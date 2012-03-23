using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class SizeExtensions
    {
        private static readonly Regex SizeTemplate =
            new Regex(@"\{Width=(?<width>-?\d+),\s?Height=(?<height>-?\d+)\}", RegexOptions.Compiled);

        public static Size Parse(string sizeString)
        {
            var result = Size.Empty;
            if (!String.IsNullOrEmpty(sizeString))
            {
                var match = SizeTemplate.Match(sizeString);
                if (match.Success)
                {
                    var widthString = match.Groups["width"].Value;
                    var heightString = match.Groups["height"].Value;
                    var width = Convert.ToInt32(widthString, 10);
                    var height = Convert.ToInt32(heightString, 10);
                    result = new Size(width, height);
                }
            }
            return result;
        }
    }
}
