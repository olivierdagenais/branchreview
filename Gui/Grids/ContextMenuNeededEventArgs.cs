using System;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Grids
{
    public class ContextMenuNeededEventArgs : EventArgs
    {
        public ContextMenu ContextMenu { get; set; }
    }
}