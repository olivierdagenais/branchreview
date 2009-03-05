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
            T result = null;
            if ( attributes != null && attributes.Length > 0 )
            {
                result = (T) attributes[0];
            }
            return result;
        }

    }

    class Program
    {
        internal static string GenerateHeader(Assembly source)
        {
            string product = source.GetCustomAttribute<AssemblyProductAttribute> ( ).Product;
            string copyright = source.GetCustomAttribute<AssemblyCopyrightAttribute> ( ).Copyright;
            string version = source.GetName ( ).Version.ToString ( );
            var registeredUser = RegisteredUserAttribute.ExtractFromCallingAssembly();

            StringBuilder result = new StringBuilder ( );
            result.AppendFormat("{0} version {1} - {2}", product, version, copyright );
            result.AppendLine ( );
            result.AppendFormat ( "Registered to: {0} <{1}>", registeredUser.DisplayName, registeredUser.EmailAddress );
            return result.ToString();
        }

        static void Main(string[] args)
        {
            var self = Assembly.GetExecutingAssembly ( );
            Console.WriteLine ( GenerateHeader(self) );
        }
    }
}
