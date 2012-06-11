namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class BranchBrowser
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.branchGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid();
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // branchGrid
            // 
            this.branchGrid.Caption = "";
            this.branchGrid.DataTable = null;
            this.branchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.branchGrid.Filter = null;
            this.branchGrid.Location = new System.Drawing.Point(0, 0);
            this.branchGrid.Name = "branchGrid";
            this.branchGrid.Size = new System.Drawing.Size(776, 87);
            this.branchGrid.TabIndex = 0;
            this.branchGrid.Title = "Branches";
            this.branchGrid.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.ContextMenuNeededEventHandler(this.branchGrid_ContextMenuNeeded);
            this.branchGrid.RowInvoked += new System.EventHandler(this.branchGrid_RowInvoked);
            // 
            // BranchBrowser
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.branchGrid);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "BranchBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid branchGrid;
    }
}