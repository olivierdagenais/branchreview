using System;
using System.Windows.Forms;

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
            if (_control.InvokeRequired)
            {
                _control.Invoke(new Action<object, EventArgs>(Tick));
            }
            else
            {
                _timer.Stop();
                _action();
            }
        }

        public void Fire()
        {
            if (_control.InvokeRequired)
            {
                _control.Invoke(new Action(Fire));
            }
            else
            {
                if (!_timer.Enabled)
                {
                    _timer.Start();
                }
            }
            Application.DoEvents();
        }
    }
}
