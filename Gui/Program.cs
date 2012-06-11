using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    /// <remarks>
    /// The single-instance feature was based on
    /// <see href="http://www.hanselman.com/blog/TheWeeklySourceCode31SingleInstanceWinFormsAndMicrosoftVisualBasicdll.aspx">
    /// The Weekly Source Code 31- Single Instance WinForms and Microsoft.VisualBasic.dll 
    /// </see>
    /// </remarks>
    class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var program = new Program();
            program.Run(Environment.GetCommandLineArgs());
        }

        public Program()
        {
            IsSingleInstance = true;
            StartupNextInstance += Program_StartupNextInstance;
        }

        void Program_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            e.BringToForeground = true;

            var form = (Main) MainForm;
            form.Start(e.CommandLine);
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new Main();
        }
    }
}