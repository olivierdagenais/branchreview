using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class SearchableDataGridView : DataGridView
    {
        public SearchableDataGridView()
        {
            this.DataBindingComplete += SearchableDataGridView_DataBindingComplete;
        }

        void SearchableDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var dataGridViewColumns = this.Columns.Cast<DataGridViewColumn>().ToList();
            var widths = dataGridViewColumns.Select(c => c.Width).ToList();
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (var c = 0; c < widths.Count; c++ )
            {
                dataGridViewColumns[c].Width = widths[c];
            }
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
                    var dataColumns = _dataTable.Columns.Cast<DataColumn> ();
                    _columnTypes = dataColumns.Select(dc => dc.DataType).ToList ();
                    #region Manage the columns based on the DataTable
                    this.AutoGenerateColumns = false;
                    this.Columns.Clear ();
                    foreach (var dataColumn in dataColumns)
                    {
                        var gridViewColumn = new DataGridViewTextBoxColumn
                        {
                            Name = dataColumn.ColumnName,
                            DataPropertyName = dataColumn.ColumnName,
                            HeaderText = dataColumn.Caption,
                        };
                        this.Columns.Add (gridViewColumn);
                    }
                    #endregion
                }
                this.DataSource = _dataTable;
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set 
            {
                _filter = value;
                if (_dataTable != null && _filter != null)
                {
                    var cloned = _dataTable.Clone ();
                    var filterParts = _filter.Split(' ');
                    var filterChunks = filterParts.Select (p => new FilterChunk (p)).ToList();
                    foreach (DataRow dataRow in _dataTable.Rows)
                    {
                        if (Matches(dataRow, _columnTypes, filterChunks))
                        {
                            cloned.Rows.Add (dataRow.ItemArray);
                        }
                    }
                    this.DataSource = cloned;
                }
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
                        var v = (string) cellValue;
                        chunkMatchesColumn = ContainsInvariantIgnoreCase(v, chunk.Text);
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

    }
}
