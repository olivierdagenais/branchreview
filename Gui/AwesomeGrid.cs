using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class AwesomeGrid : UserControl, ISupportInitialize
    {
        private static readonly DataGridViewCellStyle AlternatingRowStyle =
            new DataGridViewCellStyle { BackColor = Color.WhiteSmoke };

        public event DataGridViewRowContextMenuStripNeededEventHandler RowContextMenuStripNeeded;
        public event EventHandler SelectionChanged;

        public AwesomeGrid()
        {
            InitializeComponent();
            Configure();
            this.Grid.AlternatingRowsDefaultCellStyle = AlternatingRowStyle;
            this.Grid.DoubleClick += Grid_DoubleClick;
            this.Grid.KeyDown += Grid_KeyDown;
            this.Grid.PreviewKeyDown += Grid_PreviewKeyDown;
            this.Grid.RowContextMenuStripNeeded += Grid_RowContextMenuStripNeeded;
            this.Grid.SelectionChanged += Grid_SelectionChanged;
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

        void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        void Grid_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
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
            Grid.RowTemplate.Height = 23;
            Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid.ShowCellErrors = false;
            Grid.ShowEditingIcon = false;
            Grid.ShowRowErrors = false;
            Grid.StandardTab = true;
        }

        void Grid_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            if (RowContextMenuStripNeeded != null)
            {
                RowContextMenuStripNeeded(sender, e);
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

        internal struct FilterChunk
        {
            public string Text;
            public int? Integer;
            public FilterChunk(string text)
            {
                Text = text;
                int m;
                Integer = Int32.TryParse (text, out m) ? (int?) m : null;
            }
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
                    this.Grid.AutoGenerateColumns = false;
                    this.Grid.Columns.Clear ();
                    foreach (var dataColumn in dataColumns)
                    {
                        var gridViewColumn = new DataGridViewTextBoxColumn
                        {
                            Name = dataColumn.ColumnName,
                            DataPropertyName = dataColumn.ColumnName,
                            HeaderText = dataColumn.Caption,
                        };
                        this.Grid.Columns.Add (gridViewColumn);
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
                    if (Matches(dataRow, _columnTypes, filterChunks))
                    {
                        cloned.Rows.Add (dataRow.ItemArray);
                    }
                }
                this.Grid.DataSource = cloned;
            }
        }

        internal static bool ContainsInvariantIgnoreCase(string hayStack, string needle)
        {
            return hayStack.IndexOf (needle, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        internal static bool Matches(DataRow row, IList<Type> columnTypes, IList<FilterChunk> filterChunks)
        {
            foreach (var chunk in filterChunks)
            {
                bool chunkMatchesColumn = false;
                for (var c = 0; c < columnTypes.Count; c++)
                {
                    var columnType = columnTypes[c];
                    var cellValue = row[c];

                    if (columnType == typeof(int))
                    {
                        if (chunk.Integer.HasValue)
                        {
                            var v = (int) cellValue;
                            chunkMatchesColumn = v == chunk.Integer.Value;
                        }
                    }
                    else
                    {
                        if (cellValue != null)
                        {
                            var v = cellValue.ToString();
                            chunkMatchesColumn = ContainsInvariantIgnoreCase(v, chunk.Text);
                        }
                    }

                    if (chunkMatchesColumn)
                    {
                        break;
                    }
                }

                if (!chunkMatchesColumn) return false;
            }

            return true;
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
            _filter = SearchTextBox.Text;
            UpdateFilter();
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            SearchTextBox.SelectAll();
        }
    }
}
