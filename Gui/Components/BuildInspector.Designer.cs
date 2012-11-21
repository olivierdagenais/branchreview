namespace SoftwareNinjas.BranchAndReviewTools.Gui.Components
{
    partial class BuildInspector
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logViewer = new ScintillaNet.Scintilla();
            ((System.ComponentModel.ISupportInitialize)(this.logViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // logViewer
            // 
            this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewer.Location = new System.Drawing.Point(0, 0);
            this.logViewer.Name = "logViewer";
            this.logViewer.Size = new System.Drawing.Size(776, 87);
            this.logViewer.TabIndex = 0;
            // 
            // BuildInspector
            // 
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.logViewer);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "BuildInspector";
            ((System.ComponentModel.ISupportInitialize)(this.logViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNet.Scintilla logViewer;
    }
}