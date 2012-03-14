namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public interface IGridColumn
    {
        string Name { get; }
        string Caption { get; }
        bool Visible { get; }
    }
}