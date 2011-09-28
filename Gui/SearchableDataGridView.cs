using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class SearchableDataGridView : DataGridView
    {
        private static readonly DataGridViewCellStyle AlternatingRowStyle =
            new DataGridViewCellStyle { BackColor = Color.WhiteSmoke };

        public SearchableDataGridView()
        {
            this.AlternatingRowsDefaultCellStyle = AlternatingRowStyle;
            this.DataBindingComplete += SearchableDataGridView_DataBindingComplete;
            this.Resize += SearchableDataGridView_Resize;
            this.RowPrePaint += SearchableDataGridView_RowPrePaint;
        }

        void SearchableDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // http://stackoverflow.com/questions/285829/
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        void SearchableDataGridView_Resize(object sender, EventArgs e)
        {
            AutoSizeColumns();
        }

        #region http://social.msdn.microsoft.com/Forums/en/winforms/thread/ef369cf3-58e9-4997-acc3-87a51d83011c
        internal void InvokeContextMenu()
        {
            var selectedCells = SelectedCells.Cast<DataGridViewCell>();
            var cell = selectedCells.FirstOrDefault(dataGridViewCell => dataGridViewCell.Displayed);
            if (cell != null)
            {
                var strip = cell.ContextMenuStrip
                            ?? cell.OwningRow.ContextMenuStrip
                            ?? cell.OwningColumn.ContextMenuStrip;
                if (strip != null)
                {
                    var rect = GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                    var bottomLeft = new Point(rect.Left, rect.Bottom);
                    var screenCoordinates = PointToScreen(bottomLeft);
                    strip.Show(screenCoordinates);
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (( e.Shift && e.KeyCode == Keys.F10 ) || e.KeyCode == Keys.Apps)
            {
                // pop context menu on Shift+F10 or context key (i.e. "Apps")
                e.Handled = true;
                e.SuppressKeyPress = false;
                InvokeContextMenu();
            }
            else
            {
                base.OnKeyUp(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var info = this.HitTest(e.X, e.Y);
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    var row = Rows[info.RowIndex];
                    if (!row.Selected)
                    {
                        // Cause right-click to select cell
                        e = new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta);
                    }
                }
            }
            base.OnMouseDown(e);
        }
        #endregion

        void SearchableDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AutoSizeColumns();
        }

        private void AutoSizeColumns()
        {
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var dataGridViewColumns = this.Columns.Cast<DataGridViewColumn>().ToList();
            var widths = dataGridViewColumns.Select(c => c.Width).ToList();
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            double totalWidths = widths.Aggregate(0, (a, b) => a + b);
            var clientWidth = this.ClientSize.Width;
            var multiplier = clientWidth/totalWidths;
            for (var c = 0; c < widths.Count; c++ )
            {
                dataGridViewColumns[c].Width = (int) (widths[c] * multiplier);
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

    }
}
