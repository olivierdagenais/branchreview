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
        static void Main(string[] args)
        {
            var self = Assembly.GetExecutingAssembly ( );
            string product = self.GetCustomAttribute<AssemblyProductAttribute> ( ).Product;
            string copyright = self.GetCustomAttribute<AssemblyCopyrightAttribute> ( ).Copyright;
            string version = self.GetName ( ).Version.ToString ( );

            Console.WriteLine ( "{0} version {1} - {2}", product, version, copyright );
        }
    }
}
