using System;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Extensions
{
    /// <summary>
    /// A class to test <see cref="DataRowExtensions"/>.
    /// </summary>
    [TestFixture]
    public class DataRowExtensionsTest
    {
        private const string StringValue = "StringValue";
        private const int IntValue = 42;
        private const double DoubleValue = 3.1415926535897932384626433832795;
        private static readonly DateTime DateTimeValue = new DateTime(2012, 03, 12, 16, 05, 29);
        private static readonly Object ObjectValue = new Object();

        [Test]
        public void PointsToSameDataEmptyArrays()
        {
            var a = new object[] {};
            var b = new object[] {};

            var actual = DataRowExtensions.PointsToSameData(a, b);

            Assert.IsTrue(actual);
        }

        [Test]
        public void PointsToSameDataSameArray()
        {
            var a = new[] {StringValue, IntValue, DoubleValue, DateTimeValue, ObjectValue};

            var actual = DataRowExtensions.PointsToSameData(a, a);

            Assert.IsTrue(actual);
        }

        [Test]
        public void PointsToSameDataSameValues()
        {
            var a = new[] {StringValue, IntValue, DoubleValue, DateTimeValue, ObjectValue};
            var b = new[] {StringValue, IntValue, DoubleValue, DateTimeValue, ObjectValue};

            var actual = DataRowExtensions.PointsToSameData(a, b);

            Assert.IsTrue(actual);
        }

        [Test]
        public void PointsToSameDataEquivalentValues()
        {
            var a = new object[] {StringValue, IntValue, DoubleValue, DateTimeValue};
            var b = new object[] {"StringValue", 42, 3.1415926535897932384626433832795, 
                new DateTime(2012, 03, 12, 16, 05, 29)};

            var actual = DataRowExtensions.PointsToSameData(a, b);

            Assert.IsTrue(actual);
        }

        [Test]
        public void PointsToSameDataDifferentSizes()
        {
            var a = new object[] {StringValue, IntValue};
            var b = new object[] {StringValue, IntValue, DoubleValue};

            var actual = DataRowExtensions.PointsToSameData(a, b);

            Assert.IsFalse(actual);
        }
    }
}
