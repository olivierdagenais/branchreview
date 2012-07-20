using System;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class Throttler
    {
        private readonly Timer _timer;
        private readonly Control _control;
        private readonly Action _action;

        public Throttler(Control control, int intervalMilliseconds, Action action)
        {
            _timer = new Timer {Interval = intervalMilliseconds};
            _control = control;
            _action = action;
            _timer.Tick += Tick;
        }

        public int IntervalMilliseconds
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        void Tick(object sender, EventArgs e)
        {
            _control.InvokeIfRequired(() =>
            {
                _timer.Stop();
                _action();
            });
        }

        public void Fire()
        {
            _control.InvokeIfRequired(() =>
            {
                if (!_timer.Enabled)
                {
                    _timer.Start();
                }
            });
        }
    }
}
