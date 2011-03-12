using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests
{
    [TestFixture]
    public class CommitTest
    {
        [Test]
        public void DifferenceLeft ()
        {
            const string workingFolder = @"W:\open source\libraries\textilenet\tfs\trunk";
            const string localItem = @"W:\open source\libraries\textilenet\tfs\trunk\DressingRoom\AboutDressingRoom.cs";
            var actual = Commit.DifferenceLeft (localItem, workingFolder);
            Assert.AreEqual (@"\DressingRoom\AboutDressingRoom.cs", actual);
        }

    }
}
