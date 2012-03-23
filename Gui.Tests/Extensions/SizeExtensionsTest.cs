using System;
using System.Drawing;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Extensions
{
    /// <summary>
    /// A class to test <see cref="SizeExtensions"/>.
    /// </summary>
    [TestFixture]
    public class SizeExtensionsTest
    {
        [Test]
        public void ParseNull()
        {
            var actual = SizeExtensions.Parse(null);
            Assert.AreEqual(Size.Empty, actual);
        }

        [Test]
        public void ParseEmptyString()
        {
            var actual = SizeExtensions.Parse(String.Empty);
            Assert.AreEqual(Size.Empty, actual);
        }

        [Test]
        public void ParseZeroes()
        {
            var actual = SizeExtensions.Parse("{Width=0,Height=0}");
            Assert.AreEqual(Size.Empty, actual);
        }

        [Test]
        public void ParsePositives()
        {
            var actual = SizeExtensions.Parse("{Width=1,Height=2}");
            Assert.AreEqual(new Size(1, 2), actual);
        }

        [Test]
        public void ParseManyDigits()
        {
            var actual = SizeExtensions.Parse("{Width=12345,Height=67890}");
            Assert.AreEqual(new Size(12345, 67890), actual);
        }

        [Test]
        public void ParseNegatives()
        {
            var actual = SizeExtensions.Parse("{Width=-1,Height=-2}");
            Assert.AreEqual(new Size(-1, -2), actual);
        }

        [Test]
        public void ParseOneNegativeOnePositive()
        {
            Assert.AreEqual(new Size(1, -2), SizeExtensions.Parse("{Width=1,Height=-2}"));
            Assert.AreEqual(new Size(-1, 2), SizeExtensions.Parse("{Width=-1,Height=2}"));
        }

        [Test]
        public void ParseWithSpaceAfterComma()
        {
            var actual = SizeExtensions.Parse("{Width=1, Height=2}");
            Assert.AreEqual(new Size(1, 2), actual);
        }
    }
}
