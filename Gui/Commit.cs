using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Commit : Form
    {
        private readonly string _path;
        public string Path
        {
            get
            {
                return _path;
            }
        }

        public Commit(string path)
        {
            _path = path;

            InitializeComponent();

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = "Commit - " + _path;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            changeLog.Text = "";
            patchText.Text = "TODO: load process output of DIFF";
        }
    }
}