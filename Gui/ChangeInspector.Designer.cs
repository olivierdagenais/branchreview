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
            this.changeLog = new ScintillaNet.Scintilla();
            this.verticalDivider = new System.Windows.Forms.SplitContainer();
            this.patchText = new ScintillaNet.Scintilla();
            this.fileGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalDivider)).BeginInit();
            this.horizontalDivider.Panel1.SuspendLayout();
            this.horizontalDivider.Panel2.SuspendLayout();
            this.horizontalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changeLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalDivider)).BeginInit();
            this.verticalDivider.Panel1.SuspendLayout();
            this.verticalDivider.Panel2.SuspendLayout();
            this.verticalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patchText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).BeginInit();
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
            this.horizontalDivider.Panel1.Controls.Add(this.changeLog);
            // 
            // horizontalDivider.Panel2
            // 
            this.horizontalDivider.Panel2.Controls.Add(this.verticalDivider);
            this.horizontalDivider.Size = new System.Drawing.Size(749, 556);
            this.horizontalDivider.SplitterDistance = 108;
            this.horizontalDivider.TabIndex = 1;
            this.horizontalDivider.TabStop = false;
            // 
            // changeLog
            // 
            this.changeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLog.Location = new System.Drawing.Point(0, 0);
            this.changeLog.Name = "changeLog";
            this.changeLog.Size = new System.Drawing.Size(747, 106);
            this.changeLog.Styles.BraceBad.FontName = "Verdana";
            this.changeLog.Styles.BraceLight.FontName = "Verdana";
            this.changeLog.Styles.ControlChar.FontName = "Verdana";
            this.changeLog.Styles.Default.FontName = "Verdana";
            this.changeLog.Styles.IndentGuide.FontName = "Verdana";
            this.changeLog.Styles.LastPredefined.FontName = "Verdana";
            this.changeLog.Styles.LineNumber.FontName = "Verdana";
            this.changeLog.Styles.Max.FontName = "Verdana";
            this.changeLog.TabIndex = 0;
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
            this.verticalDivider.Panel1.Controls.Add(this.fileGrid);
            // 
            // verticalDivider.Panel2
            // 
            this.verticalDivider.Panel2.Controls.Add(this.patchText);
            this.verticalDivider.Size = new System.Drawing.Size(749, 444);
            this.verticalDivider.SplitterDistance = 235;
            this.verticalDivider.SplitterWidth = 6;
            this.verticalDivider.TabIndex = 0;
            this.verticalDivider.TabStop = false;
            // 
            // patchText
            // 
            this.patchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchText.Location = new System.Drawing.Point(0, 0);
            this.patchText.Name = "patchText";
            this.patchText.Size = new System.Drawing.Size(506, 442);
            this.patchText.Styles.BraceBad.FontName = "Verdana";
            this.patchText.Styles.BraceLight.FontName = "Verdana";
            this.patchText.Styles.ControlChar.FontName = "Verdana";
            this.patchText.Styles.Default.FontName = "Verdana";
            this.patchText.Styles.IndentGuide.FontName = "Verdana";
            this.patchText.Styles.LastPredefined.FontName = "Verdana";
            this.patchText.Styles.LineNumber.FontName = "Verdana";
            this.patchText.Styles.Max.FontName = "Verdana";
            this.patchText.TabIndex = 0;
            // 
            // fileGrid
            // 
            this.fileGrid.DataTable = null;
            this.fileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileGrid.Filter = null;
            this.fileGrid.Location = new System.Drawing.Point(0, 0);
            this.fileGrid.Name = "fileGrid";
            this.fileGrid.RowTemplate.Height = 23;
            this.fileGrid.Size = new System.Drawing.Size(233, 442);
            this.fileGrid.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.changeLog)).EndInit();
            this.verticalDivider.Panel1.ResumeLayout(false);
            this.verticalDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.verticalDivider)).EndInit();
            this.verticalDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patchText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer horizontalDivider;
        private ScintillaNet.Scintilla changeLog;
        private System.Windows.Forms.SplitContainer verticalDivider;
        private SearchableDataGridView fileGrid;
        private ScintillaNet.Scintilla patchText;
    }
}
