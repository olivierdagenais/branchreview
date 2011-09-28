using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        [Test]
        public void AdjustWidths_ColumnsThatFitAreUnchanged()
        {
            var columns = new[] {80, 97, 110, 102};
            var sum = columns.Sum();
            var actual = AccessibleDataGridView.AdjustWidths(columns, sum);
            EnumerableExtensions.EnumerateSame(columns, actual);
        }

        [Test]
        public void AdjustWidths_LastColumnWayTooWide()
        {
            var columns = new[] {80, 97, 110, 13657};
            var actual = AccessibleDataGridView.AdjustWidths(columns, 1664);
            var expected = new[] {80, 97, 110, 1377};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_FirstColumnWayTooWide()
        {
            var columns = new[] {13657, 80, 97, 110};
            var actual = AccessibleDataGridView.AdjustWidths(columns, 1664);
            var expected = new[] {1377, 80, 97, 110};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }
    }
}
