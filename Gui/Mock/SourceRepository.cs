using System;
using System.Data;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Mock
{
    internal class SourceRepository : ISourceRepository
    {
        private readonly DataTable _branches = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string)) { Caption = "Name" },
                new DataColumn("TaskID", typeof(int)) { Caption = "Task", ColumnMapping = MappingType.Hidden },
                new DataColumn("LastActivity", typeof(DateTime)) { Caption = "Last Activity" },
                {"Owner", typeof(string)},
                {"Status", typeof(string)},
            },
            Rows =
            {
                {"123_DoFtp", 123, new DateTime(2011, 09, 02, 14, 32, 06), "Joe Dassin", "unmapped"},
                {"435_DoSftp", 435, new DateTime(2011, 08, 01, 10, 05, 57), "Tom Jones", "ready for push"},
                {"436_RefactorInternet", 436, new DateTime(2011, 09, 04, 11, 30, 36), "Paul Anka", "pending changes"},
            }
        };

        public DataTable LoadBranches()
        {
            return _branches;
        }
    }
}
