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
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Grid = new SoftwareNinjas.BranchAndReviewTools.Gui.AccessibleDataGridView();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CaptionLabel
            // 
            this.CaptionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CaptionLabel.AutoSize = true;
            this.CaptionLabel.Location = new System.Drawing.Point(2, 6);
            this.CaptionLabel.Margin = new System.Windows.Forms.Padding(1);
            this.CaptionLabel.MinimumSize = new System.Drawing.Size(52, 13);
            this.CaptionLabel.Name = "CaptionLabel";
            this.CaptionLabel.Size = new System.Drawing.Size(52, 13);
            this.CaptionLabel.TabIndex = 0;
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchTextBox.Location = new System.Drawing.Point(298, 2);
            this.SearchTextBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 1);
            this.SearchTextBox.MinimumSize = new System.Drawing.Size(20, 21);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(200, 21);
            this.SearchTextBox.TabIndex = 2;
            this.SearchTextBox.TabStop = false;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.SearchTextBox.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            // 
            // SearchLabel
            // 
            this.SearchLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(240, 6);
            this.SearchLabel.Margin = new System.Windows.Forms.Padding(1);
            this.SearchLabel.MinimumSize = new System.Drawing.Size(52, 13);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(52, 13);
            this.SearchLabel.TabIndex = 1;
            this.SearchLabel.Text = "&Search:";
            this.SearchLabel.Click += new System.EventHandler(this.SearchLabel_Click);
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 3;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.Controls.Add(this.CaptionLabel, 0, 0);
            this.tableLayout.Controls.Add(this.Grid, 0, 1);
            this.tableLayout.Controls.Add(this.SearchTextBox, 2, 0);
            this.tableLayout.Controls.Add(this.SearchLabel, 1, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(500, 287);
            this.tableLayout.TabIndex = 4;
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayout.SetColumnSpan(this.Grid, 3);
            this.Grid.Location = new System.Drawing.Point(0, 27);
            this.Grid.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(500, 260);
            this.Grid.TabIndex = 3;
            // 
            // AwesomeGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayout);
            this.Name = "AwesomeGrid";
            this.Size = new System.Drawing.Size(500, 287);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AccessibleDataGridView Grid;
        public System.Windows.Forms.Label CaptionLabel;
        public System.Windows.Forms.TextBox SearchTextBox;
        public System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
    }
}
