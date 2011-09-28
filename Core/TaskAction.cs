using System;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents an action that can be performed on or around tasks.
    /// </summary>
    public class TaskAction
    {
        private readonly string _name, _caption;
        private readonly bool _isEnabled;
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAction"/> class.
        /// </summary>
        /// 
        /// <param name="name">
        /// The system name of the action.  Should be simple, such as <c>open</c>.
        /// </param>
        /// 
        /// <param name="caption">
        /// The text to show the user in the menu.  Prefix a character with <c>&amp;</c> to underline it.  Specify
        /// <c>---</c> to show a separator.
        /// </param>
        /// 
        /// <param name="isEnabled">
        /// <see langword="true"/> if the menu item should be invokable; <see langword="false" /> otherwise.
        /// </param>
        /// 
        /// <param name="action">
        /// The action to perform if the menu item is invoked.
        /// </param>
        public TaskAction(string name, string caption, bool isEnabled, Action action)
        {
            _name = name;
            _caption = caption;
            _isEnabled = isEnabled;
            _action = action;
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
        public bool IsEnabled
        {
            get { return _isEnabled; }
        }

        // TODO: add an Icon property

        /// <summary>
        /// Performs the action.
        /// </summary>
        public void Execute()
        {
            _action();
        }
    }
}
