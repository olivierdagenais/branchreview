using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class PointExtensions
    {
        private static readonly Regex PointTemplate =
            new Regex(@"\{X=(?<x>-?\d+),Y=(?<y>-?\d+)\}", RegexOptions.Compiled);

        public static Point Parse(string pointString)
        {
            var result = Point.Empty;
            if (!String.IsNullOrEmpty(pointString))
            {
                var match = PointTemplate.Match(pointString);
                if (match.Success)
                {
                    var xString = match.Groups["x"].Value;
                    var yString = match.Groups["y"].Value;
                    var x = Convert.ToInt32(xString, 10);
                    var y = Convert.ToInt32(yString, 10);
                    result = new Point(x, y);
                }
            }
            return result;
        }
    }
}
