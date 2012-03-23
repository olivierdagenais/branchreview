using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Grids
{
    public interface IAccessibleGrid
    {
        event ContextMenuNeededEventHandler ContextMenuNeeded;
        event EventHandler SelectionChanged;

        IGridColumn CreateGridColumn(string name, string caption, bool visible);
        IList<IGridColumn> Columns { get; }
        DataTable DataSource { get; set; }
        bool MultiSelect { get; set; }
        IList<IGridItem> Rows { get; }
        IList<IGridItem> SelectedRows { get; }
    }
}
