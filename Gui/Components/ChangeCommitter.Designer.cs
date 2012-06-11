namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class ChangeCommitter
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.changeInspector = new SoftwareNinjas.BranchAndReviewTools.Gui.ChangeInspector();
            this.SuspendLayout();
            // 
            // changeInspector
            // 
            this.changeInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeInspector.Location = new System.Drawing.Point(0, 0);
            this.changeInspector.Name = "changeInspector";
            this.changeInspector.Size = new System.Drawing.Size(776, 87);
            this.changeInspector.TabIndex = 0;
            this.changeInspector.Title = "Changes";
            // 
            // ChangeCommitter
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.changeInspector);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "ChangeCommitter";
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.ChangeInspector changeInspector;
    }
}