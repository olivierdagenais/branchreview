using System;
using System.Drawing;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Extensions
{
    /// <summary>
    /// A class to test <see cref="PointExtensions"/>.
    /// </summary>
    [TestFixture]
    public class PointExtensionsTest
    {
        [Test]
        public void ParseNull()
        {
            var actual = PointExtensions.Parse(null);
            Assert.AreEqual(Point.Empty, actual);
        }

        [Test]
        public void ParseEmptyString()
        {
            var actual = PointExtensions.Parse(String.Empty);
            Assert.AreEqual(Point.Empty, actual);
        }
    }
}
