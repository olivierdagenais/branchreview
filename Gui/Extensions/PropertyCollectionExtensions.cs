using System;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class PropertyCollectionExtensions
    {
        internal static bool GetBooleanPropertyValue(this PropertyCollection props, string property)
        {
            var result = true;
            if (props.ContainsKey(property))
            {
                var value = props[property];
                try
                {
                    result = Convert.ToBoolean(value);
                }
                catch (FormatException)
                {
                    // ignore
                }
                catch (InvalidCastException)
                {
                    // ignore
                }
            }
            return result;
        }
    }
}
