using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SoftwareNinjas.BranchAndReviewTools
{
    static class Extensions
    {
        public static T GetCustomAttribute<T>(this Assembly assembly) where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), false);
            
            return ( (T) attributes[0] );
        }

    }

    class Program
    {
        internal static string GenerateHeader(Assembly source)
        {
            string product = source.GetCustomAttribute<AssemblyProductAttribute> ( ).Product;
            string copyright = source.GetCustomAttribute<AssemblyCopyrightAttribute> ( ).Copyright;
            string version = source.GetName ( ).Version.ToString ( );

            string result = String.Format ( "{0} version {1} - {2}", product, version, copyright );
            return result;
        }

        static void Main(string[] args)
        {
            var self = Assembly.GetExecutingAssembly ( );
            Console.WriteLine ( GenerateHeader(self) );
        }
    }
}
