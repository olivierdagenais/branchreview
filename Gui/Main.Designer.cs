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
            System.Windows.Forms.MenuItem viewMenuRefreshSeparator;
            System.Windows.Forms.MenuItem tasksDummyMenuItem;
            System.Windows.Forms.MenuItem branchesDummyMenuItem;
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip = new System.Windows.Forms.MainMenu();
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.editMenu = new System.Windows.Forms.MenuItem();
            this.searchMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenu = new System.Windows.Forms.MenuItem();
            this.goToTasksMenuItem = new System.Windows.Forms.MenuItem();
            this.goToBranchesMenuItem = new System.Windows.Forms.MenuItem();
            this.goToPendingMenuItem = new System.Windows.Forms.MenuItem();
            this.refreshMenuItem = new System.Windows.Forms.MenuItem();
            this.tasksMenu = new System.Windows.Forms.MenuItem();
            this.branchesMenu = new System.Windows.Forms.MenuItem();
            this.commitMenu = new System.Windows.Forms.MenuItem();
            this.commitMenuItem = new System.Windows.Forms.MenuItem();
            this.tabs = new System.Windows.Forms.TabControl();
            this.taskTab = new System.Windows.Forms.TabPage();
            this.taskGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.AwesomeGrid();
            this.branchesTab = new System.Windows.Forms.TabPage();
            this.branchGridAndRestDivider = new System.Windows.Forms.SplitContainer();
            this.branchGrid = new SoftwareNinjas.BranchAndReviewTools.Gui.AwesomeGrid();
            this.activityTopBottomPanel = new System.Windows.Forms.SplitContainer();
            this.activityRevisions = new SoftwareNinjas.BranchAndReviewTools.Gui.AwesomeGrid();
            this.activityChangeInspector = new SoftwareNinjas.BranchAndReviewTools.Gui.ChangeInspector();
            this.commitTab = new System.Windows.Forms.TabPage();
            this.pendingChanges = new SoftwareNinjas.BranchAndReviewTools.Gui.ChangeInspector();
            viewMenuRefreshSeparator = new System.Windows.Forms.MenuItem();
            tasksDummyMenuItem = new System.Windows.Forms.MenuItem();
            branchesDummyMenuItem = new System.Windows.Forms.MenuItem();
            this.statusBar.SuspendLayout();
            this.tabs.SuspendLayout();
            this.taskTab.SuspendLayout();
            this.branchesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.branchGridAndRestDivider)).BeginInit();
            this.branchGridAndRestDivider.Panel1.SuspendLayout();
            this.branchGridAndRestDivider.Panel2.SuspendLayout();
            this.branchGridAndRestDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activityTopBottomPanel)).BeginInit();
            this.activityTopBottomPanel.Panel1.SuspendLayout();
            this.activityTopBottomPanel.Panel2.SuspendLayout();
            this.activityTopBottomPanel.SuspendLayout();
            this.commitTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewMenuRefreshSeparator
            // 
            viewMenuRefreshSeparator.Name = "viewMenuRefreshSeparator";
            viewMenuRefreshSeparator.Text = "-";
            // 
            // tasksDummyMenuItem
            // 
            tasksDummyMenuItem.Name = "tasksDummyMenuItem";
            tasksDummyMenuItem.Text = "-";
            // 
            // branchesDummyMenuItem
            // 
            branchesDummyMenuItem.Name = "branchesDummyMenuItem";
            branchesDummyMenuItem.Text = "-";
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
            this.statusBarText.Image = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Resources.dialog_information;
            this.statusBarText.Name = "statusBarText";
            this.statusBarText.Size = new System.Drawing.Size(59, 17);
            this.statusBarText.Text = "Ready";
            // 
            // statusBarProgress
            // 
            this.statusBarProgress.Name = "statusBarProgress";
            this.statusBarProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip
            // 
            this.menuStrip.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.tasksMenu,
            this.branchesMenu,
            this.commitMenu});
            this.menuStrip.Name = "menuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {                                
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Text = "&File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.searchMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Text = "&Edit";
            // 
            // searchMenuItem
            // 
            this.searchMenuItem.Name = "searchMenuItem";
            this.searchMenuItem.ShowShortcut = true;
            this.searchMenuItem.Text = "&Search\tCtrl+F";
            this.searchMenuItem.Click += new System.EventHandler(this.searchMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.goToTasksMenuItem,
            this.goToBranchesMenuItem,
            this.goToPendingMenuItem,
            viewMenuRefreshSeparator,
            this.refreshMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Text = "&View";
            // 
            // goToTasksMenuItem
            // 
            this.goToTasksMenuItem.Name = "goToTasksMenuItem";
            this.goToTasksMenuItem.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
            this.goToTasksMenuItem.Text = "&Tasks";
            this.goToTasksMenuItem.Click += new System.EventHandler(this.goToTasksMenuItem_Click);
            // 
            // goToBranchesMenuItem
            // 
            this.goToBranchesMenuItem.Name = "goToBranchesMenuItem";
            this.goToBranchesMenuItem.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
            this.goToBranchesMenuItem.Text = "&Branches";
            this.goToBranchesMenuItem.Click += new System.EventHandler(this.goToBranchesMenuItem_Click);
            // 
            // goToPendingMenuItem
            // 
            this.goToPendingMenuItem.Name = "goToPendingMenuItem";
            this.goToPendingMenuItem.Shortcut = System.Windows.Forms.Shortcut.Ctrl3;
            this.goToPendingMenuItem.Text = "&Pending";
            this.goToPendingMenuItem.Click += new System.EventHandler(this.goToPendingMenuItem_Click);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.refreshMenuItem.Text = "&Refresh";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // tasksMenu
            // 
            this.tasksMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            tasksDummyMenuItem});
            this.tasksMenu.Name = "tasksMenu";
            this.tasksMenu.Text = "&Tasks";
            this.tasksMenu.Popup += new System.EventHandler(this.tasksMenu_DropDownOpening);
            // 
            // branchesMenu
            // 
            this.branchesMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            branchesDummyMenuItem});
            this.branchesMenu.Name = "branchesMenu";
            this.branchesMenu.Text = "&Branches";
            this.branchesMenu.Popup += new System.EventHandler(this.branchesMenu_DropDownOpening);
            // 
            // commitMenu
            // 
            this.commitMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.commitMenuItem});
            this.commitMenu.Name = "commitMenu";
            this.commitMenu.Text = "&Pending";
            this.commitMenu.Popup += new System.EventHandler(this.commitMenu_DropDownOpening);
            // 
            // commitMenuItem
            // 
            this.commitMenuItem.Name = "commitMenuItem";
            this.commitMenuItem.Text = "&Commit\tCtrl+Enter";
            this.commitMenuItem.Click += new System.EventHandler(this.commitMenuItem_Click);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.taskTab);
            this.tabs.Controls.Add(this.branchesTab);
            this.tabs.Controls.Add(this.commitTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 24);
            this.tabs.Margin = new System.Windows.Forms.Padding(0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(792, 527);
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
            this.taskTab.Size = new System.Drawing.Size(784, 501);
            this.taskTab.TabIndex = 1;
            this.taskTab.Text = "Tasks";
            this.taskTab.UseVisualStyleBackColor = true;
            // 
            // taskGrid
            // 
            this.taskGrid.Caption = "";
            this.taskGrid.DataTable = null;
            this.taskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskGrid.Filter = null;
            this.taskGrid.Location = new System.Drawing.Point(3, 3);
            this.taskGrid.Name = "taskGrid";
            this.taskGrid.Size = new System.Drawing.Size(778, 495);
            this.taskGrid.TabIndex = 0;
            this.taskGrid.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.ContextMenuNeededEventHandler(this.taskGrid_ContextMenuNeeded);
            this.taskGrid.RowInvoked += new System.EventHandler(this.taskGrid_RowInvoked);
            // 
            // branchesTab
            // 
            this.branchesTab.Controls.Add(this.branchGridAndRestDivider);
            this.branchesTab.Location = new System.Drawing.Point(4, 22);
            this.branchesTab.Name = "branchesTab";
            this.branchesTab.Padding = new System.Windows.Forms.Padding(3);
            this.branchesTab.Size = new System.Drawing.Size(784, 501);
            this.branchesTab.TabIndex = 2;
            this.branchesTab.Text = "Branches";
            this.branchesTab.UseVisualStyleBackColor = true;
            this.branchesTab.Layout += new System.Windows.Forms.LayoutEventHandler(this.branchesTab_Layout);
            // 
            // branchGridAndRestDivider
            // 
            this.branchGridAndRestDivider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.branchGridAndRestDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.branchGridAndRestDivider.Location = new System.Drawing.Point(3, 3);
            this.branchGridAndRestDivider.Name = "branchGridAndRestDivider";
            this.branchGridAndRestDivider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // branchGridAndRestDivider.Panel1
            // 
            this.branchGridAndRestDivider.Panel1.Controls.Add(this.branchGrid);
            // 
            // branchGridAndRestDivider.Panel2
            // 
            this.branchGridAndRestDivider.Panel2.Controls.Add(this.activityTopBottomPanel);
            this.branchGridAndRestDivider.Size = new System.Drawing.Size(778, 495);
            this.branchGridAndRestDivider.SplitterDistance = 89;
            this.branchGridAndRestDivider.TabIndex = 1;
            this.branchGridAndRestDivider.TabStop = false;
            this.branchGridAndRestDivider.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.branchGridAndRestDivider_SplitterMoved);
            // 
            // branchGrid
            // 
            this.branchGrid.Caption = "";
            this.branchGrid.DataTable = null;
            this.branchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.branchGrid.Filter = null;
            this.branchGrid.Location = new System.Drawing.Point(0, 0);
            this.branchGrid.Name = "branchGrid";
            this.branchGrid.Size = new System.Drawing.Size(776, 87);
            this.branchGrid.TabIndex = 0;
            this.branchGrid.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.ContextMenuNeededEventHandler(this.branchGrid_ContextMenuNeeded);
            this.branchGrid.RowInvoked += new System.EventHandler(this.branchGrid_RowInvoked);
            // 
            // activityTopBottomPanel
            // 
            this.activityTopBottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityTopBottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityTopBottomPanel.Location = new System.Drawing.Point(0, 0);
            this.activityTopBottomPanel.Name = "activityTopBottomPanel";
            this.activityTopBottomPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // activityTopBottomPanel.Panel1
            // 
            this.activityTopBottomPanel.Panel1.Controls.Add(this.activityRevisions);
            // 
            // activityTopBottomPanel.Panel2
            // 
            this.activityTopBottomPanel.Panel2.Controls.Add(this.activityChangeInspector);
            this.activityTopBottomPanel.Size = new System.Drawing.Size(778, 402);
            this.activityTopBottomPanel.SplitterDistance = 85;
            this.activityTopBottomPanel.TabIndex = 1;
            this.activityTopBottomPanel.TabStop = false;
            this.activityTopBottomPanel.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.activityTopBottomPanel_SplitterMoved);
            // 
            // activityRevisions
            // 
            this.activityRevisions.Caption = "";
            this.activityRevisions.DataTable = null;
            this.activityRevisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityRevisions.Filter = null;
            this.activityRevisions.Location = new System.Drawing.Point(0, 0);
            this.activityRevisions.Name = "activityRevisions";
            this.activityRevisions.Size = new System.Drawing.Size(776, 83);
            this.activityRevisions.TabIndex = 0;
            this.activityRevisions.ContextMenuStripNeeded += new SoftwareNinjas.BranchAndReviewTools.Gui.ContextMenuNeededEventHandler(this.activityRevisions_ContextMenuNeeded);
            this.activityRevisions.RowInvoked += new System.EventHandler(this.activityRevisions_RowInvoked);
            // 
            // activityChangeInspector
            // 
            this.activityChangeInspector.ActionsForChangesFunction = null;
            this.activityChangeInspector.ChangesFunction = null;
            this.activityChangeInspector.ComputeDifferencesFunction = null;
            this.activityChangeInspector.Context = null;
            this.activityChangeInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityChangeInspector.HorizontalDividerSplitterDistance = 25;
            this.activityChangeInspector.Location = new System.Drawing.Point(0, 0);
            this.activityChangeInspector.MessageFunction = null;
            this.activityChangeInspector.Name = "activityChangeInspector";
            this.activityChangeInspector.PatchText = "";
            this.activityChangeInspector.Size = new System.Drawing.Size(776, 311);
            this.activityChangeInspector.TabIndex = 0;
            this.activityChangeInspector.VerticalDividerSplitterDistance = 243;
            this.activityChangeInspector.HorizontalDividerSplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.activityChangeInspector_HorizontalDividerSplitterMoved);
            this.activityChangeInspector.VerticalDividerSplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.activityChangeInspector_VerticalDividerSplitterMoved);
            // 
            // commitTab
            // 
            this.commitTab.Controls.Add(this.pendingChanges);
            this.commitTab.Location = new System.Drawing.Point(4, 22);
            this.commitTab.Name = "commitTab";
            this.commitTab.Size = new System.Drawing.Size(784, 501);
            this.commitTab.TabIndex = 4;
            this.commitTab.Text = "Pending";
            this.commitTab.UseVisualStyleBackColor = true;
            this.commitTab.Layout += new System.Windows.Forms.LayoutEventHandler(this.commitTab_Layout);
            // 
            // pendingChanges
            // 
            this.pendingChanges.ActionsForChangesFunction = null;
            this.pendingChanges.ChangesFunction = null;
            this.pendingChanges.ComputeDifferencesFunction = null;
            this.pendingChanges.Context = null;
            this.pendingChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pendingChanges.HorizontalDividerSplitterDistance = 78;
            this.pendingChanges.Location = new System.Drawing.Point(0, 0);
            this.pendingChanges.MessageFunction = null;
            this.pendingChanges.Name = "pendingChanges";
            this.pendingChanges.PatchText = "";
            this.pendingChanges.Size = new System.Drawing.Size(784, 501);
            this.pendingChanges.TabIndex = 0;
            this.pendingChanges.VerticalDividerSplitterDistance = 245;
            this.pendingChanges.HorizontalDividerSplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.pendingChanges_HorizontalDividerSplitterMoved);
            this.pendingChanges.VerticalDividerSplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.pendingChanges_VerticalDividerSplitterMoved);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.statusBar);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SoftwareNinjas.BranchAndReviewTools.Gui.Properties.Settings.Default.WindowLocation;
            this.Menu = this.menuStrip;
            this.Name = "Main";
            this.Text = "Branch and Review Tools";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.taskTab.ResumeLayout(false);
            this.branchesTab.ResumeLayout(false);
            this.branchGridAndRestDivider.Panel1.ResumeLayout(false);
            this.branchGridAndRestDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.branchGridAndRestDivider)).EndInit();
            this.branchGridAndRestDivider.ResumeLayout(false);
            this.activityTopBottomPanel.Panel1.ResumeLayout(false);
            this.activityTopBottomPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.activityTopBottomPanel)).EndInit();
            this.activityTopBottomPanel.ResumeLayout(false);
            this.commitTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarText;
        private System.Windows.Forms.ToolStripProgressBar statusBarProgress;
        private System.Windows.Forms.MainMenu menuStrip;
        private System.Windows.Forms.MenuItem fileMenu;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage branchesTab;
        private System.Windows.Forms.TabPage commitTab;
        private AwesomeGrid taskGrid;
        private AwesomeGrid branchGrid;
        private System.Windows.Forms.MenuItem viewMenu;
        private System.Windows.Forms.MenuItem refreshMenuItem;
        private System.Windows.Forms.MenuItem goToTasksMenuItem;
        private System.Windows.Forms.MenuItem goToBranchesMenuItem;
        private System.Windows.Forms.MenuItem goToPendingMenuItem;
        private System.Windows.Forms.MenuItem tasksMenu;
        private System.Windows.Forms.MenuItem branchesMenu;
        private System.Windows.Forms.MenuItem commitMenu;
        private System.Windows.Forms.MenuItem editMenu;
        private System.Windows.Forms.MenuItem searchMenuItem;
        private System.Windows.Forms.SplitContainer branchGridAndRestDivider;
        private System.Windows.Forms.SplitContainer activityTopBottomPanel;
        private AwesomeGrid activityRevisions;
        private ChangeInspector activityChangeInspector;
        private ChangeInspector pendingChanges;
        private System.Windows.Forms.MenuItem commitMenuItem;
    }
}