using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class AccessibleDataGridView : DataGridView
    {
        public event ContextMenuNeededEventHandler ContextMenuNeeded;

        private readonly Throttler _resizeThrottle;

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
    }
}
