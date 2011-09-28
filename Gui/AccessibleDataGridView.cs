using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class AccessibleDataGridView : DataGridView
    {
        public AccessibleDataGridView()
        {
            this.DataBindingComplete += Grid_DataBindingComplete;
            this.Resize += Grid_Resize;
            this.RowPrePaint += Grid_RowPrePaint;
        }

        void Grid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // http://stackoverflow.com/questions/285829/
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        void Grid_Resize(object sender, EventArgs e)
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

        void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AutoSizeColumns();
        }

        internal static IList<int> AdjustWidths(IList<int> widths, int maxWidth)
        {
            var result = new List<int>(widths.Count);
            foreach (var width in widths)
            {
                var currentWidth = Math.Min(maxWidth, width);
                result.Add(currentWidth);
                maxWidth -= currentWidth;
            }
            return result;
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
    }
}
