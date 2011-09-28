namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.taskTab = new System.Windows.Forms.TabPage();
            this.taskGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            this.branchesTab = new System.Windows.Forms.TabPage();
            this.branchGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            this.activityTab = new System.Windows.Forms.TabPage();
            this.commitTab = new System.Windows.Forms.TabPage();
            this.menuStrip = new System.Windows.Forms.ToolStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchStrip = new System.Windows.Forms.ToolStrip();
            this.searchLabel = new System.Windows.Forms.ToolStripLabel();
            this.searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.statusBar.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.tabs.SuspendLayout();
            this.taskTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).BeginInit();
            this.branchesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.searchStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarText,
            this.statusBarProgress});
            this.statusBar.Location = new System.Drawing.Point(0, 551);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusBar.Size = new System.Drawing.Size(679, 22);
            this.statusBar.TabIndex = 0;
            // 
            // statusBarText
            // 
            this.statusBarText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarText.Name = "statusBarText";
            this.statusBarText.Size = new System.Drawing.Size(43, 17);
            this.statusBarText.Text = "Ready";
            // 
            // statusBarProgress
            // 
            this.statusBarProgress.Name = "statusBarProgress";
            this.statusBarProgress.Size = new System.Drawing.Size(86, 16);
            // 
            // toolStripContainer
            // 
            this.toolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tabs);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(679, 501);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(679, 551);
            this.toolStripContainer.TabIndex = 1;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.searchStrip);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.taskTab);
            this.tabs.Controls.Add(this.branchesTab);
            this.tabs.Controls.Add(this.activityTab);
            this.tabs.Controls.Add(this.commitTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(679, 501);
            this.tabs.TabIndex = 0;
            // 
            // taskTab
            // 
            this.taskTab.Controls.Add(this.taskGrid);
            this.taskTab.Location = new System.Drawing.Point(4, 22);
            this.taskTab.Name = "taskTab";
            this.taskTab.Padding = new System.Windows.Forms.Padding(3);
            this.taskTab.Size = new System.Drawing.Size(671, 475);
            this.taskTab.TabIndex = 1;
            this.taskTab.Text = "Tasks";
            this.taskTab.UseVisualStyleBackColor = true;
            // 
            // taskGrid
            // 
            this.taskGrid.DataTable = null;
            this.taskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskGrid.Filter = null;
            this.taskGrid.Location = new System.Drawing.Point(3, 3);
            this.taskGrid.Name = "taskGrid";
            this.taskGrid.Size = new System.Drawing.Size(665, 469);
            this.taskGrid.TabIndex = 0;
            this.taskGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.taskGrid_RowContextMenuStripNeeded);
            // 
            // branchesTab
            // 
            this.branchesTab.Controls.Add(this.branchGrid);
            this.branchesTab.Location = new System.Drawing.Point(4, 22);
            this.branchesTab.Name = "branchesTab";
            this.branchesTab.Padding = new System.Windows.Forms.Padding(3);
            this.branchesTab.Size = new System.Drawing.Size(671, 475);
            this.branchesTab.TabIndex = 2;
            this.branchesTab.Text = "Branches";
            this.branchesTab.UseVisualStyleBackColor = true;
            // 
            // branchGrid
            // 
            this.branchGrid.DataTable = null;
            this.branchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.branchGrid.Filter = null;
            this.branchGrid.Location = new System.Drawing.Point(3, 3);
            this.branchGrid.MultiSelect = false;
            this.branchGrid.Name = "branchGrid";
            this.branchGrid.Size = new System.Drawing.Size(665, 469);
            this.branchGrid.TabIndex = 0;
            this.branchGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.branchGrid_RowContextMenuStripNeeded);
            // 
            // activityTab
            // 
            this.activityTab.Location = new System.Drawing.Point(4, 22);
            this.activityTab.Name = "activityTab";
            this.activityTab.Padding = new System.Windows.Forms.Padding(3);
            this.activityTab.Size = new System.Drawing.Size(671, 475);
            this.activityTab.TabIndex = 3;
            this.activityTab.Text = "Activity/Log";
            this.activityTab.UseVisualStyleBackColor = true;
            // 
            // commitTab
            // 
            this.commitTab.Location = new System.Drawing.Point(4, 22);
            this.commitTab.Name = "commitTab";
            this.commitTab.Size = new System.Drawing.Size(671, 475);
            this.commitTab.TabIndex = 4;
            this.commitTab.Text = "Commit";
            this.commitTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip.Location = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default.MenuLocation;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(40, 25);
            this.menuStrip.TabIndex = 0;
            // 
            // fileMenu
            // 
            this.fileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.ShowDropDownArrow = false;
            this.fileMenu.Size = new System.Drawing.Size(30, 22);
            this.fileMenu.Text = "&File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(95, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // searchStrip
            // 
            this.searchStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.searchStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchLabel,
            this.searchTextBox});
            this.searchStrip.Location = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default.SearchLocation;
            this.searchStrip.Name = "searchStrip";
            this.searchStrip.Size = new System.Drawing.Size(259, 25);
            this.searchStrip.TabIndex = 1;
            // 
            // searchLabel
            // 
            this.searchLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(47, 22);
            this.searchLabel.Text = "&Search";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 25);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 573);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.statusBar);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default.WindowLocation;
            this.Name = "Main";
            this.Text = "Branch and Review Tools";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.taskTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).EndInit();
            this.branchesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.searchStrip.ResumeLayout(false);
            this.searchStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarText;
        private System.Windows.Forms.ToolStripProgressBar statusBarProgress;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip menuStrip;
        private System.Windows.Forms.ToolStripDropDownButton fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage branchesTab;
        private System.Windows.Forms.TabPage activityTab;
        private System.Windows.Forms.TabPage commitTab;
        private System.Windows.Forms.ToolStrip searchStrip;
        private System.Windows.Forms.ToolStripLabel searchLabel;
        private System.Windows.Forms.ToolStripTextBox searchTextBox;
        private SearchableDataGridView taskGrid;
        private SearchableDataGridView branchGrid;
    }
}