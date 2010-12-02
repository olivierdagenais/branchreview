using System;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var commit = new Commit
            {
                Text = "Commit - " + Environment.CurrentDirectory
            };
            Application.Run(commit);
        }
    }
}