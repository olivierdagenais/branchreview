using System;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Mock;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Mock
{
    [TestFixture]
    public class SourceRepositoryTest
    {
        [Test]
        public void TestParseIso8601()
        {
            var expected = new DateTime(2011, 09, 03, 21, 11, 06, 438, DateTimeKind.Utc) + new TimeSpan(9150);
            var actual = SourceRepository.ParseIso8601("2011-09-03T21:11:06.438915Z");
            Assert.AreEqual(expected.Ticks, actual.Ticks);
            Assert.AreEqual(expected.Kind, actual.Kind);
        }
    }
}
