using System;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class ContextMenuNeededEventArgs : EventArgs
    {
        public ContextMenu ContextMenu { get; set; }
    }
}