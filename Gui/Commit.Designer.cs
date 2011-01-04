namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    partial class Commit
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (Commit));
            this.verticalDivisor = new System.Windows.Forms.SplitContainer ();
            this.changeLog = new System.Windows.Forms.TextBox ();
            this.changesDivisor = new System.Windows.Forms.SplitContainer ();
            this.changedFiles = new System.Windows.Forms.ListView ();
            this.changedFilesPathColumn = new System.Windows.Forms.ColumnHeader ();
            this.changedFilesTypeColumn = new System.Windows.Forms.ColumnHeader ();
            this.patchText = new System.Windows.Forms.TextBox ();
            this.statusBar = new System.Windows.Forms.StatusStrip ();
            this.statusBarText = new System.Windows.Forms.ToolStripStatusLabel ();
            this.verticalDivisor.Panel1.SuspendLayout ();
            this.verticalDivisor.Panel2.SuspendLayout ();
            this.verticalDivisor.SuspendLayout ();
            this.changesDivisor.Panel1.SuspendLayout ();
            this.changesDivisor.Panel2.SuspendLayout ();
            this.changesDivisor.SuspendLayout ();
            this.statusBar.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // verticalDivisor
            // 
            this.verticalDivisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalDivisor.Location = new System.Drawing.Point (0, 0);
            this.verticalDivisor.Name = "verticalDivisor";
            this.verticalDivisor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // verticalDivisor.Panel1
            // 
            this.verticalDivisor.Panel1.Controls.Add (this.changeLog);
            // 
            // verticalDivisor.Panel2
            // 
            this.verticalDivisor.Panel2.Controls.Add (this.changesDivisor);
            this.verticalDivisor.Size = new System.Drawing.Size (792, 551);
            this.verticalDivisor.SplitterDistance = 114;
            this.verticalDivisor.TabIndex = 0;
            this.verticalDivisor.TabStop = false;
            // 
            // changeLog
            // 
            this.changeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLog.Font = new System.Drawing.Font ("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ));
            this.changeLog.Location = new System.Drawing.Point (0, 0);
            this.changeLog.Multiline = true;
            this.changeLog.Name = "changeLog";
            this.changeLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.changeLog.Size = new System.Drawing.Size (792, 114);
            this.changeLog.TabIndex = 0;
            this.changeLog.Text = "Initial creation of a Gui project with a skeleton for the Commit window.";
            this.changeLog.KeyDown += new System.Windows.Forms.KeyEventHandler (this.changeLog_KeyDown);
            // 
            // changesDivisor
            // 
            this.changesDivisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changesDivisor.Location = new System.Drawing.Point (0, 0);
            this.changesDivisor.Name = "changesDivisor";
            // 
            // changesDivisor.Panel1
            // 
            this.changesDivisor.Panel1.Controls.Add (this.changedFiles);
            // 
            // changesDivisor.Panel2
            // 
            this.changesDivisor.Panel2.Controls.Add (this.patchText);
            this.changesDivisor.Size = new System.Drawing.Size (792, 433);
            this.changesDivisor.SplitterDistance = 330;
            this.changesDivisor.TabIndex = 1;
            this.changesDivisor.TabStop = false;
            // 
            // changedFiles
            // 
            this.changedFiles.AllowColumnReorder = true;
            this.changedFiles.Columns.AddRange (new System.Windows.Forms.ColumnHeader[] {
            this.changedFilesPathColumn,
            this.changedFilesTypeColumn});
            this.changedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changedFiles.FullRowSelect = true;
            this.changedFiles.HideSelection = false;
            this.changedFiles.Location = new System.Drawing.Point (0, 0);
            this.changedFiles.Name = "changedFiles";
            this.changedFiles.Size = new System.Drawing.Size (330, 433);
            this.changedFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.changedFiles.TabIndex = 0;
            this.changedFiles.UseCompatibleStateImageBehavior = false;
            this.changedFiles.View = System.Windows.Forms.View.Details;
            this.changedFiles.SelectedIndexChanged += new System.EventHandler (this.changedFiles_SelectedIndexChanged);
            this.changedFiles.DoubleClick += new System.EventHandler (this.changedFiles_DoubleClick);
            this.changedFiles.KeyUp += new System.Windows.Forms.KeyEventHandler (this.changedFiles_KeyUp);
            // 
            // changedFilesPathColumn
            // 
            this.changedFilesPathColumn.Text = "Path";
            this.changedFilesPathColumn.Width = 250;
            // 
            // changedFilesTypeColumn
            // 
            this.changedFilesTypeColumn.Text = "Status";
            // 
            // patchText
            // 
            this.patchText.BackColor = System.Drawing.SystemColors.Window;
            this.patchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchText.Font = new System.Drawing.Font ("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ));
            this.patchText.Location = new System.Drawing.Point (0, 0);
            this.patchText.Multiline = true;
            this.patchText.Name = "patchText";
            this.patchText.ReadOnly = true;
            this.patchText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.patchText.Size = new System.Drawing.Size (458, 433);
            this.patchText.TabIndex = 0;
            this.patchText.Text = resources.GetString ("patchText.Text");
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.statusBarText});
            this.statusBar.Location = new System.Drawing.Point (0, 551);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size (792, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBarText
            // 
            this.statusBarText.Name = "statusBarText";
            this.statusBarText.Size = new System.Drawing.Size (48, 17);
            this.statusBarText.Text = "Ready";
            // 
            // Commit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF (7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (792, 573);
            this.Controls.Add (this.verticalDivisor);
            this.Controls.Add (this.statusBar);
            this.KeyPreview = true;
            this.Name = "Commit";
            this.Text = "Commit - PATH";
            this.Load += new System.EventHandler (this.Commit_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler (this.Commit_KeyUp);
            this.verticalDivisor.Panel1.ResumeLayout (false);
            this.verticalDivisor.Panel1.PerformLayout ();
            this.verticalDivisor.Panel2.ResumeLayout (false);
            this.verticalDivisor.ResumeLayout (false);
            this.changesDivisor.Panel1.ResumeLayout (false);
            this.changesDivisor.Panel2.ResumeLayout (false);
            this.changesDivisor.Panel2.PerformLayout ();
            this.changesDivisor.ResumeLayout (false);
            this.statusBar.ResumeLayout (false);
            this.statusBar.PerformLayout ();
            this.ResumeLayout (false);
            this.PerformLayout ();

        }

        #endregion

        private System.Windows.Forms.SplitContainer verticalDivisor;
        private System.Windows.Forms.TextBox changeLog;
        private System.Windows.Forms.SplitContainer changesDivisor;
        private System.Windows.Forms.TextBox patchText;
        private System.Windows.Forms.ListView changedFiles;
        private System.Windows.Forms.ColumnHeader changedFilesPathColumn;
        private System.Windows.Forms.ColumnHeader changedFilesTypeColumn;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarText;
    }
}