namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class BuildBrowser
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.builds = new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid();
            ((System.ComponentModel.ISupportInitialize)(this.builds)).BeginInit();
            this.SuspendLayout();
            // 
            // builds
            // 
            this.builds.Caption = "";
            this.builds.DataTable = null;
            this.builds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.builds.Filter = null;
            this.builds.Location = new System.Drawing.Point(0, 0);
            this.builds.Name = "builds";
            this.builds.Size = new System.Drawing.Size(776, 83);
            this.builds.TabIndex = 0;
            this.builds.Title = "Builds";
            this.builds.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.ContextMenuNeededEventHandler(this.builds_ContextMenuNeeded);
            this.builds.RowInvoked += new System.EventHandler(this.builds_RowInvoked);
            // 
            // BuildBrowser
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.builds);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "BuildBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.builds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid builds;
    }
}