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
            System.Windows.Forms.ToolStripSeparator viewMenuRefreshSeparator;
            System.Windows.Forms.ToolStripSeparator tasksDummyMenuItem;
            System.Windows.Forms.ToolStripSeparator branchesDummyMenuItem;
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip = new System.Windows.Forms.ToolStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.searchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tasksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.branchesMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.activityMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.commitMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tabs = new System.Windows.Forms.TabControl();
            this.taskTab = new System.Windows.Forms.TabPage();
            this.taskGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            this.branchesTab = new System.Windows.Forms.TabPage();
            this.branchGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            this.activityTab = new System.Windows.Forms.TabPage();
            this.commitTab = new System.Windows.Forms.TabPage();
            this.commitHorizontalDivider = new System.Windows.Forms.SplitContainer();
            this.changeLog = new ScintillaNet.Scintilla();
            this.commitVerticalDivider = new System.Windows.Forms.SplitContainer();
            this.changedFiles = new SoftwareNinjas.BranchAndReviewTools.Gui.SearchableDataGridView();
            this.patchText = new ScintillaNet.Scintilla();
            viewMenuRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
            tasksDummyMenuItem = new System.Windows.Forms.ToolStripSeparator();
            branchesDummyMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.statusBar.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.tabs.SuspendLayout();
            this.taskTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).BeginInit();
            this.branchesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).BeginInit();
            this.commitTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commitHorizontalDivider)).BeginInit();
            this.commitHorizontalDivider.Panel1.SuspendLayout();
            this.commitHorizontalDivider.Panel2.SuspendLayout();
            this.commitHorizontalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changeLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commitVerticalDivider)).BeginInit();
            this.commitVerticalDivider.Panel1.SuspendLayout();
            this.commitVerticalDivider.Panel2.SuspendLayout();
            this.commitVerticalDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changedFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patchText)).BeginInit();
            this.SuspendLayout();
            // 
            // viewMenuRefreshSeparator
            // 
            viewMenuRefreshSeparator.Name = "viewMenuRefreshSeparator";
            viewMenuRefreshSeparator.Size = new System.Drawing.Size(181, 6);
            // 
            // tasksDummyMenuItem
            // 
            tasksDummyMenuItem.Name = "tasksDummyMenuItem";
            tasksDummyMenuItem.Size = new System.Drawing.Size(57, 6);
            // 
            // branchesDummyMenuItem
            // 
            branchesDummyMenuItem.Name = "branchesDummyMenuItem";
            branchesDummyMenuItem.Size = new System.Drawing.Size(57, 6);
            // 
            // statusBar
            // 
            this.statusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarText,
            this.statusBarProgress});
            this.statusBar.Location = new System.Drawing.Point(0, 551);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(792, 22);
            this.statusBar.TabIndex = 0;
            // 
            // statusBarText
            // 
            this.statusBarText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarText.Name = "statusBarText";
            this.statusBarText.Size = new System.Drawing.Size(48, 17);
            this.statusBarText.Text = "Ready";
            // 
            // statusBarProgress
            // 
            this.statusBarProgress.Name = "statusBarProgress";
            this.statusBarProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.menuStrip, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tabs, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 551);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.tasksMenu,
            this.branchesMenu,
            this.activityMenu,
            this.commitMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(342, 25);
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
            this.fileMenu.Size = new System.Drawing.Size(34, 22);
            this.fileMenu.Text = "&File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchMenuItem});
            this.editMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editMenu.Name = "editMenu";
            this.editMenu.ShowDropDownArrow = false;
            this.editMenu.Size = new System.Drawing.Size(37, 22);
            this.editMenu.Text = "&Edit";
            // 
            // searchMenuItem
            // 
            this.searchMenuItem.Name = "searchMenuItem";
            this.searchMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchMenuItem.Size = new System.Drawing.Size(170, 22);
            this.searchMenuItem.Text = "&Search";
            this.searchMenuItem.Click += new System.EventHandler(this.searchMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tasksMenuItem,
            this.branchesMenuItem,
            this.logMenuItem,
            this.commitMenuItem,
            viewMenuRefreshSeparator,
            this.refreshMenuItem});
            this.viewMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.ShowDropDownArrow = false;
            this.viewMenu.Size = new System.Drawing.Size(43, 22);
            this.viewMenu.Text = "&View";
            // 
            // tasksMenuItem
            // 
            this.tasksMenuItem.Name = "tasksMenuItem";
            this.tasksMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.tasksMenuItem.Size = new System.Drawing.Size(184, 22);
            this.tasksMenuItem.Text = "&Tasks";
            this.tasksMenuItem.Click += new System.EventHandler(this.tasksMenuItem_Click);
            // 
            // branchesMenuItem
            // 
            this.branchesMenuItem.Name = "branchesMenuItem";
            this.branchesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.branchesMenuItem.Size = new System.Drawing.Size(184, 22);
            this.branchesMenuItem.Text = "&Branches";
            this.branchesMenuItem.Click += new System.EventHandler(this.branchesMenuItem_Click);
            // 
            // logMenuItem
            // 
            this.logMenuItem.Name = "logMenuItem";
            this.logMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.logMenuItem.Size = new System.Drawing.Size(184, 22);
            this.logMenuItem.Text = "&Log";
            this.logMenuItem.Click += new System.EventHandler(this.logMenuItem_Click);
            // 
            // commitMenuItem
            // 
            this.commitMenuItem.Name = "commitMenuItem";
            this.commitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.commitMenuItem.Size = new System.Drawing.Size(184, 22);
            this.commitMenuItem.Text = "&Changes";
            this.commitMenuItem.Click += new System.EventHandler(this.commitMenuItem_Click);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshMenuItem.Size = new System.Drawing.Size(184, 22);
            this.refreshMenuItem.Text = "&Refresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // tasksMenu
            // 
            this.tasksMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tasksMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            tasksDummyMenuItem});
            this.tasksMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tasksMenu.Name = "tasksMenu";
            this.tasksMenu.ShowDropDownArrow = false;
            this.tasksMenu.Size = new System.Drawing.Size(50, 22);
            this.tasksMenu.Text = "&Tasks";
            this.tasksMenu.DropDownOpening += new System.EventHandler(this.tasksMenu_DropDownOpening);
            // 
            // branchesMenu
            // 
            this.branchesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.branchesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            branchesDummyMenuItem});
            this.branchesMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.branchesMenu.Name = "branchesMenu";
            this.branchesMenu.ShowDropDownArrow = false;
            this.branchesMenu.Size = new System.Drawing.Size(72, 22);
            this.branchesMenu.Text = "&Branches";
            this.branchesMenu.DropDownOpening += new System.EventHandler(this.branchesMenu_DropDownOpening);
            // 
            // activityMenu
            // 
            this.activityMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.activityMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.activityMenu.Name = "activityMenu";
            this.activityMenu.ShowDropDownArrow = false;
            this.activityMenu.Size = new System.Drawing.Size(35, 22);
            this.activityMenu.Text = "&Log";
            // 
            // commitMenu
            // 
            this.commitMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.commitMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.commitMenu.Name = "commitMenu";
            this.commitMenu.ShowDropDownArrow = false;
            this.commitMenu.Size = new System.Drawing.Size(68, 22);
            this.commitMenu.Text = "&Changes";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.taskTab);
            this.tabs.Controls.Add(this.branchesTab);
            this.tabs.Controls.Add(this.activityTab);
            this.tabs.Controls.Add(this.commitTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(3, 28);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(786, 520);
            this.tabs.TabIndex = 0;
            this.tabs.TabStop = false;
            this.tabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabs_Selected);
            // 
            // taskTab
            // 
            this.taskTab.Controls.Add(this.taskGrid);
            this.taskTab.Location = new System.Drawing.Point(4, 22);
            this.taskTab.Name = "taskTab";
            this.taskTab.Padding = new System.Windows.Forms.Padding(3);
            this.taskTab.Size = new System.Drawing.Size(778, 494);
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
            this.taskGrid.RowTemplate.Height = 23;
            this.taskGrid.Size = new System.Drawing.Size(772, 488);
            this.taskGrid.TabIndex = 0;
            this.taskGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.taskGrid_RowContextMenuStripNeeded);
            this.taskGrid.DoubleClick += new System.EventHandler(this.taskGrid_DoubleClick);
            this.taskGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.taskGrid_KeyDown);
            this.taskGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Grid_PreviewKeyDown);
            // 
            // branchesTab
            // 
            this.branchesTab.Controls.Add(this.branchGrid);
            this.branchesTab.Location = new System.Drawing.Point(4, 22);
            this.branchesTab.Name = "branchesTab";
            this.branchesTab.Padding = new System.Windows.Forms.Padding(3);
            this.branchesTab.Size = new System.Drawing.Size(778, 494);
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
            this.branchGrid.RowTemplate.Height = 23;
            this.branchGrid.Size = new System.Drawing.Size(772, 488);
            this.branchGrid.TabIndex = 0;
            this.branchGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.branchGrid_RowContextMenuStripNeeded);
            this.branchGrid.DoubleClick += new System.EventHandler(this.branchGrid_DoubleClick);
            this.branchGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.branchGrid_KeyDown);
            this.branchGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Grid_PreviewKeyDown);
            // 
            // activityTab
            // 
            this.activityTab.Location = new System.Drawing.Point(4, 22);
            this.activityTab.Name = "activityTab";
            this.activityTab.Padding = new System.Windows.Forms.Padding(3);
            this.activityTab.Size = new System.Drawing.Size(778, 494);
            this.activityTab.TabIndex = 3;
            this.activityTab.Text = "Log";
            this.activityTab.UseVisualStyleBackColor = true;
            // 
            // commitTab
            // 
            this.commitTab.Controls.Add(this.commitHorizontalDivider);
            this.commitTab.Location = new System.Drawing.Point(4, 22);
            this.commitTab.Name = "commitTab";
            this.commitTab.Size = new System.Drawing.Size(778, 494);
            this.commitTab.TabIndex = 4;
            this.commitTab.Text = "Changes";
            this.commitTab.UseVisualStyleBackColor = true;
            // 
            // commitHorizontalDivider
            // 
            this.commitHorizontalDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commitHorizontalDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commitHorizontalDivider.Location = new System.Drawing.Point(0, 0);
            this.commitHorizontalDivider.Name = "commitHorizontalDivider";
            this.commitHorizontalDivider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // commitHorizontalDivider.Panel1
            // 
            this.commitHorizontalDivider.Panel1.Controls.Add(this.changeLog);
            // 
            // commitHorizontalDivider.Panel2
            // 
            this.commitHorizontalDivider.Panel2.Controls.Add(this.commitVerticalDivider);
            this.commitHorizontalDivider.Size = new System.Drawing.Size(778, 494);
            this.commitHorizontalDivider.SplitterDistance = 228;
            this.commitHorizontalDivider.TabIndex = 0;
            this.commitHorizontalDivider.TabStop = false;
            // 
            // changeLog
            // 
            this.changeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLog.Location = new System.Drawing.Point(0, 0);
            this.changeLog.Name = "changeLog";
            this.changeLog.Size = new System.Drawing.Size(776, 226);
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
            // commitVerticalDivider
            // 
            this.commitVerticalDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commitVerticalDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commitVerticalDivider.Location = new System.Drawing.Point(0, 0);
            this.commitVerticalDivider.Name = "commitVerticalDivider";
            // 
            // commitVerticalDivider.Panel1
            // 
            this.commitVerticalDivider.Panel1.Controls.Add(this.changedFiles);
            // 
            // commitVerticalDivider.Panel2
            // 
            this.commitVerticalDivider.Panel2.Controls.Add(this.patchText);
            this.commitVerticalDivider.Size = new System.Drawing.Size(778, 262);
            this.commitVerticalDivider.SplitterDistance = 273;
            this.commitVerticalDivider.SplitterWidth = 6;
            this.commitVerticalDivider.TabIndex = 0;
            this.commitVerticalDivider.TabStop = false;
            // 
            // changedFiles
            // 
            this.changedFiles.DataTable = null;
            this.changedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changedFiles.Filter = null;
            this.changedFiles.Location = new System.Drawing.Point(0, 0);
            this.changedFiles.Name = "changedFiles";
            this.changedFiles.RowTemplate.Height = 23;
            this.changedFiles.Size = new System.Drawing.Size(271, 260);
            this.changedFiles.TabIndex = 0;
            this.changedFiles.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.changedFiles_RowContextMenuStripNeeded);
            this.changedFiles.DoubleClick += new System.EventHandler(this.changedFiles_DoubleClick);
            this.changedFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.changedFiles_KeyDown);
            this.changedFiles.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Grid_PreviewKeyDown);
            // 
            // patchText
            // 
            this.patchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchText.Location = new System.Drawing.Point(0, 0);
            this.patchText.Name = "patchText";
            this.patchText.Size = new System.Drawing.Size(497, 260);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.statusBar);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default.WindowLocation;
            this.Name = "Main";
            this.Text = "Branch and Review Tools";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.taskTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).EndInit();
            this.branchesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).EndInit();
            this.commitTab.ResumeLayout(false);
            this.commitHorizontalDivider.Panel1.ResumeLayout(false);
            this.commitHorizontalDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commitHorizontalDivider)).EndInit();
            this.commitHorizontalDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.changeLog)).EndInit();
            this.commitVerticalDivider.Panel1.ResumeLayout(false);
            this.commitVerticalDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commitVerticalDivider)).EndInit();
            this.commitVerticalDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.changedFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patchText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarText;
        private System.Windows.Forms.ToolStripProgressBar statusBarProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ToolStrip menuStrip;
        private System.Windows.Forms.ToolStripDropDownButton fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage branchesTab;
        private System.Windows.Forms.TabPage activityTab;
        private System.Windows.Forms.TabPage commitTab;
        private SearchableDataGridView taskGrid;
        private SearchableDataGridView branchGrid;
        private System.Windows.Forms.SplitContainer commitHorizontalDivider;
        private System.Windows.Forms.SplitContainer commitVerticalDivider;
        private ScintillaNet.Scintilla changeLog;
        private ScintillaNet.Scintilla patchText;
        private SearchableDataGridView changedFiles;
        private System.Windows.Forms.ToolStripDropDownButton viewMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem branchesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commitMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tasksMenu;
        private System.Windows.Forms.ToolStripDropDownButton branchesMenu;
        private System.Windows.Forms.ToolStripDropDownButton activityMenu;
        private System.Windows.Forms.ToolStripDropDownButton commitMenu;
        private System.Windows.Forms.ToolStripDropDownButton editMenu;
        private System.Windows.Forms.ToolStripMenuItem searchMenuItem;
    }
}