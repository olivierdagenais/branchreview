using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class AwesomeGrid : UserControl, ISupportInitialize
    {

        private static readonly DataGridViewCellStyle AlternatingRowStyle =
            new DataGridViewCellStyle { BackColor = Color.WhiteSmoke };

        public event ContextMenuNeededEventHandler ContextMenuStripNeeded;
        public event EventHandler SelectionChanged;
        public event EventHandler RowInvoked;

        private readonly Throttler _searchThrottle;

        public AwesomeGrid()
        {
            InitializeComponent();
            Configure();
            this.Grid.AlternatingRowsDefaultCellStyle = AlternatingRowStyle;
            this.Grid.DoubleClick += Grid_DoubleClick;
            this.Grid.KeyDown += Grid_KeyDown;
            this.Grid.PreviewKeyDown += Grid_PreviewKeyDown;
            this.Grid.ContextMenuNeeded += Grid_ContextMenuNeeded;
            this.Grid.SelectionChanged += Grid_SelectionChanged;

            _searchThrottle = new Throttler(200, () =>
                {
                    _filter = SearchTextBox.Text;
                    UpdateFilter();
                }
            );
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
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Grid.AllowUserToResizeRows = false;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Grid.BackgroundColor = SystemColors.Window;
            Grid.BorderStyle = BorderStyle.None;
            Grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            Grid.ReadOnly = true;
            Grid.RowHeadersVisible = false;
            Grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            Grid.RowTemplate.Height = /* TODO: auto-detect based on font size */ 17;
            Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid.ShowCellErrors = false;
            Grid.ShowEditingIcon = false;
            Grid.ShowRowErrors = false;
            Grid.StandardTab = true;
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
        private IList<bool> _columnSearchable;
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
                    _columnSearchable = dataColumns.Select(dc => dc.IsSearchable()).ToList();
                    #region Manage the columns based on the DataTable)
                    this.Grid.AutoGenerateColumns = false;
                    this.Grid.Columns.Clear ();
                    foreach (var dataColumn in dataColumns)
                    {
                        var gridViewColumn = new DataGridViewTextBoxColumn
                        {
                            Name = dataColumn.ColumnName,
                            DataPropertyName = dataColumn.ColumnName,
                            HeaderText = dataColumn.Caption,
                            Visible = dataColumn.IsVisible(),
                        };
                        this.Grid.Columns.Add (gridViewColumn);
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
                    if (dataRow.Matches(_columnTypes, _columnSearchable, filterChunks))
                    {
                        cloned.Rows.Add (dataRow.ItemArray);
                    }
                }
                this.Grid.DataSource = cloned;
            }
        }

        #region Implementation of ISupportInitialize

        public void BeginInit()
        {
            ((ISupportInitialize)this.Grid).BeginInit();
        }

        public void EndInit()
        {
            ((ISupportInitialize) this.Grid).EndInit();
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
    }
}
