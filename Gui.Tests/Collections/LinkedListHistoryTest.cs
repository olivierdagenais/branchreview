using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Collections;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Collections
{
    /// <summary>
    /// A class to test <see cref="LinkedListHistory{T}"/>.
    /// </summary>
    [TestFixture]
    public class LinkedListHistoryTest : AbstractHistoryTest
    {
        public LinkedListHistoryTest() : base(() => new LinkedListHistory<string>())
        {
        }
    }
}
