namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class RevisionBrowser
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.activityRevisions = new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid();
            ((System.ComponentModel.ISupportInitialize)(this.activityRevisions)).BeginInit();
            this.SuspendLayout();
            // 
            // activityRevisions
            // 
            this.activityRevisions.Caption = "";
            this.activityRevisions.DataTable = null;
            this.activityRevisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityRevisions.Filter = null;
            this.activityRevisions.Location = new System.Drawing.Point(0, 0);
            this.activityRevisions.Name = "activityRevisions";
            this.activityRevisions.Size = new System.Drawing.Size(776, 83);
            this.activityRevisions.TabIndex = 0;
            this.activityRevisions.Title = "Revisions";
            this.activityRevisions.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.ContextMenuNeededEventHandler(this.activityRevisions_ContextMenuNeeded);
            this.activityRevisions.RowInvoked += new System.EventHandler(this.activityRevisions_RowInvoked);
            // 
            // RevisionBrowser
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.activityRevisions);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "RevisionBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.activityRevisions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid activityRevisions;
    }
}