using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Grids
{
    public class AccessibleDataGridView : DataGridView, IAccessibleGrid
    {
        public event ContextMenuNeededEventHandler ContextMenuNeeded;

        private readonly Throttler _resizeThrottle;
        private readonly ColumnCollection _columnCollectionWrapper;
        private readonly ItemCollection _rowCollectionWrapper;
        private readonly SelectedItemCollection _selectedRowCollectionWrapper;

        private void InvokeContextMenuStripNeeded(ContextMenuNeededEventArgs e)
        {
            if (ContextMenuNeeded != null)
            {
                ContextMenuNeeded(this, e);
            }
        }

        public AccessibleDataGridView()
        {
            _resizeThrottle = new Throttler(50, AutoSizeColumns);
            _columnCollectionWrapper = new ColumnCollection(Columns);
            _rowCollectionWrapper = new ItemCollection(Rows);
            _selectedRowCollectionWrapper = new SelectedItemCollection(this);

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AutoGenerateColumns = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BackgroundColor = SystemColors.Window;
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            ReadOnly = true;
            RowHeadersVisible = false;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            RowTemplate.Height = /* TODO: auto-detect based on font size */ 17;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ShowCellErrors = false;
            ShowEditingIcon = false;
            ShowRowErrors = false;
            StandardTab = true;

            this.KeyDown += Grid_KeyDown;
            this.DataBindingComplete += Grid_DataBindingComplete;
            this.Resize += Grid_Resize;
            this.GotFocus += Grid_GotFocus;
            this.LostFocus += Grid_LostFocus;
            this.RowPrePaint += Grid_RowPrePaint;
            Grid_LostFocus(this, null);
        }

        void Grid_LostFocus(object sender, EventArgs e)
        {
            this.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
            this.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
        }

        void Grid_GotFocus(object sender, EventArgs e)
        {
            this.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            this.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
        }

        void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37 || e.KeyValue == 39) // left or right arrow keys
            {
                // swallow keystroke to prevent flicker
                e.SuppressKeyPress = true;
                return;
            }
            if (e.Modifiers == Keys.None)
            {
                if (e.KeyValue == 36) // Home
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("^{HOME}"); // Control+Home
                    return;
                }
                if (e.KeyValue == 35) // End
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("^{END}"); // Control+End
                    return;
                }
            }
        }

        void Grid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // http://stackoverflow.com/questions/285829/
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        void Grid_Resize(object sender, EventArgs e)
        {
            _resizeThrottle.Fire();
        }

        #region http://social.msdn.microsoft.com/Forums/en/winforms/thread/ef369cf3-58e9-4997-acc3-87a51d83011c
        internal void InvokeContextMenu(Point menuLocation)
        {
            var args = new ContextMenuNeededEventArgs();
            InvokeContextMenuStripNeeded(args);
            if (args.ContextMenu != null)
            {
                args.ContextMenu.Show(this, menuLocation);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (( e.Shift && e.KeyCode == Keys.F10 ) || e.KeyCode == Keys.Apps)
            {
                // pop context menu on Shift+F10 or context key (i.e. "Apps")
                e.Handled = true;
                e.SuppressKeyPress = false;
                var selectedCells = SelectedCells.Cast<DataGridViewCell>();
                var cell = selectedCells.FirstOrDefault(dataGridViewCell => dataGridViewCell.Displayed);
                if (cell != null)
                {
                    var rect = GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                    var bottomLeft = new Point(rect.Left, rect.Bottom);
                    InvokeContextMenu(bottomLeft);
                }
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
                        base.OnMouseDown(e);
                    }

                    InvokeContextMenu(PointToClient(Cursor.Position));
                    return;
                }
            }
            base.OnMouseDown(e);
        }
        #endregion

        void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AutoSizeColumns();
        }

        internal static IList<int> AdjustWidths(IList<int> widths, int maxWidth)
        {
            var sum = (double) widths.Sum();
            if (sum <= maxWidth)
            {
                return widths.ToList();
            }
            var numberOfWidths = widths.Count;
            var result = new List<int>(numberOfWidths);
            var largeWidths = new List<int>(numberOfWidths);
            foreach (var width in widths)
            {
                var fraction = width / sum;
                if (fraction < 0.2)
                {
                    result.Add(width);
                    largeWidths.Add(0);
                }
                else
                {
                    result.Add(0);
                    largeWidths.Add(width);
                }
            }

            var sumLargeWidths = (double) largeWidths.Sum();
            maxWidth -= result.Sum();
            for (int i = 0; i < numberOfWidths; i++)
            {
                if (result[i] == 0)
                {
                    var currentWidth = widths[i] * maxWidth / sumLargeWidths;
                    result[i] = (int) currentWidth;
                }
            }
            return result;
        }

        private void AutoSizeColumns()
        {
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var dataGridViewColumns = this.Columns.Cast<DataGridViewColumn>().ToList();
            var clientWidth = this.ClientSize.Width;
            if (this.VerticalScrollBar.Visible)
            {
                clientWidth -= this.VerticalScrollBar.Size.Width;
            }
            var widths = dataGridViewColumns.Where(c => c.Visible).Select(c => c.Width).ToList();
            var adjustedWidths = AdjustWidths(widths, clientWidth);
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            double totalWidths = adjustedWidths.Aggregate(0, (a, b) => a + b);
            var multiplier = clientWidth/totalWidths;
            for (int c = 0, v = 0; c < adjustedWidths.Count; c++ )
            {
                while (!dataGridViewColumns[v].Visible)
                {
                    v++;
                }
                dataGridViewColumns[v].Width = (int) (adjustedWidths[c] * multiplier);
                v++;
            }
        }

        #region IAccessibleGrid-related wrappers

        private class ColumnCollection : UnsupportedList<IGridColumn>
        {
            private readonly DataGridViewColumnCollection _base;

            public ColumnCollection(DataGridViewColumnCollection columnCollection)
            {
                _base = columnCollection;
            }

            public override void Insert(int index, IGridColumn item)
            {
                _base.Insert(index, GridColumn.ToNativeColumn(item));
            }

            public override void RemoveAt(int index)
            {
                _base.RemoveAt(index);
            }

            public override IGridColumn this[int index]
            {
                get
                {
                    return GridColumn.FromNativeColumn(_base[index]);
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override void Add(IGridColumn item)
            {
                _base.Add(GridColumn.ToNativeColumn(item));
            }

            public override void Clear() { _base.Clear(); }

            public override int Count { get { return _base.Count; } }

            public override bool IsReadOnly { get { return _base.IsReadOnly; } }

            public override IEnumerator<IGridColumn> GetEnumerator()
            {
                return _base.Cast<DataGridViewColumn>().Select(GridColumn.FromNativeColumn).GetEnumerator();
            }
        }

        private class ItemCollection : UnsupportedList<IGridItem>
        {
            private readonly DataGridViewRowCollection _base;

            public ItemCollection(DataGridViewRowCollection rowCollection)
            {
                _base = rowCollection;
            }

            public override IGridItem this[int index]
            {
                get
                {
                    return GridItem.FromNativeItem(_base[index]);
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override int Count { get { return _base.Count; } }

            public override bool IsReadOnly { get { return true; } }

            public override IEnumerator<IGridItem> GetEnumerator()
            {
                return _base.Cast<DataGridViewRow>().Select(GridItem.FromNativeItem).GetEnumerator();
            }
        }

        private class SelectedItemCollection : UnsupportedList<IGridItem>
        {
            private readonly DataGridView _base;

            public SelectedItemCollection(DataGridView dataGridView)
            {
                _base = dataGridView;
            }

            public override IGridItem this[int index]
            {
                get
                {
                    return GridItem.FromNativeItem(_base.SelectedRows[index]);
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override void Clear()
            {
                _base.ClearSelection();
            }

            public override int Count { get { return _base.SelectedRows.Count; } }

            public override bool IsReadOnly { get { return true; } }

            public override IEnumerator<IGridItem> GetEnumerator()
            {
                return _base.SelectedRows.Cast<DataGridViewRow>().Select(GridItem.FromNativeItem).GetEnumerator();
            }
        }

        private class GridItem : IGridItem
        {
            private readonly DataGridViewRow _nativeItem;

            private GridItem(DataGridViewRow nativeItem)
            {
                _nativeItem = nativeItem;
            }

            #region IGridItem Members

            public DataRow DataRow { get; private set; }

            public bool Selected
            {
                get { return _nativeItem.Selected; }
                set { _nativeItem.Selected = value; }
            }

            #endregion

            public static IGridItem FromNativeItem(DataGridViewRow nativeItem)
            {
                var dataRowView = (DataRowView) nativeItem.DataBoundItem;
                var gridItem = new GridItem(nativeItem)
                {
                    DataRow = dataRowView.Row,
                };
                return gridItem;
            }
        }

        private class GridColumn : IGridColumn
        {
            #region IGridColumn Members

            public string Name { get; set; }

            public string Caption { get; set; }

            public bool Visible { get; set; }

            #endregion

            public static DataGridViewColumn ToNativeColumn(IGridColumn gridColumn)
            {
                var ourGridColumn = (GridColumn) gridColumn;
                var nativeColumn = new DataGridViewTextBoxColumn
                {
                    Name = ourGridColumn.Name,
                    DataPropertyName = ourGridColumn.Name,
                    HeaderText = ourGridColumn.Caption,
                    Visible = ourGridColumn.Visible,
                };
                return nativeColumn;
            }

            public static IGridColumn FromNativeColumn(DataGridViewColumn nativeColumn)
            {
                var gridColumn = new GridColumn
                {
                    Name = nativeColumn.Name,
                    Caption = nativeColumn.HeaderText,
                    Visible = nativeColumn.Visible,
                };
                return gridColumn;
            }
        }

        #endregion

        #region IAccessibleGrid members

        public IGridColumn CreateGridColumn(string name, string caption, bool visible)
        {
            var gridColumn = new GridColumn
            {
                Name = name,
                Caption = caption,
                Visible = visible,
            };
            return gridColumn;
        }

        IList<IGridColumn> IAccessibleGrid.Columns { get { return _columnCollectionWrapper; } }

        IList<IGridItem> IAccessibleGrid.Rows { get { return _rowCollectionWrapper; } }

        IList<IGridItem> IAccessibleGrid.SelectedRows { get { return _selectedRowCollectionWrapper; } }

        DataTable IAccessibleGrid.DataSource
        {
            get { return (DataTable) DataSource; }
            set { DataSource = value; }
        }

        bool IAccessibleGrid.MultiSelect
        { 
            get { return MultiSelect; }
            set { MultiSelect = value; }
        }
        #endregion
    }
}
