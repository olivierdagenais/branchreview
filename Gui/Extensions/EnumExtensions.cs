using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class EnumExtensions
    {
        public static T Parse<T>(object value) where T : struct
        {
            var type = typeof (T);
            var stringValue = value.ToString();
            var rawResult = Enum.Parse(type, stringValue);
            var result = (T) rawResult;
            return result;
        }
    }
}
