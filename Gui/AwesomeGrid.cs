﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class AwesomeGrid : UserControl
    {
        public event ContextMenuNeededEventHandler ContextMenuStripNeeded;
        public event EventHandler SelectionChanged;
        public event EventHandler RowInvoked;

        private readonly Timer _throttleTimer = new Timer
        {
            Interval = 200,
        };

        public AwesomeGrid()
        {
            InitializeComponent();
            Configure();
            this.Grid.AlternatingBackColor = Color.WhiteSmoke;
            this.Grid.DoubleClick += Grid_DoubleClick;
            this.Grid.KeyDown += Grid_KeyDown;
            this.Grid.PreviewKeyDown += Grid_PreviewKeyDown;
            this.Grid.ContextMenuNeeded += Grid_ContextMenuNeeded;
            this.Grid.SelectedIndexChanged += Grid_SelectedIndexChanged;
            _throttleTimer.Tick += _throttleTimer_Tick;
        }

        void _throttleTimer_Tick(object sender, EventArgs e)
        {
            _throttleTimer.Stop();
            _filter = SearchTextBox.Text;
            UpdateFilter();
        }

        // method can not be made static because the Form Designer re-writes the event wire-up with "this."
        void Grid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.IsInputKey = true;
            }
        }

        public string Caption
        {
            get { return CaptionLabel.Text; }
            set { CaptionLabel.Text = value; }
        }

        protected void OnRowInvoked(EventArgs e)
        {
            if (RowInvoked != null)
            {
                RowInvoked(this, e);
            }
        }

        void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                OnRowInvoked(e);
            }
            else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {
                SearchTextBox.Focus();
            }
        }

        void Grid_DoubleClick(object sender, EventArgs e)
        {
            OnRowInvoked(e);
        }

        private void Configure()
        {
            Grid.BorderStyle = BorderStyle.None;
        }

        void Grid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            if (ContextMenuStripNeeded != null)
            {
                ContextMenuStripNeeded(sender, e);
            }
        }

        void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (SearchTextBox.Focused)
                {
                    Grid.Focus();
                    return true;
                }
            }
            var processCmdKey = base.ProcessCmdKey(ref msg, keyData);
            return processCmdKey;
        }

        private IList<Type> _columnTypes;
        private DataTable _dataTable;
        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                if (_dataTable != null)
                {
                    var dataColumns = _dataTable.Columns.Cast<DataColumn>();
                    _columnTypes = dataColumns.Select(dc => dc.DataType).ToList();
                    #region Manage the columns based on the DataTable
                    this.Grid.Columns.Clear ();
                    foreach (var dataColumn in dataColumns)
                    {
                        var gridViewColumn = new ColumnHeader
                        {
                            Name = dataColumn.ColumnName,
                            Text = dataColumn.Caption,
                        };
                        this.Grid.Columns.Add (gridViewColumn);
                    }
                    // dummy column for the auto-sizing
                    this.Grid.Columns.Add(String.Empty);
                    #endregion

                    #region Adjust throttle based on data size
                    if (_dataTable.Rows.Count > 500)
                    {
                        _throttleTimer.Interval = 200;
                    }
                    else if (_dataTable.Rows.Count > 100)
                    {
                        _throttleTimer.Interval = 100;
                    }
                    else
                    {
                        _throttleTimer.Interval = 1;
                    }
                    #endregion
                }
                this.Grid.DataSource = _dataTable;
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SearchTextBox.Text = value; }
        }

        private void UpdateFilter()
        {
            if (_dataTable != null && _filter != null)
            {
                var cloned = _dataTable.Clone ();
                var filterParts = _filter.Split(' ');
                var filterChunks = filterParts.Select (p => new FilterChunk(p)).ToList();
                foreach (DataRow dataRow in _dataTable.Rows)
                {
                    if (dataRow.Matches(_columnTypes, filterChunks))
                    {
                        cloned.Rows.Add (dataRow.ItemArray);
                    }
                }
                this.Grid.DataSource = cloned;
            }
        }

        private void SearchLabel_Click(object sender, EventArgs e)
        {
            SearchTextBox.Focus();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            _throttleTimer.Stop();
            _throttleTimer.Start();
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            SearchTextBox.SelectAll();
        }
    }
}
