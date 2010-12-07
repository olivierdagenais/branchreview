using NUnit.Framework;
using Parent = SoftwareNinjas.BranchAndReviewTools.Gui.Commit;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Test
{
    [TestFixture]
    public class Commit
    {
        [Test]
        public void DifferenceLeft ()
        {
            const string workingFolder = @"W:\open source\libraries\textilenet\tfs\trunk";
            const string localItem = @"W:\open source\libraries\textilenet\tfs\trunk\DressingRoom\AboutDressingRoom.cs";
            var actual = Parent.DifferenceLeft (localItem, workingFolder);
            Assert.AreEqual (@"\DressingRoom\AboutDressingRoom.cs", actual);
        }

    }
}
