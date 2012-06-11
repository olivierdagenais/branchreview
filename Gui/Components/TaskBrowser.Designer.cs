namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class TaskBrowser
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.taskGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // taskGrid
            // 
            this.taskGrid.Caption = "";
            this.taskGrid.DataTable = null;
            this.taskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskGrid.Filter = null;
            this.taskGrid.Location = new System.Drawing.Point(3, 3);
            this.taskGrid.Name = "taskGrid";
            this.taskGrid.Size = new System.Drawing.Size(778, 495);
            this.taskGrid.TabIndex = 0;
            this.taskGrid.Title = "Tasks";
            this.taskGrid.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.Grids.ContextMenuNeededEventHandler(this.taskGrid_ContextMenuNeeded);
            this.taskGrid.RowInvoked += new System.EventHandler(this.taskGrid_RowInvoked);
            // 
            // TaskBrowser
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.taskGrid);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "TaskBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.Grids.AwesomeGrid taskGrid;
    }
}