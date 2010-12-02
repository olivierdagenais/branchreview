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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Commit));
            this.verticalDivisor = new System.Windows.Forms.SplitContainer();
            this.changeLog = new System.Windows.Forms.TextBox();
            this.patchText = new System.Windows.Forms.TextBox();
            this.verticalDivisor.Panel1.SuspendLayout();
            this.verticalDivisor.Panel2.SuspendLayout();
            this.verticalDivisor.SuspendLayout();
            this.SuspendLayout();
            // 
            // verticalDivisor
            // 
            this.verticalDivisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalDivisor.Location = new System.Drawing.Point(0, 0);
            this.verticalDivisor.Name = "verticalDivisor";
            this.verticalDivisor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // verticalDivisor.Panel1
            // 
            this.verticalDivisor.Panel1.Controls.Add(this.changeLog);
            // 
            // verticalDivisor.Panel2
            // 
            this.verticalDivisor.Panel2.Controls.Add(this.patchText);
            this.verticalDivisor.Size = new System.Drawing.Size(759, 594);
            this.verticalDivisor.SplitterDistance = 125;
            this.verticalDivisor.TabIndex = 0;
            this.verticalDivisor.TabStop = false;
            // 
            // changeLog
            // 
            this.changeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeLog.Location = new System.Drawing.Point(0, 0);
            this.changeLog.Multiline = true;
            this.changeLog.Name = "changeLog";
            this.changeLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.changeLog.Size = new System.Drawing.Size(759, 125);
            this.changeLog.TabIndex = 0;
            this.changeLog.Text = "Initial creation of a Gui project with a skeleton for the Commit window.";
            // 
            // patchText
            // 
            this.patchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patchText.Location = new System.Drawing.Point(0, 0);
            this.patchText.Multiline = true;
            this.patchText.Name = "patchText";
            this.patchText.ReadOnly = true;
            this.patchText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.patchText.Size = new System.Drawing.Size(759, 465);
            this.patchText.TabIndex = 0;
            this.patchText.Text = resources.GetString("patchText.Text");
            // 
            // Commit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 594);
            this.Controls.Add(this.verticalDivisor);
            this.KeyPreview = true;
            this.Name = "Commit";
            this.Text = "Commit - PATH";
            this.Load += new System.EventHandler(this.Commit_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Commit_KeyUp);
            this.verticalDivisor.Panel1.ResumeLayout(false);
            this.verticalDivisor.Panel1.PerformLayout();
            this.verticalDivisor.Panel2.ResumeLayout(false);
            this.verticalDivisor.Panel2.PerformLayout();
            this.verticalDivisor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer verticalDivisor;
        private System.Windows.Forms.TextBox changeLog;
        private System.Windows.Forms.TextBox patchText;
    }
}