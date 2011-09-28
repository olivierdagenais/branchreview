using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;

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

        public Func<object, DataTable> PendingChangesFunction { get; set; }
        public Func<IEnumerable<object>, string> ComputeDifferencesFunction { get; set; }
        public Func<IEnumerable<object>, IList<MenuAction>> ActionsForPendingChangesFunction { get; set; }
    }
}
