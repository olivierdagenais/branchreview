using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.History
{
    public interface IHistoryItem
    {
        IHistoryContainer Container { get; set; }
        Control Control { get; }
        string Title { get; }
    }
}
