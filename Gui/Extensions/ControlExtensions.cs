using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
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

        public static void ExecuteLater(this Control control, int milliseconds, Action action)
        {
            var delayedWorker = new Timer {Interval = milliseconds};
            delayedWorker.Tick += (s, ea) =>
            {
                delayedWorker.Stop();
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action(action));
                }
                else
                {
                    action();
                }
            };
            delayedWorker.Start();
        }

        public static void StartTask(this Control control, Action backgroundWork, Action<Task> guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinue(control, t, guiContinuation));
        }

        public static void StartTask<T>(this Control control, Func<T> backgroundWork, Action<Task<T>> guiContinuation)
        {
            Task.Factory.StartNew(backgroundWork).ContinueWith(t => GuiContinue(control, t, guiContinuation));
        }

        internal static void GuiContinue<T>(Control control, T task, Action<T> guiContinuation)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => guiContinuation(task)));
            }
            else
            {
                guiContinuation(task);
            }
        }
    }
}
