using System.Windows.Forms;
using ScintillaNet;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    public partial class BuildInspector : AbstractHistoryComponent
    {
        private object _buildId;

        public BuildInspector
        (ITaskRepository taskRepository, ISourceRepository sourceRepository, IShelvesetRepository shelvesetRepository, IBuildRepository buildRepository)
            : base(taskRepository, sourceRepository, shelvesetRepository, buildRepository)
        {
            InitializeComponent();
            logViewer.InitializeDefaults();
            logViewer.IsReadOnly = true;
            logViewer.LongLines.EdgeMode = EdgeMode.None;
        }

        protected override Control ControlToFocus { get { return logViewer; } }

        public object BuildId
        {
            get { return _buildId; }
            set
            {
                _buildId = value;
                this.ExecuteLater(10, () => SwitchCurrentTab(true));
            }
        }

        #region Common

        private void SwitchCurrentTab(bool refresh)
        {
            if (logViewer.Text == null || refresh)
            {
                this.StartTask(() => _buildRepository.GetBuildLog(_buildId), LoadBuildLog);
            }												
        }

        private void LoadBuildLog(string logText)
        {
            logViewer.SetReadOnlyText(logText);
            this.ExecuteLater(10, () => logViewer.Focus());
        }

        #endregion

        #region IHistoryItem Members

        public override string Title { get; set; }

        public override void Reload()
        {
            SwitchCurrentTab(true);
        }
        #endregion
    }
}
