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
    public class AccessibleListView : ListView, ISupportInitialize
    {
        public event ContextMenuNeededEventHandler ContextMenuNeeded;

        private class ColumnComparer : IComparer<object>
        {
            public int Compare(object x, object y)
            {
                if (x is DBNull || x == null)
                {
                    if (y is DBNull || y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y is DBNull || y == null)
                {
                    return 1;
                }
                if (x is IComparable)
                {
                    var comparableX = (IComparable) x;
                    return comparableX.CompareTo(y);
                }
                if (y is IComparable)
                {
                    var comparableY = (IComparable) y;
                    return -1 * comparableY.CompareTo(x);
                }
                throw new ArgumentException("Neither x nor y were IComparable");
            }
        }

        private static readonly ColumnComparer TheColumnComparer = new ColumnComparer();

        private DataTable _dataSource;
        private int _sortingColumn;
        private SortOrder _sortOrder = SortOrder.Ascending;
        private readonly ToolTip _toolTip = new ToolTip();

        public AccessibleListView()
        {
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.View = View.Details;

            this.ColumnClick += AccessibleListView_ColumnClick;
            this.Resize += AccessibleListView_Resize;
            this.ItemMouseHover += AccessibleListView_ItemMouseHover;
        }

        void AccessibleListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            var position = PointToClient(Cursor.Position);
            var subItem = e.Item.GetSubItemAt(position.X, position.Y);
            if (subItem != null)
            {
                _toolTip.SetToolTip(this, subItem.Text);
                _toolTip.Active = true;
            }
        }

        void AccessibleListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_dataSource != null)
            {
                if (e.Column >= _dataSource.Columns.Count)
                {
                    // skip last dummy column
                    return;
                }
            }
            if (_sortingColumn == e.Column)
            {
                _sortOrder = _sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _sortingColumn = e.Column;
                _sortOrder = SortOrder.Ascending;
            }
            DataBind();
        }

        private void DataBind()
        {
            using (new BeforeAfter(BeginUpdate, EndUpdate))
            {
                this.Items.Clear();
                if (_dataSource != null)
                {
                    // TODO: We might be better off never creating the hidden columns' headers in the first place
                    var columns = _dataSource.Columns.Count;
                    var visibleColumns = Columns.Cast<AccessibleColumnHeader>().Where(c => c.Visible).ToList();
                    if (Columns.Count != visibleColumns.Count)
                    {
                        Columns.Clear();
                        foreach (var accessibleColumnHeader in visibleColumns)
                        {
                            Columns.Add(accessibleColumnHeader);
                        }
                    }
                    var dataRows = _dataSource.AsEnumerable();
                    var sourceColumn = MapToSourceColumn(_dataSource.Columns.Cast<DataColumn>(), _sortingColumn);
                    OrderedEnumerableRowCollection<DataRow> sortedRows;
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        sortedRows = dataRows.OrderBy(r => r[sourceColumn], TheColumnComparer);
                    }
                    else
                    {
                        sortedRows = dataRows.OrderByDescending(r => r[sourceColumn], TheColumnComparer);
                    }
                    var row = 0;
                    foreach (var dataRow in sortedRows)
                    {
                        var strings = new List<string>(columns);
                        for (var c = 0; c < columns; c++)
                        {
                            if (_dataSource.Columns[c].IsVisible())
                            {
                                var value = dataRow.ItemArray[c];
                                strings.Add(value == null ? String.Empty : value.ToString());
                            }
                        }
                        var listViewItem = new ListViewItem(strings.ToArray())
                        {
                            BackColor = (row % 2 == 0) ? this.BackColor : AlternatingBackColor,
                            Tag = dataRow,
                        };
                        row++;
                        this.Items.Add(listViewItem);
                    }
                }
            }
            if (this.Items.Count > 0)
            {
                this.SelectedIndices.Add(0);
            }
        }

        internal static int MapToSourceColumn(IEnumerable<DataColumn> columns, int clickedColumn)
        {
            var result = 0;
            var e = columns.GetEnumerator();
            var i = 0;
            while (e.MoveNext())
            {
                var column = e.Current;
                if (!column.IsVisible())
                {
                    result++;
                }
                if (i == clickedColumn)
                {
                    break;
                }
                result++;
                i++;
            }
            return result;
        }


        public Color AlternatingBackColor { get; set; }

        public DataTable DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                DataBind();
                AutoSizeColumns();
            }
        }

        void AccessibleListView_Resize(object sender, EventArgs e)
        {
            AutoSizeColumns();
        }

        private void InvokeContextMenuStripNeeded(ContextMenuNeededEventArgs e)
        {
            if (ContextMenuNeeded != null)
            {
                ContextMenuNeeded(this, e);
            }
        }

        private void InvokeContextMenu(Point screenLocation)
        {
            var args = new ContextMenuNeededEventArgs();
            InvokeContextMenuStripNeeded(args);
            if (args.ContextMenu != null)
            {
                args.ContextMenu.Show(this, screenLocation);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (( e.Shift && e.KeyCode == Keys.F10 ) || e.KeyCode == Keys.Apps)
            {
                // pop context menu on Shift+F10 or context key (i.e. "Apps")
                e.Handled = true;
                e.SuppressKeyPress = false;
                var focusedItem = this.FocusedItem;
                if (focusedItem != null)
                {
                    var rect = focusedItem.Bounds;
                    var bottomLeft = new Point(rect.Left, rect.Bottom);
                    InvokeContextMenu(bottomLeft);
                }
            }
            else
            {
                base.OnKeyUp(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                InvokeContextMenu(e.Location);
            }
            else
            base.OnMouseUp(e);
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
            if (this.Columns.Count == 0)
            {
                return;
            }
            using (new BeforeAfter(BeginUpdate, EndUpdate))
            using (new BeforeAfter(() => Resize -= AccessibleListView_Resize, 
                () => Resize += AccessibleListView_Resize))
            {
                this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                // exclude the last column from these computations
                var columnHeaders = this.Columns.Cast<ColumnHeader>().Take(this.Columns.Count - 1).ToList();
                var clientWidth = this.ClientSize.Width;
                var widths = columnHeaders.Select(c => c.Width).ToList();
                var adjustedWidths = AdjustWidths(widths, clientWidth);
                double totalWidths = adjustedWidths.Aggregate(0, (a, b) => a + b);
                var multiplier = clientWidth/totalWidths;
                for (var c = 0; c < adjustedWidths.Count; c++ )
                {
                    columnHeaders[c].Width = (int) (adjustedWidths[c] * multiplier);
                }
                this.Columns[columnHeaders.Count].Width = 0;
            }
        }

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
            // do nothing
        }

        void ISupportInitialize.EndInit()
        {
            // do nothing
        }

        #endregion
    }
}
