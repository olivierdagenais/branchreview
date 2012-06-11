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

        [Test]
        public void Add_TwoItems()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");

            mru.Add("two");
            
            Assert.AreEqual(2, mru.Count);
            EnumerableExtensions.EnumerateSame(new [] {"two", "one"}, mru);
        }

        [Test]
        public void Add_TwoItems_OneMostRecentlyUsed()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");
            mru.Add("two");

            mru.Add("one");
            
            Assert.AreEqual(2, mru.Count);
            EnumerableExtensions.EnumerateSame(new [] {"one", "two"}, mru);
        }

        [Test]
        public void Remove_Exists()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");
            mru.Add("two");

            var actual = mru.Remove("one");

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void Remove_DoesNotExist()
        {
            var mru = new MostRecentlyUsedCollection<String>();
            mru.Add("one");
            mru.Add("two");

            var actual = mru.Remove("three");

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void IsReadOnly()
        {
            var mru = new MostRecentlyUsedCollection<String>();

            Assert.AreEqual(false, mru.IsReadOnly);
        }
    }
}
