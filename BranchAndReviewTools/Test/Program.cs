using System;
using System.Reflection;

using NUnit.Framework;
using Parent = SoftwareNinjas.BranchAndReviewTools;

namespace SoftwareNinjas.BranchAndReviewTools.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.Program"/>
    /// </summary>
    [TestFixture]
    public class Program
    {
        /// <summary>
        /// Tests <see cref="Parent.Program.GenerateHeader(Assembly)"/>
        /// </summary>
        [Test]
        public void GenerateHeader()
        {
            string actual = Parent.Program.GenerateHeader ( Assembly.GetExecutingAssembly ( ) );
            Assert.IsTrue ( actual.Contains ( " version " ) );
        }
    }
}
