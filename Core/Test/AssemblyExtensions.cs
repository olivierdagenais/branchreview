using System;
using System.Reflection;

using NUnit.Framework;
using Parent = SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.AssemblyExtensions"/>
    /// </summary>
    [TestFixture]
    public class AssemblyExtensions
    {
        /// <summary>
        /// Tests <see cref="Parent.AssemblyExtensions.GenerateHeader(Assembly)"/>
        /// </summary>
        [Test]
        public void GenerateHeader()
        {
            string actual = Parent.AssemblyExtensions.GenerateHeader ( Assembly.GetExecutingAssembly ( ) );
            Assert.IsTrue ( actual.Contains ( " version " ) );
        }
    }
}
