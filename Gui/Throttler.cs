using System;
using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class Throttler
    {
        private readonly Timer _timer;
        private readonly Action _action;

        public Throttler(int intervalMilliseconds, Action action)
        {
            _timer = new Timer {Interval = intervalMilliseconds};
            _timer.Tick += Tick;
            _action = action;
        }

        public int IntervalMilliseconds
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        void Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _action();
        }

        public void Fire()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
            }
            Application.DoEvents();
        }
    }
}
