using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class DataColumnExtensions
    {
        public static bool IsVisible(this DataColumn dataColumn)
        {
            return dataColumn.ExtendedProperties.GetBooleanPropertyValue("Visible");
        }

        public static bool IsSearchable(this DataColumn dataColumn)
        {
            return dataColumn.ExtendedProperties.GetBooleanPropertyValue("Searchable");
        }
    }
}
