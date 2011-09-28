using System.Collections.Generic;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    internal static class ToolStripItemCollectionExtensions
    {
        public static void AddSeparator(this ToolStripItemCollection items)
        {
            var item = new ToolStripSeparator();
            items.Add(item);
        }

        public static void AddActions(this ToolStripItemCollection items, params MenuAction[] actions)
        {
            AddActions(items, (IEnumerable<MenuAction>)actions);
        }

        public static void AddActions(this ToolStripItemCollection items, IEnumerable<MenuAction> actions)
        {
            foreach (var menuAction in actions)
            {
                items.AddAction(menuAction);
            }
        }

        public static void AddAction(this ToolStripItemCollection items, MenuAction action)
        {
            ToolStripItem item;
            if (action.IsSeparator)
            {
                items.AddSeparator();
            }
            else
            {
                item = new ToolStripMenuItem
                {
                    Name = action.Name,
                    Text = action.Caption,
                    Enabled = action.Enabled,
                    Image = action.Image,
                };
                item.Click += (clickSender, eventArgs) => action.Execute();
                items.Add(item);
            }
            
        }

        public static void InvokeFirstMenuItem(this ToolStripItemCollection items)
        {
            if (items.Count > 0)
            {
                items[0].PerformClick();
            }
        }
}
}
