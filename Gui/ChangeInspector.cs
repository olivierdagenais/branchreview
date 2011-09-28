using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class ChangeInspector : UserControl
    {
        public ChangeInspector()
        {
            InitializeComponent();
            ChangeLog.InitializeDefaults();
            PatchViewer.InitializeDefaults();
            PatchViewer.InitializeDiff();
        }

        public string PatchText
        {
            get { return PatchViewer.Text; }
            set { PatchViewer.SetReadOnlyText(value); }
        }

        // TODO: GetPendingChanges
        // TODO: ComputeDifferences
        // TODO: GetActionsForPendingChanges
    }
}
