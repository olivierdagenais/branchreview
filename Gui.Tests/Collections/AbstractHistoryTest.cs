using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Collections
{
    /// <summary>
    /// A class to test <see cref="AbstractHistory{T}"/>.
    /// </summary>
    public abstract class AbstractHistoryTest
    {
        private static void AssertEqual<T>(string expected, IEnumerable<T> actual)
        {
            var sb = new StringBuilder();
            foreach (var t in actual)
            {
                sb.Append(t);
            }
            Assert.AreEqual(expected, sb.ToString());
        }

        private void Test(string expected, params string[] items)
        {
            int count = 0;
            var ah = _factory();
            Assert.IsFalse(ah.CanBack);
            Assert.IsFalse(ah.CanForward);
            foreach (var item in items)
            {
                ah.Go(item);
                Assert.IsFalse(ah.CanForward);
                AssertThrows<InvalidOperationException>(ah.Forward);
                count++;
                if (count > 1)
                {
                    Assert.IsTrue(ah.CanBack);
                }
                Assert.AreEqual(item, ah.Last);
                Assert.AreEqual(count, ah.Count);
            }
            AssertEqual(expected, ah);
        }

        private AbstractHistory<string> Build(params string[] items)
        {
            var ah = _factory();
            foreach (var item in items)
            {
                ah.Go(item);
            }
            return ah;
        }

        private static void AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is TException);
                return;
            }
            Assert.Fail("Expected exception of type {0}", typeof (TException));
        }

        private readonly Func<AbstractHistory<string>> _factory;

        protected AbstractHistoryTest(Func<AbstractHistory<string>> implementationFactory)
        {
            _factory = implementationFactory;
        }

        [Test]
        public void FullTest()
        {
            var ah = Build("l", "m", "n", "o", "p");
            Assert.AreEqual(5, ah.Count);
            Assert.IsTrue(ah.CanBack);

            ah.BackTo("n");
            Assert.AreEqual(3, ah.Count);
            Assert.AreEqual("n", ah.Last);
            Assert.IsTrue(ah.CanBack);
            Assert.IsTrue(ah.CanForward);

            ah.Forward();
            Assert.AreEqual(4, ah.Count);
            Assert.AreEqual("o", ah.Last);
            Assert.IsTrue(ah.CanBack);
            Assert.IsTrue(ah.CanForward);

            ah.Back();
            Assert.AreEqual(3, ah.Count);
            Assert.AreEqual("n", ah.Last);
            Assert.IsTrue(ah.CanBack);
            Assert.IsTrue(ah.CanForward);

            ah.Go("q");
            Assert.AreEqual(4, ah.Count);
            Assert.AreEqual("q", ah.Last);
            Assert.IsTrue(ah.CanBack);
            Assert.IsFalse(ah.CanForward);
            AssertThrows<InvalidOperationException>(ah.Forward);
        }

        [Test]
        public void Go()
        {
            Test("l", "l");
        }

        [Test]
        public void GoTwoItems()
        {
            Test("lm", "l", "m");
        }

        [Test]
        public void GoManyItems()
        {
            Test("lmnop", "l", "m", "n", "o", "p");
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void Back()
        {
            var ah = Build();
            ah.Back();
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void GoAndBackOneItem()
        {
            var ah = Build("l");
            ah.Back();
        }

        [Test]
        public void GoAndBackTwoItems()
        {
            var ah = Build("l", "m");
            ah.Back();
            Assert.AreEqual(1, ah.Count);
            AssertThrows<InvalidOperationException>(ah.Back);
        }

        [Test]
        public void GoAndBackManyItems()
        {
            var ah = Build("l", "m", "n", "o", "p");
            ah.Back();
            AssertEqual("lmno", ah);
            Assert.AreEqual("o", ah.Last);
            Assert.AreEqual(4, ah.Count);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ForwardNoItems()
        {
            var ah = Build();
            ah.Forward();
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ForwardNotEnoughItems()
        {
            var ah = Build("l");
            ah.Forward();
        }

        [Test]
        public void ForwardManyItems()
        {
            var ah = Build("l", "m", "n");
            Assert.AreEqual(3, ah.Count);
            ah.Back();
            Assert.AreEqual(2, ah.Count);
            Assert.AreEqual("m", ah.Last);
            ah.Forward();
            Assert.AreEqual(3, ah.Count);
            Assert.AreEqual("n", ah.Last);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void BackToNoItems()
        {
            var ah = Build();

            ah.BackTo("l");
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void BackToOneItem()
        {
            var ah = Build("l");

            ah.BackTo("l");
        }

        [Test]
        public void BackToManyItems()
        {
            var ah = Build("l", "m", "n", "o", "p");
            Assert.AreEqual(5, ah.Count);
            Assert.AreEqual("p", ah.Last);

            ah.BackTo("n");
            Assert.AreEqual(3, ah.Count);
            Assert.AreEqual("n", ah.Last);

            ah.Forward();
            Assert.AreEqual(4, ah.Count);
            Assert.AreEqual("o", ah.Last);
            
            ah.Forward();
            Assert.AreEqual(5, ah.Count);
            Assert.AreEqual("p", ah.Last);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void BackToItemNotFound()
        {
            var ah = Build("l", "m", "n", "o", "p");
            Assert.AreEqual(5, ah.Count);

            ah.BackTo("a");
        }
    }
}
