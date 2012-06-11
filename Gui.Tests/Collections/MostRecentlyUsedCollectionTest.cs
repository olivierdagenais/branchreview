using System;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;
using SoftwareNinjas.Core.Test;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Collections
{
    /// <summary>
    /// A class to test <see cref="MostRecentlyUsedCollection{T}"/>.
    /// </summary>
    [TestFixture]
    public class MostRecentlyUsedCollectionTest
    {
        [Test]
        public void Add_FirstItem()
        {
            var mru = new MostRecentlyUsedCollection<String>();

            mru.Add("one");

            Assert.AreEqual(1, mru.Count);
        }

        [Test]
        public void Contains_OneItem()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");

            var actual = mru.Contains("one");

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void GetEnumerator_OneItem()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");

            EnumerableExtensions.EnumerateSame(new[] {"one"}, mru);
        }
    }
}
