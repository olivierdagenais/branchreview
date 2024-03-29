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
        public void ParseObject()
        {
            object value = "{X=1,Y=2}";
            var actual = PointExtensions.Parse(value);
            Assert.AreEqual(new Point(1, 2), actual);
        }

        [Test]
        public void ParseEmptyString()
        {
            var actual = PointExtensions.Parse(String.Empty);
            Assert.AreEqual(Point.Empty, actual);
        }

        [Test]
        public void ParseZeroes()
        {
            var actual = PointExtensions.Parse("{X=0,Y=0}");
            Assert.AreEqual(Point.Empty, actual);
        }

        [Test]
        public void ParsePositives()
        {
            var actual = PointExtensions.Parse("{X=1,Y=2}");
            Assert.AreEqual(new Point(1, 2), actual);
        }

        [Test]
        public void ParseManyDigits()
        {
            var actual = PointExtensions.Parse("{X=12345,Y=67890}");
            Assert.AreEqual(new Point(12345, 67890), actual);
        }

        [Test]
        public void ParseNegatives()
        {
            var actual = PointExtensions.Parse("{X=-1,Y=-2}");
            Assert.AreEqual(new Point(-1, -2), actual);
        }

        [Test]
        public void ParseOneNegativeOnePositive()
        {
            Assert.AreEqual(new Point(1, -2), PointExtensions.Parse("{X=1,Y=-2}"));
            Assert.AreEqual(new Point(-1, 2), PointExtensions.Parse("{X=-1,Y=2}"));
        }

        [Test]
        public void ParseWithSpaceAfterComma()
        {
            var actual = PointExtensions.Parse("{X=1, Y=2}");
            Assert.AreEqual(new Point(1, 2), actual);
        }
    }
}
