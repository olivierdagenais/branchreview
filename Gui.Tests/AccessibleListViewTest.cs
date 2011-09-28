using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using SoftwareNinjas.Core.Test;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests
{
    /// <summary>
    /// A class to test <see cref="AccessibleListView"/>.
    /// </summary>
    [TestFixture]
    public class AccessibleListViewTest
    {
        private static readonly IList<int> EmptyWidthList = new ReadOnlyCollection<int>(new List<int>(0));

        [Test]
        public void AdjustWidths_EmptyList()
        {
            var actual = AccessibleListView.AdjustWidths(EmptyWidthList, 0);
            EnumerableExtensions.EnumerateSame(EmptyWidthList, actual);
        }

        [Test]
        public void AdjustWidths_ColumnsThatFitAreUnchanged()
        {
            var columns = new[] {80, 97, 110, 102};
            var sum = columns.Sum();
            var actual = AccessibleListView.AdjustWidths(columns, sum);
            EnumerableExtensions.EnumerateSame(columns, actual);
        }

        [Test]
        public void AdjustWidths_LastColumnWayTooWide()
        {
            var columns = new[] {80, 97, 110, 13657};
            var actual = AccessibleListView.AdjustWidths(columns, 1664);
            var expected = new[] {80, 97, 110, 1377};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_FirstColumnWayTooWide()
        {
            var columns = new[] {13657, 80, 97, 110};
            var actual = AccessibleListView.AdjustWidths(columns, 1664);
            var expected = new[] {1377, 80, 97, 110};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_LastTwoColumnsWayTooWide()
        {
            var columns = new[] {10, 20, 15, 2000, 3000};
            var actual = AccessibleListView.AdjustWidths(columns, 1045);
            var expected = new[] {10, 20, 15, 400, 600};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_FirstTwoColumnsWayTooWide()
        {
            var columns = new[] {2000, 3000, 10, 20, 15};
            var actual = AccessibleListView.AdjustWidths(columns, 1045);
            var expected = new[] {400, 600, 10, 20, 15};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_MiddleTwoColumnsWayTooWide()
        {
            var columns = new[] {10, 20, 2000, 3000, 15};
            var actual = AccessibleListView.AdjustWidths(columns, 1045);
            var expected = new[] {10, 20, 400, 600, 15};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_TwoInterleavedColumnsWayTooWide()
        {
            var columns = new[] {10, 2000, 20, 3000, 15};
            var actual = AccessibleListView.AdjustWidths(columns, 1045);
            var expected = new[] {10, 400, 20, 600, 15};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_OneColumnWayTooWide()
        {
            var columns = new[] {2000};
            var actual = AccessibleListView.AdjustWidths(columns, 1000);
            var expected = new[] {1000};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_TwoColumnsWayTooWide()
        {
            var columns = new[] {2000, 3000};
            var actual = AccessibleListView.AdjustWidths(columns, 1000);
            var expected = new[] {400, 600};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_FiveColumnsWayTooWide()
        {
            var columns = new[] {200, 200, 200, 200, 200};
            var actual = AccessibleListView.AdjustWidths(columns, 999);
            var expected = new[] {199, 199, 199, 199, 199};
            EnumerableExtensions.EnumerateSame(expected, actual);
        }

        [Test]
        public void AdjustWidths_SixColumnsWayTooWide()
        {
            var columns = new[] {200, 200, 200, 200, 200, 200};
            var actual = AccessibleListView.AdjustWidths(columns, 1000);
            // all columns are less than 20%; let the multiplier take care of it later
            EnumerableExtensions.EnumerateSame(columns, actual);
        }
    }
}
