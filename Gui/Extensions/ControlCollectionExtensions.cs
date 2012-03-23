using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class ControlCollectionExtensions
    {
        public static Control RemoveLast(this Control.ControlCollection collection)
        {
            Control result = null;
            var lastItemIndex = collection.Count - 1;
            if (lastItemIndex >= 0)
            {
                result = collection[lastItemIndex];
                collection.RemoveAt(lastItemIndex);
            }
            return result;
        }

        public static Control GetLast(this Control.ControlCollection collection)
        {
            Control result = null;
            var lastItemIndex = collection.Count - 1;
            if (lastItemIndex >= 0)
            {
                result = collection[lastItemIndex];
            }
            return result;
        }
    }
}
