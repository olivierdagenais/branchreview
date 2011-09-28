using System;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class ContextMenuStripNeededEventArgs : EventArgs
    {
        public ContextMenuStrip ContextMenuStrip { get; set; }
    }
}