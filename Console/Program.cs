using System;
using System.Reflection;
using System.Text;

using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Console
{
    /// <summary>
    /// The executable entry-point for the console version.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Processes the <paramref name="arguments"/> and then invokes the requested functionality.
        /// </summary>
        /// 
        /// <param name="arguments">
        /// Zero or more strings that configure the behaviour of the program instantiation.
        /// </param>
        public static void Main(string[] arguments)
        {
            var header = Assembly.GetExecutingAssembly ( ).GenerateHeader ( );
            System.Console.WriteLine ( header );
        }
    }
}
