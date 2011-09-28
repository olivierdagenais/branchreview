using System;
using System.Data;
using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Gui.Extensions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Tests.Extensions
{
    /// <summary>
    /// A class to test <see cref="DataTableExtensions"/>.
    /// </summary>
    [TestFixture]
    public class DataTableExtensionsTest
    {
        private readonly DataTable _branches = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string)) { Caption = "Name" },
                new DataColumn("TaskID", typeof(int)) { Caption = "Task", ColumnMapping = MappingType.Hidden },
                new DataColumn("BasePath", typeof(string)) { Caption = "Path" },
                new DataColumn("LastActivity", typeof(DateTime)) { Caption = "Last Activity" },
                {"Owner", typeof(string)},
                {"Status", typeof(string)},
            },
            Rows =
            {
                {"root", 0, null, new DateTime(2011, 09, 03, 21, 11, 06), null, "unmapped"},
                {"trunk", 0, @"c:\src\trunk", new DateTime(2011, 09, 03, 21, 11, 06), null, "ready"},
                {"123_DoFtp", 123, null, new DateTime(2011, 09, 02, 14, 32, 06), "Joe Dassin", "unmapped"},
                {"435_DoSftp", 435, @"c:\src\branches\TomJones\435_DoSftp", new DateTime(2011, 08, 01, 10, 05, 57), "Tom Jones", "ready for push"},
                {"436_RefactorInternet", 436, @"c:\src\branches\PaulAnka\436_RefactorInternet", new DateTime(2011, 09, 04, 11, 30, 36), "Paul Anka", "pending changes"},
            }
        };

        [Test]
        public void FindFirst_Typical()
        {
            var actual = _branches.FindFirst("TaskID", 123);
            Assert.AreEqual(_branches.Rows[2], actual);
        }

        [Test]
        public void FindFirst_Duplicates()
        {
            var actual = _branches.FindFirst("TaskID", 0);
            Assert.AreEqual(_branches.Rows[0], actual);
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        public void FindFirst_NotFound()
        {
            _branches.FindFirst("TaskID", 42);
        }
    }
}
