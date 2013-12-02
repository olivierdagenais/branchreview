using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Mock
{
    /// <summary>
    /// An implementation of <see cref="ISourceRepository"/> with fake data.
    /// </summary>
    [Export(typeof(ISourceRepository))]
    public class SourceRepository : ISourceRepository
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
                {"2_DoSftp", 2, @"c:\src\branches\TomJones\2_DoSftp", new DateTime(2011, 08, 01, 10, 05, 57), "Tom Jones", "ready for push"},
                {"436_RefactorInternet", 436, @"c:\src\branches\PaulAnka\436_RefactorInternet", new DateTime(2011, 09, 04, 11, 30, 36), "Paul Anka", "pending changes"},
            }
        };

        private readonly Dictionary<string, DataTable> _revisionTablesByBranchId;

        /// <summary>
        /// Creates a new instance of the <see cref="SourceRepository"/> class.
        /// </summary>
        public SourceRepository()
        {
            _revisionTablesByBranchId = new Dictionary<string, DataTable>();
            var branchIdsToRevisionRepositories = new Dictionary<string, string>
            {
                {"root", "root.xml"},
                {"trunk", "suitability.xml"},
                {"123_DoFtp", "thesis.xml"},
                {"2_DoSftp", "todd.xml"},
                {"436_RefactorInternet", "vendor.xml"},
            };
            foreach (var pair in branchIdsToRevisionRepositories)
            {
                var revisions = new DataTable
                {
                    Columns =
                    {
                        new DataColumn("ID", typeof(int)) { Caption = "Revision" },
                        {"Author", typeof(string)},
                        {"Date", typeof(DateTime)},
                        {"Message", typeof(string)},
                    },
                };
                using (var stream = AssemblyExtensions.OpenScopedResourceStream<SourceRepository>(pair.Value))
                {
                    var doc = new XmlDocument();
                    doc.Load(stream);
                    var logEntryNodes = doc.SelectNodes("/log/logentry");
                    if (logEntryNodes == null) continue;
                    foreach (XmlNode logEntryNode in logEntryNodes)
                    {
                        var revision = Convert.ToInt32(logEntryNode.Attributes["revision"].Value, 10);
                        var authorNode = logEntryNode.SelectSingleNode("author");
                        var author = authorNode == null ? null : authorNode.InnerText;
                        var date = ParseIso8601(logEntryNode.SelectSingleNode("date").InnerText);
                        var msg = logEntryNode.SelectSingleNode("msg").InnerText;
                        revisions.Rows.Add(revision, author, date, msg);
                    }
                }
                _revisionTablesByBranchId.Add(pair.Key, revisions);
            }
        }

        ILog ISourceRepository.Log { get; set; }

        DataTable ISourceRepository.LoadBranches()
        {
            return _branches;
        }

        IList<MenuAction> ISourceRepository.GetBranchActions()
        {
            IList<MenuAction> actions = new[]
            {
                new MenuAction("seeInRepo", "&See branches in repository", true,
                    () => Debug.WriteLine("Showing branches")),
            };
            return actions;
        }

        IList<MenuAction> ISourceRepository.GetBranchActions(object branchId)
        {
            var id = (string) branchId;
            var escapedId = id.Replace("'", "''");
            var row = Enumerable.FirstOrDefault(_branches.Select("[ID] = '" + escapedId + "'"));
            IList<MenuAction> actions = new[]
            {
                new MenuAction("pull", "Pu&ll", true, () => Debug.WriteLine("Pulling {0}", new[] {id})),
                new MenuAction("push", "Pu&sh", (string) row["Status"] == "ready for push",
                               () => Debug.WriteLine("Pushing {0}", new[] {id})),
                MenuAction.Separator,
                new MenuAction("delete", "&Delete", true,
                               () => { Debug.WriteLine("Deleting branch {0}", new[] {id}); return true; }),
            };
            return actions;
        }

        void ISourceRepository.CreateBranch(object taskId)
        {
            Debug.WriteLine("Creating branch {0}...", taskId);
        }

        DataTable ISourceRepository.GetPendingChanges(object branchId)
        {
            Debug.WriteLine("Scanning for changes in {0}...", branchId);
            switch (branchId.ToString())
            {
                case "436_RefactorInternet":
                    return Data.RefactorInternetPendingChanges;
                default:
                    return Data.EmptyPendingChanges;
            }
        }

        string ISourceRepository.ComputePendingDifferences(object branchId, IEnumerable<object> pendingChangeIds)
        {
            var numberOfPendingChanges = pendingChangeIds.Count();
            var suffix = (numberOfPendingChanges == 1 ? "" : "s");
            Debug.WriteLine("Computing pending differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return Data.HardcodedDifferences;
        }

        IList<MenuAction> ISourceRepository.GetActionsForPendingChanges(object branchId, IEnumerable<object> pendingChangeIds)
        {
            var numberOfPendingChangeIds = pendingChangeIds.Count();
            if (numberOfPendingChangeIds == 0)
            {
                return MenuAction.EmptyList;
            }
            var suffix = ((numberOfPendingChangeIds == 1) ? "" : "s");
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfPendingChangeIds, suffix)),
                new MenuAction("revert", "&Revert", true,
                    () => Debug.WriteLine("Reverting {0} change{1}", numberOfPendingChangeIds, suffix)),
            };
        }

        void ISourceRepository.Commit(object branchId, string message)
        {
            string firstLine;
            using (var sr = new StringReader(message))
            {
                firstLine = sr.ReadLine();
            }
            Debug.WriteLine("Committing to '{0}' branch with summary: {1}", branchId, firstLine);
        }

        DataTable ISourceRepository.LoadRevisions(object branchId)
        {
            Debug.WriteLine("Loading revisions for branch {0}...", new[] {branchId});
            return _revisionTablesByBranchId[(string) branchId];
        }

        IList<MenuAction> ISourceRepository.GetRevisionActions(object revisionId)
        {
            return new[]
            {
                new MenuAction("rollback", "&Rollback", true,
                    () => Debug.WriteLine("Rolling back revision {0}", new [] {revisionId})),
            };
        }

        DataTable ISourceRepository.GetRevisionChanges(object revisionId)
        {
            Debug.WriteLine("Scanning for changes in revision {0}...", new[] {revisionId});
            return Data.RefactorInternetPendingChanges;
        }

        string ISourceRepository.GetRevisionMessage(object revisionId)
        {
            Debug.WriteLine("Obtaining the message for revision {0}...", new[] {revisionId});
            var table = _revisionTablesByBranchId["root"];
            var rows = table.Select("ID = {0}".FormatInvariant(revisionId));
            var row = rows[0];
            var value = row["Message"];
            return (string) value;
        }

        string ISourceRepository.ComputeRevisionDifferences(object revisionId, IEnumerable<object> changeIds)
        {
            var numberOfPendingChanges = changeIds.Count();
            var suffix = ( numberOfPendingChanges == 1 ? "" : "s" );
            Debug.WriteLine("Computing revision differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return Data.HardcodedDifferences;
        }

        IList<MenuAction> ISourceRepository.GetActionsForRevisionChanges(object revisionId, IEnumerable<object> changeIds)
        {
            var numberOfRevisionChangeIds = changeIds.Count();
            if (numberOfRevisionChangeIds == 0)
            {
                return MenuAction.EmptyList;
            }
            var suffix = ( ( numberOfRevisionChangeIds == 1 ) ? "" : "s" );
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfRevisionChangeIds, suffix)),
                new MenuAction("blame", "&Blame", true,
                    () => Debug.WriteLine("Launching blame for {0} change{1}", numberOfRevisionChangeIds, suffix)),
            };
        }

        internal static DateTime ParseIso8601(string s)
        {
            //                                  "2011-09-03T21:11:06.438915Z"
            var result = DateTime.ParseExact(s, "yyyy-MM-ddTHH:mm:ss.ffffffK", null, DateTimeStyles.AdjustToUniversal);
            return result;
        }
    }
}
