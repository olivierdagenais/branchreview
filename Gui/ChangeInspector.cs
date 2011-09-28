using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class ChangeInspector : UserControl
    {
        public ChangeInspector()
        {
            InitializeComponent();
            ChangeLog.InitializeDefaults();
            PatchText.InitializeDefaults();
            PatchText.InitializeDiff();
        }
    }
}
