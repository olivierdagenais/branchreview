using SoftwareNinjas.BranchAndReviewTools.Gui.History;
using WeifenLuo.WinFormsUI.Docking;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class HistoryWindow : DockContent
    {
        public HistoryWindow(IHistoryItem historyItem)
        {
            InitializeComponent();
            historyContainer.Navigated += historyContainer_Navigated;
            historyContainer.Push(historyItem);
        }

        void historyContainer_Navigated(object sender, System.EventArgs e)
        {
            this.Text = historyContainer.Current.Title;
        }
    }
}
