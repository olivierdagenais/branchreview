﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class ChangeInspector : UserControl
    {
        public ChangeInspector()
        {
            InitializeComponent();
            ChangeLog.InitializeDefaults();
            PatchViewer.InitializeDefaults();
            PatchViewer.InitializeDiff();
        }

        public string PatchText
        {
            get { return PatchViewer.Text; }
            set { PatchViewer.SetReadOnlyText(value); }
        }

        // given a Context (the current BranchId in the "Pending Changes" case and the current revision in "Log"),
        // produces a table of the changes involved
        public Func<object, DataTable> ChangesFunction { get; set; }
        internal DataTable GetChanges(object contextId)
        {
            return ChangesFunction != null ? ChangesFunction(contextId) : null;
        }

        // given one or more "ID"s from the table generated by ChangesFunction, produces a patch
        public Func<IEnumerable<object>, string> ComputeDifferencesFunction { get; set; }
        internal string ComputeDifferences(IEnumerable<object> selectedIds)
        {
            return ComputeDifferencesFunction != null ? ComputeDifferencesFunction(selectedIds) : String.Empty;
        }

        public Func<IEnumerable<object>, IList<MenuAction>> ActionsForChangesFunction { get; set; }
        internal IList<MenuAction> GetActionsForChanges(IEnumerable<object> selectedIds)
        {
            return ActionsForChangesFunction != null ? ActionsForChangesFunction(selectedIds) : MenuAction.EmptyList;
        }


        public object Context { get; set; }

        #region FileGrid
        private IEnumerable<object> FindSelectedIds()
        {
            var selectedRows = FileGrid.SelectedRows.Cast<DataGridViewRow>();
            return selectedRows.Map(row => row.Cells["ID"].Value);
        }

        void FileGrid_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            var menu = BuildChangedFilesActionMenu();
            e.ContextMenuStrip = menu;
        }

        private ContextMenuStrip BuildChangedFilesActionMenu()
        {
            var selectedIds = FindSelectedIds();
            var actions = GetActionsForChanges(selectedIds);
            var menu = new ContextMenuStrip();
            menu.Items.AddActions(actions);
            return menu;
        }

        private void InvokeDefaultChangedFilesAction()
        {
            var menu = BuildChangedFilesActionMenu();
            menu.Items.InvokeFirstMenuItem();
        }

        void FileGrid_DoubleClick(object sender, EventArgs e)
        {
            InvokeDefaultChangedFilesAction();
        }

        void FileGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                InvokeDefaultChangedFilesAction();
            }
        }
        #endregion
    }
}
