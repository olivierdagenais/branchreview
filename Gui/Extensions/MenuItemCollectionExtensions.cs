using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.History;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    internal static class MenuItemCollectionExtensions
    {
        public static void AddSeparator(this Menu.MenuItemCollection items)
        {
            var item = new MenuItem("-");
            items.Add(item);
        }

        public static void AddActions(this Menu.MenuItemCollection items, params MenuAction[] actions)
        {
            AddActions(items, (IEnumerable<MenuAction>)actions);
        }

        public static void AddActions(this Menu.MenuItemCollection items, IEnumerable<MenuAction> actions)
        {
            foreach (var menuAction in actions)
            {
                items.AddAction(menuAction);
            }
        }

        public static void AddAction(this Menu.MenuItemCollection items, MenuAction action)
        {
            MenuItem item;
            if (action.IsSeparator)
            {
                items.AddSeparator();
            }
            else
            {
                item = new MenuItem
                {
                    Name = action.Name,
                    Text = action.Caption,
                    Enabled = action.Enabled,
                    // TODO: see if MenuItem supports this: Image = action.Image,
                };
                // TODO: execute safely (i.e. catch exceptions to report errors) and maybe asynchronously
                item.Click += (clickSender, eventArgs) =>
                {
                    var result = action.Execute();
                    if (result)
                    {
                        var menuItem = (MenuItem) clickSender;
                        var parentMenu = menuItem.GetContextMenu();
                        Debug.Assert(parentMenu != null);
                        var parentControl = parentMenu.SourceControl;
                        while (parentControl != null && !(parentControl is IHistoryContainer))
                        {
                            parentControl = parentControl.Parent;
                        }
                        if (parentControl != null)
                        {
                            var historyContainer = (IHistoryContainer) parentControl;
                            var currentHistoryItem = historyContainer.Current;
                            currentHistoryItem.Reload();
                        }
                    }
                };
                items.Add(item);
            }
            
        }

        public static void InvokeFirstMenuItem(this Menu.MenuItemCollection items)
        {
            if (items.Count > 0)
            {
                items[0].PerformClick();
            }
        }
    }
}
