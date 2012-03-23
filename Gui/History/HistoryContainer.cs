using System;
using System.Diagnostics;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.History
{
    public partial class HistoryContainer : UserControl, IHistoryContainer
    {
        public event EventHandler Navigated;

        private readonly AbstractHistory<IHistoryItem> _breadCrumbs = new LinkedListHistory<IHistoryItem>();
 
        public HistoryContainer()
        {
            InitializeComponent();
        }

        private void RaiseNavigated()
        {
            if (Navigated != null)
            {
                Navigated(this, EventArgs.Empty);
            }
        }

        #region IHistoryContainer Members

        public void Push(IHistoryItem historyItem)
        {
            _breadCrumbs.Go(historyItem);
            UpdateAfterForwardOrPush();
        }

        private void UpdateAfterForwardOrPush()
        {
            RemoveCurrentControl();
            if (_breadCrumbs.Count > 1)
            {
                crumbLinks.Controls.GetLast().Enabled = true;
                var pathSeparator = new Label
                {
                    Text = ">",
                    AutoSize = true,
                };
                crumbLinks.Controls.Add(pathSeparator);
            }
            _breadCrumbs.Last.Container = this;
            var linkLabel = new LinkLabel
            {
                Text = _breadCrumbs.Last.Title,
                Enabled = false,
                AutoSize = true,
            };
            var control = _breadCrumbs.Last.Control;
            linkLabel.LinkClicked += (sender, args) => PopTo(control);
            crumbLinks.Controls.Add(linkLabel);
            SetCurrentControl(control);
            RaiseNavigated();
        }

        public bool CanGoBack { get { return _breadCrumbs.CanBack; } }

        public void GoBack()
        {
            RemoveCurrentControl();
            Pop();
            SetCurrentControl(_breadCrumbs.Last.Control);
            RaiseNavigated();
        }

        public bool CanGoForward { get { return _breadCrumbs.CanForward; } }

        public void GoForward()
        {
            _breadCrumbs.Forward();
            UpdateAfterForwardOrPush();
        }

        #endregion

        private void PopTo(Control control)
        {
            // TODO: could first enumerate the _breadCrums to check if Control is there, to prevent popping to first
            Debug.Assert(_breadCrumbs.Count > 0);
            RemoveCurrentControl();
            while (_breadCrumbs.Count > 1)
            {
                var breadCrumb = _breadCrumbs.Last;
                if (ReferenceEquals(breadCrumb.Control, control))
                {
                    break;
                }
                Pop();
            }
            SetCurrentControl(control);
            RaiseNavigated();
        }

        private void Pop()
        {
            _breadCrumbs.Back();
            crumbLinks.Controls.RemoveLast(/* LinkLabel with title */);
            crumbLinks.Controls.RemoveLast(/* Label ">" */);
            crumbLinks.Controls.GetLast().Enabled = false;
        }

        private void SetCurrentControl(Control control)
        {
            mainLayout.Controls.Add(control, 0, 1);
            control.Focus();
        }

        private void RemoveCurrentControl()
        {
            if (mainLayout.Controls.Count > 1)
            {
                mainLayout.Controls.RemoveAt(1);
            }
        }
    }
}
