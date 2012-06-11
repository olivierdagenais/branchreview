using SoftwareNinjas.BranchAndReviewTools.Gui.WeifenLuo.WinFormsUI.Docking;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    /// <remarks>
    /// Adapted from <see href="http://sourceforge.net/projects/dockpanelsuite/forums/forum/402316/topic/1526067"/>.
    /// </remarks>
    public static class DockPanelExtensions
    {
        public static IDockContent GetFirstDocument(this DockPanel dockPanel)
        {
            if (dockPanel.ActivePane == null)
                return null;
            if (dockPanel.ActivePane.Contents.Count == 0)
                return null;

            return dockPanel.ActivePane.Contents[0];
        }

        // Returns the last DockContent in ActivePane:
        public static IDockContent GetLastDocument(this DockPanel dockPanel)
        {
            if (dockPanel.ActivePane == null)
                return null;
            if (dockPanel.ActivePane.Contents.Count == 0)
                return null;

            return dockPanel.ActivePane.Contents[dockPanel.ActivePane.Contents.Count - 1];
        }

        // Returns the previous DockContent in ActivePane, prior to the ActiveContent:
        public static IDockContent GetPreviousDocument(this DockPanel dockPanel)
        {
            if (dockPanel.ActivePane == null)
                return null;
            bool getPreviousDocument = false;
            for (int x = dockPanel.ActivePane.Contents.Count - 1; x > -1; x--)
            {
                DockContent dockContent = (DockContent)dockPanel.ActivePane.Contents[x];
                if (dockContent == dockPanel.ActivePane.ActiveContent)
                    getPreviousDocument = true;
                else if (getPreviousDocument && !dockContent.IsHidden)
                    return dockContent;
            }
            return null;
        }

        // Returns the next DockContent in ActivePane, following the ActiveContent:
        public static IDockContent GetNextDocument(this DockPanel dockPanel)
        {
            if (dockPanel.ActivePane == null)
                return null;
            bool getNextDocument = false;
            for (int x = 0; x < dockPanel.ActivePane.Contents.Count; x++)
            {
                DockContent dockContent = (DockContent)dockPanel.ActivePane.Contents[x];
                if (dockContent == dockPanel.ActivePane.ActiveContent)
                    getNextDocument = true;
                else if (getNextDocument && !dockContent.IsHidden)
                    return dockContent;
            }
            return null;
        }
    }
}
