using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    internal static class ControlExtensions
    {
        private static readonly string ApplicationHome = "Software".CombinePath(
            Assembly.GetCallingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
            Assembly.GetCallingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product
        );

        public static T LoadSetting<T>(this Control control, Expression<Func<T>> propertyExpression, T defaultValue)
        {
            return LoadSetting(control, propertyExpression, defaultValue, null);
        }

        public static T LoadSetting<T>(this Control control, Expression<Func<T>> propertyExpression, T defaultValue, Func<Object, T> converter)
        {
            using (var sk = Registry.CurrentUser.CreateSubKey(ApplicationHome))
            {
                Debug.Assert(sk != null);

                var name = propertyExpression.DetermineName();
                var settingName = control.Name + name;
                var rawValue = sk.GetValue(settingName, defaultValue);
                T value;
                if (converter != null)
                {
                    value = converter(rawValue);
                }
                else
                {
                    value = (T) rawValue;
                }
                return value;
            }
        }

        public static void SaveSetting<T>(this Control control, Expression<Func<T>> propertyExpression)
        {
            using (var sk = Registry.CurrentUser.CreateSubKey(ApplicationHome))
            {
                Debug.Assert(sk != null);

                var name = propertyExpression.DetermineName();
                var settingName = control.Name + name;
                var func = propertyExpression.Compile();
                var value = func();
                sk.SetValue(settingName, value);
            }
        }

        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void ExecuteLater(this Control control, int milliseconds, Action action)
        {
            var delayedWorker = new Timer {Interval = milliseconds};
            delayedWorker.Tick += (s, ea) =>
            {
                delayedWorker.Stop();
                control.InvokeIfRequired(action);
            };
            delayedWorker.Start();
        }

        public static void StartTask(this Control control, Action backgroundWork, Action<Task> guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => control.InvokeIfRequired(() => guiContinuation(t)));
        }

        public static void StartTask<T>(this Control control, Func<T> backgroundWork, Action<Task<T>> guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => control.InvokeIfRequired(() => guiContinuation(t)));
        }

        public static void StartTask(this Control control, Action backgroundWork, Action guiContinuation, Action<AggregateException> guiFault)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinueWithFault(control, t, guiContinuation, guiFault));
        }

        public static void StartTask(this Control control, Action backgroundWork, Action guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinueWithFault(control, t, guiContinuation, null));
        }

        public static void StartTask<T>(this Control control, Func<T> backgroundWork, Action<T> guiContinuation, Action<AggregateException> guiFault)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinueWithFault(control, t, () => guiContinuation(t.Result), guiFault));
        }

        public static void StartTask<T>(this Control control, Func<T> backgroundWork, Action<T> guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinueWithFault(control, t, () => guiContinuation(t.Result), null));
        }

        internal static void GuiContinueWithFault<T>(Control control, T task, Action guiContinuation, Action<AggregateException> guiFault) where T : Task
        {
            if (task.IsFaulted)
            {
                var e = task.Exception;
                if (e != null)
                {
                    if (guiFault != null)
                    {
                        control.InvokeIfRequired(() => guiFault(e));
                    }
                    else
                    {
                        control.ToDo("Log this hidden fault somewhere useful: {0}", e.Message);
                    }
                }
            }
            else
            {
                control.InvokeIfRequired(guiContinuation);
            }
        }
    }
}
