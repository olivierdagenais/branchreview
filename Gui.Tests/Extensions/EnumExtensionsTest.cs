using System.Windows.Forms;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Extensions
{
    /// <summary>
    /// A class to test <see cref="EnumExtensions"/>.
    /// </summary>
    [TestFixture]
    public class EnumExtensionsTest
    {
        [Test]
        public void ParseFormWindowState()
        {
            Assert.AreEqual(FormWindowState.Normal, EnumExtensions.Parse<FormWindowState>("Normal"));
            Assert.AreEqual(FormWindowState.Maximized, EnumExtensions.Parse<FormWindowState>("Maximized"));
            Assert.AreEqual(FormWindowState.Minimized, EnumExtensions.Parse<FormWindowState>("Minimized"));
        }
    }
}
