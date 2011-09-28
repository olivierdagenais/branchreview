using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
