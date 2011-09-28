namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    partial class ChangeInspector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.horizontalDivider = new System.Windows.Forms.SplitContainer();
            this.ChangeLog = new ScintillaNet.Scintilla();
            this.verticalDivider = new System.Windows.Forms.SplitContainer();
            this.PatchText = new ScintillaNet.Scintilla();
            this.FileGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalDivider)).BeginInit();
            this.horizontalDivider.Panel1.SuspendLayout();
            this.horizontalDivider.Panel2.SuspendLayout();
            this.horizontalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChangeLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalDivider)).BeginInit();
            this.verticalDivider.Panel1.SuspendLayout();
            this.verticalDivider.Panel2.SuspendLayout();
            this.verticalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatchText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // horizontalDivider
            // 
            this.horizontalDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.horizontalDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontalDivider.Location = new System.Drawing.Point(0, 0);
            this.horizontalDivider.Name = "horizontalDivider";
            this.horizontalDivider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizontalDivider.Panel1
            // 
            this.horizontalDivider.Panel1.Controls.Add(this.ChangeLog);
            // 
            // horizontalDivider.Panel2
            // 
            this.horizontalDivider.Panel2.Controls.Add(this.verticalDivider);
            this.horizontalDivider.Size = new System.Drawing.Size(749, 556);
            this.horizontalDivider.SplitterDistance = 108;
            this.horizontalDivider.TabIndex = 1;
            this.horizontalDivider.TabStop = false;
            // 
            // ChangeLog
            // 
            this.ChangeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeLog.Location = new System.Drawing.Point(0, 0);
            this.ChangeLog.Name = "ChangeLog";
            this.ChangeLog.Size = new System.Drawing.Size(747, 106);
            this.ChangeLog.Styles.BraceBad.FontName = "Verdana";
            this.ChangeLog.Styles.BraceLight.FontName = "Verdana";
            this.ChangeLog.Styles.ControlChar.FontName = "Verdana";
            this.ChangeLog.Styles.Default.FontName = "Verdana";
            this.ChangeLog.Styles.IndentGuide.FontName = "Verdana";
            this.ChangeLog.Styles.LastPredefined.FontName = "Verdana";
            this.ChangeLog.Styles.LineNumber.FontName = "Verdana";
            this.ChangeLog.Styles.Max.FontName = "Verdana";
            this.ChangeLog.TabIndex = 0;
            // 
            // verticalDivider
            // 
            this.verticalDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.verticalDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalDivider.Location = new System.Drawing.Point(0, 0);
            this.verticalDivider.Name = "verticalDivider";
            // 
            // verticalDivider.Panel1
            // 
            this.verticalDivider.Panel1.Controls.Add(this.FileGrid);
            // 
            // verticalDivider.Panel2
            // 
            this.verticalDivider.Panel2.Controls.Add(this.PatchText);
            this.verticalDivider.Size = new System.Drawing.Size(749, 444);
            this.verticalDivider.SplitterDistance = 235;
            this.verticalDivider.SplitterWidth = 6;
            this.verticalDivider.TabIndex = 0;
            this.verticalDivider.TabStop = false;
            // 
            // PatchText
            // 
            this.PatchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PatchText.Location = new System.Drawing.Point(0, 0);
            this.PatchText.Name = "PatchText";
            this.PatchText.Size = new System.Drawing.Size(506, 442);
            this.PatchText.Styles.BraceBad.FontName = "Verdana";
            this.PatchText.Styles.BraceLight.FontName = "Verdana";
            this.PatchText.Styles.ControlChar.FontName = "Verdana";
            this.PatchText.Styles.Default.FontName = "Verdana";
            this.PatchText.Styles.IndentGuide.FontName = "Verdana";
            this.PatchText.Styles.LastPredefined.FontName = "Verdana";
            this.PatchText.Styles.LineNumber.FontName = "Verdana";
            this.PatchText.Styles.Max.FontName = "Verdana";
            this.PatchText.TabIndex = 0;
            // 
            // FileGrid
            // 
            this.FileGrid.DataTable = null;
            this.FileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileGrid.Filter = null;
            this.FileGrid.Location = new System.Drawing.Point(0, 0);
            this.FileGrid.Name = "FileGrid";
            this.FileGrid.RowTemplate.Height = 23;
            this.FileGrid.Size = new System.Drawing.Size(233, 442);
            this.FileGrid.TabIndex = 0;
            // 
            // ChangeInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.horizontalDivider);
            this.Name = "ChangeInspector";
            this.Size = new System.Drawing.Size(749, 556);
            this.horizontalDivider.Panel1.ResumeLayout(false);
            this.horizontalDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horizontalDivider)).EndInit();
            this.horizontalDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChangeLog)).EndInit();
            this.verticalDivider.Panel1.ResumeLayout(false);
            this.verticalDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.verticalDivider)).EndInit();
            this.verticalDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PatchText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer horizontalDivider;
        private System.Windows.Forms.SplitContainer verticalDivider;
        public ScintillaNet.Scintilla PatchText;
        public SearchableDataGridView FileGrid;
        public ScintillaNet.Scintilla ChangeLog;
    }
}
