using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using SoftwareNinjas.Core.Test;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests
{
    /// <summary>
    /// A class to test <see cref="AccessibleDataGridView"/>.
    /// </summary>
    [TestFixture]
    public class AccessibleDataGridViewTest
    {
        private static readonly IList<int> EmptyWidthList = new ReadOnlyCollection<int>(new List<int>(0));

        [Test]
        public void AdjustWidths_EmptyList()
        {
            var actual = AccessibleDataGridView.AdjustWidths(EmptyWidthList, 0);
            EnumerableExtensions.EnumerateSame(EmptyWidthList, actual);
        }
    }
}
