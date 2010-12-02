using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Commit : Form
    {
        // TODO: move to TFS plug-in
        private static readonly string PathToVisualStudio =
            @"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\";
            // TODO: the following does not work with a 64-bit process (???)
            // (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\10.0", "InstallDir", null);
        private static readonly string PathToTfExe =
            Path.Combine(PathToVisualStudio, "tf.exe");

        private readonly string _workingFolder;
        public string WorkingFolder
        {
            get
            {
                return _workingFolder;
            }
        }

        public Commit(string workingFolder)
        {
            _workingFolder = workingFolder;

            InitializeComponent();

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = "Commit - " + _workingFolder;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            changeLog.Text = "";
        }

        // TODO: TFS-specific, move to plug-in
        internal static string LoadDiff(string currentDirectory)
        {
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    RedirectStandardOutput = true,
                    FileName = PathToTfExe,
                    Arguments = "diff",
                    WorkingDirectory = currentDirectory,
                }
            };
            p.Start();
            var result = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return result;
        }

        private void Commit_Load(object sender, System.EventArgs e)
        {
            patchText.Text = LoadDiff(_workingFolder);
        }
    }
}