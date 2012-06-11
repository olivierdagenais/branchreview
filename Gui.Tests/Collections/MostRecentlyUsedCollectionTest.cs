using System;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Collections
{
    /// <summary>
    /// A class to test <see cref="MostRecentlyUsedCollection{T}"/>.
    /// </summary>
    [TestFixture]
    public class MostRecentlyUsedCollectionTest
    {
        [Test]
        public void Add_First()
        {
            var mru = new MostRecentlyUsedCollection<String>();

            mru.Add("one");

            Assert.AreEqual(1, mru.Count);
        }

        [Test]
        public void ContainsOneItem()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");

            var actual = mru.Contains("one");

            Assert.AreEqual(true, actual);
        }
    }
}
