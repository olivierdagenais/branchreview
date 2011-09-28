using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents an action that can be performed on or around tasks.
    /// </summary>
    public class MenuAction
    {
        /// <summary>
        /// An empty, immutable list of <see cref="MenuAction"/> objects, for those times where there are no actions.
        /// </summary>
        public static readonly IList<MenuAction> EmptyList = new ReadOnlyCollection<MenuAction>(new MenuAction[] {});

        /// <summary>
        /// Special instance of <see cref="MenuAction"/> to use when a separator is needed.
        /// </summary>
        public static readonly MenuAction Separator = new MenuAction(null, null, false, null);

        /// <summary>
        /// Convenience property to determine if the current instance represents the singleton <see cref="Separator"/>.
        /// </summary>
        /// 
        /// <returns>
        /// <see langword="true"/> if the instance is the <see cref="Separator"/>;
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool IsSeparator
        {
            get
            {
                return this == Separator;
            }
        }

        private readonly string _name, _caption;
        private readonly bool _enabled;
        private readonly Action _action;
        private readonly Image _image;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAction"/> class.
        /// </summary>
        /// 
        /// <param name="name">
        /// The system name of the action.  Should be simple, such as <c>open</c>.
        /// </param>
        /// 
        /// <param name="caption">
        /// The text to show the user in the menu.  Prefix a character with <c>&amp;</c> to underline it.
        /// </param>
        /// 
        /// <param name="enabled">
        /// <see langword="true"/> if the menu item should be invokable; <see langword="false" /> otherwise.
        /// </param>
        /// 
        /// <param name="action">
        /// The action to perform if the menu item is invoked.
        /// </param>
        public MenuAction(string name, string caption, bool enabled, Action action)
        {
            _name = name;
            _caption = caption;
            _enabled = enabled;
            _action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAction"/> class.
        /// </summary>
        /// 
        /// <param name="name">
        /// The system name of the action.  Should be simple, such as <c>open</c>.
        /// </param>
        /// 
        /// <param name="caption">
        /// The text to show the user in the menu.  Prefix a character with <c>&amp;</c> to underline it.
        /// </param>
        /// 
        /// <param name="enabled">
        /// <see langword="true"/> if the menu item should be invokable; <see langword="false" /> otherwise.
        /// </param>
        /// 
        /// <param name="action">
        /// The action to perform if the menu item is invoked.
        /// </param>
        /// 
        /// <param name="image">
        /// The icon to display in the menu.
        /// </param>
        public MenuAction(string name, string caption, bool enabled, Action action, Image image)
        {
            _name = name;
            _caption = caption;
            _enabled = enabled;
            _action = action;
            _image = image;
        }

        /// <summary>
        /// Specifies the system name of the action, so that actions that be scanned programmatically.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Specifies the text displayed in the menu.
        /// </summary>
        public string Caption
        {
            get { return _caption; }
        }

        /// <summary>
        /// Specifies whether the action is enabled or not, to provide more predictable menus.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
        }

        /// <summary>
        /// The icon to display next to the text. Optional.
        /// </summary>
        public Image Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        public void Execute()
        {
            _action();
        }
    }
}
