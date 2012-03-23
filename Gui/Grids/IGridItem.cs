using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Grids
{
    public interface IGridItem
    {
        DataRow DataRow { get; }
        bool Selected { get; set; }
    }
}
