namespace SoftwareNinjas.BranchAndReviewTools.Gui.Grids
{
    public interface IGridColumn
    {
        string Name { get; }
        string Caption { get; }
        bool Visible { get; }
    }
}