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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.searchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tasksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.branchesMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.commitMenu = new System.Windows.Forms.ToolStripDropDownButton();
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
            this.commitHorizontalDivider = new System.Windows.Forms.SplitContainer();
            this.changeLog = new ScintillaNet.Scintilla();
            this.commitVerticalDivider = new System.Windows.Forms.SplitContainer();
            this.changedFiles = new SoftwareNinjas.BranchAndReviewTools.Gui.AwesomeGrid();
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
            ((System.ComponentModel.ISupportInitialize)(this.branchGridAndRestDivider)).BeginInit();
            this.branchGridAndRestDivider.Panel1.SuspendLayout();
            this.branchGridAndRestDivider.Panel2.SuspendLayout();
            this.branchGridAndRestDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityTopBottomPanel)).BeginInit();
            this.activityTopBottomPanel.Panel1.SuspendLayout();
            this.activityTopBottomPanel.Panel2.SuspendLayout();
            this.activityTopBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activityRevisions)).BeginInit();
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
            this.tableLayoutPanel.Controls.Add(this.statusBar, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 573);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.tasksMenu,
            this.branchesMenu,
            this.commitMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 27);
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
            this.fileMenu.Size = new System.Drawing.Size(34, 20);
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
            this.editMenu.Size = new System.Drawing.Size(37, 20);
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
            this.commitMenuItem,
            viewMenuRefreshSeparator,
            this.refreshMenuItem});
            this.viewMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.ShowDropDownArrow = false;
            this.viewMenu.Size = new System.Drawing.Size(43, 20);
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
            // commitMenuItem
            // 
            this.commitMenuItem.Name = "commitMenuItem";
            this.commitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
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
            this.tasksMenu.Size = new System.Drawing.Size(50, 20);
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
            this.branchesMenu.Size = new System.Drawing.Size(72, 20);
            this.branchesMenu.Text = "&Branches";
            this.branchesMenu.DropDownOpening += new System.EventHandler(this.branchesMenu_DropDownOpening);
            // 
            // commitMenu
            // 
            this.commitMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.commitMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.commitMenu.Name = "commitMenu";
            this.commitMenu.ShowDropDownArrow = false;
            this.commitMenu.Size = new System.Drawing.Size(68, 20);
            this.commitMenu.Text = "&Changes";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.taskTab);
            this.tabs.Controls.Add(this.branchesTab);
            this.tabs.Controls.Add(this.commitTab);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 27);
            this.tabs.Margin = new System.Windows.Forms.Padding(0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(792, 524);
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
            this.taskTab.Size = new System.Drawing.Size(784, 498);
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
            this.taskGrid.Size = new System.Drawing.Size(778, 492);
            this.taskGrid.TabIndex = 0;
            this.taskGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.taskGrid_RowContextMenuStripNeeded);
            this.taskGrid.DoubleClick += new System.EventHandler(this.taskGrid_DoubleClick);
            this.taskGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.taskGrid_KeyDown);
            this.taskGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Grid_PreviewKeyDown);
            // 
            // branchesTab
            // 
            this.branchesTab.Controls.Add(this.branchGridAndRestDivider);
            this.branchesTab.Location = new System.Drawing.Point(4, 22);
            this.branchesTab.Name = "branchesTab";
            this.branchesTab.Padding = new System.Windows.Forms.Padding(3);
            this.branchesTab.Size = new System.Drawing.Size(784, 498);
            this.branchesTab.TabIndex = 2;
            this.branchesTab.Text = "Branches";
            this.branchesTab.UseVisualStyleBackColor = true;
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
            this.branchGridAndRestDivider.Size = new System.Drawing.Size(778, 492);
            this.branchGridAndRestDivider.SplitterDistance = 122;
            this.branchGridAndRestDivider.TabIndex = 1;
            this.branchGridAndRestDivider.TabStop = false;
            // 
            // branchGrid
            // 
            this.branchGrid.DataTable = null;
            this.branchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.branchGrid.Filter = null;
            this.branchGrid.Location = new System.Drawing.Point(0, 0);
            this.branchGrid.Name = "branchGrid";
            this.branchGrid.Size = new System.Drawing.Size(776, 120);
            this.branchGrid.TabIndex = 0;
            this.branchGrid.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.branchGrid_RowContextMenuStripNeeded);
            this.branchGrid.DoubleClick += new System.EventHandler(this.branchGrid_DoubleClick);
            this.branchGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.branchGrid_KeyDown);
            this.branchGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Grid_PreviewKeyDown);
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
            this.activityTopBottomPanel.Size = new System.Drawing.Size(778, 366);
            this.activityTopBottomPanel.SplitterDistance = 78;
            this.activityTopBottomPanel.TabIndex = 1;
            this.activityTopBottomPanel.TabStop = false;
            // 
            // activityRevisions
            // 
            this.activityRevisions.DataTable = null;
            this.activityRevisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityRevisions.Filter = null;
            this.activityRevisions.Location = new System.Drawing.Point(0, 0);
            this.activityRevisions.Name = "activityRevisions";
            this.activityRevisions.Size = new System.Drawing.Size(776, 76);
            this.activityRevisions.TabIndex = 0;
            // 
            // activityChangeInspector
            // 
            this.activityChangeInspector.ActionsForChangesFunction = null;
            this.activityChangeInspector.ChangesFunction = null;
            this.activityChangeInspector.ComputeDifferencesFunction = null;
            this.activityChangeInspector.Context = null;
            this.activityChangeInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityChangeInspector.Location = new System.Drawing.Point(0, 0);
            this.activityChangeInspector.Name = "activityChangeInspector";
            this.activityChangeInspector.PatchText = "";
            this.activityChangeInspector.Size = new System.Drawing.Size(776, 282);
            this.activityChangeInspector.TabIndex = 0;
            // 
            // commitTab
            // 
            this.commitTab.Controls.Add(this.commitHorizontalDivider);
            this.commitTab.Location = new System.Drawing.Point(4, 22);
            this.commitTab.Name = "commitTab";
            this.commitTab.Size = new System.Drawing.Size(784, 498);
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
            this.commitHorizontalDivider.Size = new System.Drawing.Size(784, 498);
            this.commitHorizontalDivider.SplitterDistance = 228;
            this.commitHorizontalDivider.TabIndex = 0;
            this.commitHorizontalDivider.TabStop = false;
            // 
            // changeLog
            // 
            this.changeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLog.Location = new System.Drawing.Point(0, 0);
            this.changeLog.Name = "changeLog";
            this.changeLog.Size = new System.Drawing.Size(782, 226);
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
            this.commitVerticalDivider.Size = new System.Drawing.Size(784, 266);
            this.commitVerticalDivider.SplitterDistance = 275;
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
            this.changedFiles.Size = new System.Drawing.Size(273, 264);
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
            this.patchText.Size = new System.Drawing.Size(501, 264);
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
            this.branchGridAndRestDivider.Panel1.ResumeLayout(false);
            this.branchGridAndRestDivider.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.branchGridAndRestDivider)).EndInit();
            this.branchGridAndRestDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.branchGrid)).EndInit();
            this.activityTopBottomPanel.Panel1.ResumeLayout(false);
            this.activityTopBottomPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.activityTopBottomPanel)).EndInit();
            this.activityTopBottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.activityRevisions)).EndInit();
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

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarText;
        private System.Windows.Forms.ToolStripProgressBar statusBarProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripDropDownButton fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage branchesTab;
        private System.Windows.Forms.TabPage commitTab;
        private AwesomeGrid taskGrid;
        private AwesomeGrid branchGrid;
        private System.Windows.Forms.SplitContainer commitHorizontalDivider;
        private System.Windows.Forms.SplitContainer commitVerticalDivider;
        private ScintillaNet.Scintilla changeLog;
        private ScintillaNet.Scintilla patchText;
        private AwesomeGrid changedFiles;
        private System.Windows.Forms.ToolStripDropDownButton viewMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem branchesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commitMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tasksMenu;
        private System.Windows.Forms.ToolStripDropDownButton branchesMenu;
        private System.Windows.Forms.ToolStripDropDownButton commitMenu;
        private System.Windows.Forms.ToolStripDropDownButton editMenu;
        private System.Windows.Forms.ToolStripMenuItem searchMenuItem;
        private System.Windows.Forms.SplitContainer branchGridAndRestDivider;
        private System.Windows.Forms.SplitContainer activityTopBottomPanel;
        private AwesomeGrid activityRevisions;
        private ChangeInspector activityChangeInspector;
    }
}