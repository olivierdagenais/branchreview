namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    partial class AwesomeGrid
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
            this.CaptionLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.Grid = new SoftwareNinjas.BranchAndReviewTools.Gui.AccessibleDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CaptionLabel
            // 
            this.CaptionLabel.AutoSize = true;
            this.CaptionLabel.Location = new System.Drawing.Point(2, 6);
            this.CaptionLabel.MinimumSize = new System.Drawing.Size(52, 13);
            this.CaptionLabel.Name = "CaptionLabel";
            this.CaptionLabel.Size = new System.Drawing.Size(52, 13);
            this.CaptionLabel.TabIndex = 0;
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTextBox.Location = new System.Drawing.Point(298, 2);
            this.SearchTextBox.MinimumSize = new System.Drawing.Size(200, 21);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(200, 21);
            this.SearchTextBox.TabIndex = 2;
            this.SearchTextBox.TabStop = false;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.SearchTextBox.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            // 
            // SearchLabel
            // 
            this.SearchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(240, 6);
            this.SearchLabel.MinimumSize = new System.Drawing.Size(52, 13);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(52, 13);
            this.SearchLabel.TabIndex = 1;
            this.SearchLabel.Text = "&Search:";
            this.SearchLabel.Click += new System.EventHandler(this.SearchLabel_Click);
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.Location = new System.Drawing.Point(0, 27);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(500, 260);
            this.Grid.TabIndex = 3;
            // 
            // AwesomeGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.CaptionLabel);
            this.Controls.Add(this.Grid);
            this.Name = "AwesomeGrid";
            this.Size = new System.Drawing.Size(500, 287);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AccessibleDataGridView Grid;
        public System.Windows.Forms.Label CaptionLabel;
        public System.Windows.Forms.TextBox SearchTextBox;
        public System.Windows.Forms.Label SearchLabel;
    }
}
