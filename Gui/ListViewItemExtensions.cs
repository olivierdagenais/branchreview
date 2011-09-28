using System.Data;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public static class ListViewItemExtensions
    {
        public static DataRow GetRow(this ListViewItem item)
        {
            return (DataRow) item.Tag;
        }
    }
}
