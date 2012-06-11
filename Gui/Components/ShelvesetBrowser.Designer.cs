namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class ShelvesetBrowser
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shelvesetGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid();
            ((System.ComponentModel.ISupportInitialize)(this.shelvesetGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // shelvesetGrid
            // 
            this.shelvesetGrid.Caption = "";
            this.shelvesetGrid.DataTable = null;
            this.shelvesetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shelvesetGrid.Filter = null;
            this.shelvesetGrid.Location = new System.Drawing.Point(0, 0);
            this.shelvesetGrid.Name = "shelvesetGrid";
            this.shelvesetGrid.Size = new System.Drawing.Size(776, 83);
            this.shelvesetGrid.TabIndex = 0;
            this.shelvesetGrid.Title = "Shelvesets";
            this.shelvesetGrid.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.ContextMenuNeededEventHandler(this.shelvesetGrid_ContextMenuNeeded);
            this.shelvesetGrid.RowInvoked += new System.EventHandler(this.shelvesetGrid_RowInvoked);
            // 
            // ShelvesetBrowser
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.shelvesetGrid);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "ShelvesetBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.shelvesetGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid shelvesetGrid;
    }
}