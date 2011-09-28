using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
                new DataColumn("BasePath", typeof(string)) { Caption = "Path" },
                new DataColumn("LastActivity", typeof(DateTime)) { Caption = "Last Activity" },
                {"Owner", typeof(string)},
                {"Status", typeof(string)},
            },
            Rows =
            {
                {"123_DoFtp", 123, null, new DateTime(2011, 09, 02, 14, 32, 06), "Joe Dassin", "unmapped"},
                {"435_DoSftp", 435, @"c:\src\branches\TomJones\435_DoSftp", new DateTime(2011, 08, 01, 10, 05, 57), "Tom Jones", "ready for push"},
                {"436_RefactorInternet", 436, @"c:\src\branches\PaulAnka\436_RefactorInternet", new DateTime(2011, 09, 04, 11, 30, 36), "Paul Anka", "pending changes"},
            }
        };

        public DataTable LoadBranches()
        {
            return _branches;
        }

        public IList<MenuAction> GetActionsForBranch(object branchId)
        {
            IList<MenuAction> actions;
            if (null == branchId)
            {
                actions = new[]
                {
                    new MenuAction("create", "Create branch", true, () => Debug.WriteLine("Creating branch")),
                };
            }
            else
            {
                var id = (string) branchId;
                var escapedId = id.Replace("'", "''");
                var row = _branches.Select("[ID] = '" + escapedId + "'").FirstOrDefault();
                actions = new[]
                {
                    new MenuAction("pull", "Pu&ll", true, () => Debug.WriteLine("Pulling {0}", id)),
                    new MenuAction("push", "Pu&sh", (string) row["Status"] == "ready for push",
                                   () => Debug.WriteLine("Pushing {0}", id)),
                    new MenuAction("sep1", MenuAction.Separator, true, null),
                    new MenuAction("delete", "&Delete", (string) row["Status"] == "synched",
                                    () => Debug.WriteLine("Deleting branch {0}", id)),
                };
            }
            return actions;
        }
    }
}
