using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Parent = SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.EnumerableExtensions"/>
    /// </summary>
    [TestFixture]
    public class EnumerableExtensions
    {
        /// <summary>
        /// Tests <see cref="Parent.EnumerableExtensions.Join(IEnumerable{Object},String)"/>.
        /// </summary>
        [Test]
        public void Join()
        {
            Assert.AreEqual("1,2,3", new string[] { "1", "2", "3" }.Join(","));
            Assert.AreEqual("1, 2, 3", new string[] { "1", "2", "3" }.Join(", "));
            Assert.AreEqual("", new string[] { }.Join(","));
            Assert.AreEqual("1", new string[] { "1" }.Join(","));
            Assert.AreEqual("1,2", new string[] { "1", "2" }.Join(","));
        }

        /// <summary>
        /// Tests <see cref="Parent.EnumerableExtensions.QuoteForShell(IEnumerable{Object})"/>
        /// </summary>
        [Test]
        public void QuoteForShell()
        {
            Assert.AreEqual(String.Empty, new string[] { }.QuoteForShell());
            Assert.AreEqual(String.Empty, new string[] { "" }.QuoteForShell());
            Assert.AreEqual("\"one\"", new string[] { "one" }.QuoteForShell());
            Assert.AreEqual("\"with space\"", new string[] { "with space" }.QuoteForShell());

            Assert.AreEqual("\"with space\" \"with another\"", 
                new string[] { "with space", "with another" }.QuoteForShell());
            Assert.AreEqual("\"mixed content\" \"42\"",
                new object[] { "mixed content", 42 }.QuoteForShell());

            Assert.AreEqual("\"--summary=$projectName ($reviewer): $summary\" \"--target-people=$reviewer\"", 
                new string[] { "--summary=$projectName ($reviewer): $summary", 
                        "--target-people=$reviewer" }.QuoteForShell());
        }
    }
}
