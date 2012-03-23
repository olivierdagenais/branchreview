using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
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
            using (var sk = Registry.CurrentUser.CreateSubKey(ApplicationHome))
            {
                Debug.Assert(sk != null);

                var name = propertyExpression.DetermineName();
                var settingName = control.Name + name;
                var value = (T) sk.GetValue(settingName, defaultValue);
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

    }
}
