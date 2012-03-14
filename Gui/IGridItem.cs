using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public interface IGridItem
    {
        DataRow DataRow { get; }
        bool Selected { get; set; }
    }
}
