using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class AwesomeGrid : UserControl, ISupportInitialize
    {
        public event ContextMenuNeededEventHandler ContextMenuStripNeeded;
        public event EventHandler SelectionChanged;
        public event EventHandler RowInvoked;

        private readonly Throttler _searchThrottle;
        private bool _isBinding;

        public AwesomeGrid()
        {
            InitializeComponent();
            this.grid.DoubleClick += Grid_DoubleClick;
            this.grid.KeyDown += Grid_KeyDown;
            this.grid.PreviewKeyDown += Grid_PreviewKeyDown;
            this.grid.ContextMenuNeeded += Grid_ContextMenuNeeded;
            this.grid.SelectionChanged += Grid_SelectionChanged;

            this.Click += AwesomeGrid_Click;
            this.tableLayout.Click += AwesomeGrid_Click;
            this.CaptionLabel.Click += AwesomeGrid_Click;

            this.GotFocus += AwesomeGrid_GotFocus;
            this.CaptionLabel.GotFocus += AwesomeGrid_GotFocus;
            this.SearchLabel.GotFocus += AwesomeGrid_GotFocus;
            this.SearchTextBox.GotFocus += AwesomeGrid_GotFocus;
            this.grid.GotFocus += AwesomeGrid_GotFocus;
            
            this.LostFocus += AwesomeGrid_LostFocus;
            this.CaptionLabel.LostFocus += AwesomeGrid_LostFocus;
            this.SearchLabel.LostFocus += AwesomeGrid_LostFocus;
            this.SearchTextBox.LostFocus += AwesomeGrid_LostFocus;
            this.grid.LostFocus += AwesomeGrid_LostFocus;

            _searchThrottle = new Throttler(200, () =>
                {
                    _filter = SearchTextBox.Text;
                    UpdateFilter();
                }
            );
            AwesomeGrid_LostFocus(this, null);
        }

        public IAccessibleGrid Grid
        {
            get { return this.grid; }
        }

        void AwesomeGrid_LostFocus(object sender, EventArgs e)
        {
            UpdateColors(SystemColors.InactiveCaptionText, SystemColors.InactiveCaption);
        }

        void AwesomeGrid_GotFocus(object sender, EventArgs e)
        {
            UpdateColors(SystemColors.ActiveCaptionText, SystemColors.ActiveCaption);
        }

        private void UpdateColors(Color foreground, Color background)
        {
            this.BackColor = background;
            this.CaptionLabel.ForeColor = foreground;
            this.CaptionLabel.BackColor = background;
            this.SearchLabel.ForeColor = foreground;
            this.SearchLabel.BackColor = background;
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

        void Grid_ContextMenuNeeded(object sender, ContextMenuNeededEventArgs e)
        {
            if (ContextMenuStripNeeded != null)
            {
                ContextMenuStripNeeded(sender, e);
            }
        }

        void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null && _dataTable != null && !_isBinding)
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
                    grid.Focus();
                    return true;
                }
            }
            var processCmdKey = base.ProcessCmdKey(ref msg, keyData);
            return processCmdKey;
        }

        private IList<Type> _columnTypes;
        private IList<bool> _columnSearchable;
        private DataTable _dataTable;
        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                this.Grid.Columns.Clear ();
                if (_dataTable != null)
                {
                    var dataColumns = _dataTable.Columns.Cast<DataColumn>();
                    _columnTypes = dataColumns.Select(dc => dc.DataType).ToList();
                    _columnSearchable = dataColumns.Select(dc => dc.IsSearchable()).ToList();
                    #region Manage the columns based on the DataTable)
                    foreach (var dataColumn in dataColumns)
                    {
                        var gridColumn = this.Grid.CreateGridColumn
                            (dataColumn.ColumnName, dataColumn.Caption, dataColumn.IsVisible());
                        this.Grid.Columns.Add (gridColumn);
                    }
                    #endregion

                    #region Adjust throttle based on data size
                    if (_dataTable.Rows.Count > 500)
                    {
                        _searchThrottle.IntervalMilliseconds = 200;
                    }
                    else if (_dataTable.Rows.Count > 100)
                    {
                        _searchThrottle.IntervalMilliseconds = 100;
                    }
                    else
                    {
                        _searchThrottle.IntervalMilliseconds = 1;
                    }
                    #endregion
                }
                UpdateFilter();
            }
        }

        private void UpdateDataSource(DataTable dataSource)
        {
            var oldSelection = this.Grid.SelectedRows.Map(row => row.DataRow).ToList();
            _isBinding = true;
            this.Grid.SelectedRows.Clear();
            this.Grid.DataSource = dataSource;
            foreach (var gridItem in this.Grid.Rows)
            {
                for (var i = 0; i < oldSelection.Count; i++)
                {
                    if (gridItem.DataRow.PointsToSameData(oldSelection[i]))
                    {
                        gridItem.Selected = true;
                        oldSelection.RemoveAt(i);
                        break;
                    }
                }
                if (oldSelection.Count == 0)
                {
                    break;
                }
            }
            _isBinding = false;
            // TODO: only fire this if the selection actually changed (i.e. items still in oldSelection)
            Grid_SelectionChanged(this, null);
        }

        private string _filter = String.Empty;
        public string Filter
        {
            get { return _filter; }
            set { SearchTextBox.Text = value; }
        }

        private void UpdateFilter()
        {
            if (_dataTable == null || String.IsNullOrEmpty(_filter))
            {
                UpdateDataSource(_dataTable);
            }
            else
            {
                var cloned = _dataTable.Clone ();
                var filterParts = _filter.Split(' ');
                var filterChunks = filterParts.Select (p => new FilterChunk(p)).ToList();
                foreach (DataRow dataRow in _dataTable.Rows)
                {
                    if (dataRow.Matches(_columnTypes, _columnSearchable, filterChunks))
                    {
                        cloned.Rows.Add (dataRow.ItemArray);
                    }
                }
                UpdateDataSource(cloned);
            }
        }

        #region Implementation of ISupportInitialize

        public void BeginInit()
        {
            var supportInitialize = this.Grid as ISupportInitialize;
            if (supportInitialize != null)
            {
                supportInitialize.BeginInit();
            }
        }

        public void EndInit()
        {
            var supportInitialize = this.Grid as ISupportInitialize;
            if (supportInitialize != null)
            {
                supportInitialize.EndInit();
            }
        }

        #endregion

        private void SearchLabel_Click(object sender, EventArgs e)
        {
            SearchTextBox.Focus();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            _searchThrottle.Fire();
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            SearchTextBox.SelectAll();
        }

        private void AwesomeGrid_Click(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
