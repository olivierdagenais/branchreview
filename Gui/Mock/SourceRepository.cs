﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Mock
{
    internal class SourceRepository : ISourceRepository
    {
        private const string HardcodedDifferences = @"Index: D:/Work/open source/tools/BART/trunk/BranchAndReviewTools.sln
===================================================================
--- D:/Work/open source/tools/BART/trunk/BranchAndReviewTools.sln	(revision 24)
+++ D:/Work/open source/tools/BART/trunk/BranchAndReviewTools.sln	(revision 25)
@@ -16,6 +16,8 @@
 EndProject
 Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""SvnExe"", ""SvnExe\SvnExe.csproj"", ""{7F251D4B-C45E-49E3-AF9E-360E11D98F8B}""
 EndProject
+Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""Gui"", ""Gui\Gui.csproj"", ""{BA6AA408-8948-47A8-A3D8-4A50136A7602}""
+EndProject
 Global
 	GlobalSection(SolutionConfigurationPlatforms) = preSolution
 		Debug|Any CPU = Debug|Any CPU
@@ -36,6 +38,10 @@
 		{7F251D4B-C45E-49E3-AF9E-360E11D98F8B}.Debug|Any CPU.Build.0 = Debug|Any CPU
 		{7F251D4B-C45E-49E3-AF9E-360E11D98F8B}.Release|Any CPU.ActiveCfg = Release|Any CPU
 		{7F251D4B-C45E-49E3-AF9E-360E11D98F8B}.Release|Any CPU.Build.0 = Release|Any CPU
+		{BA6AA408-8948-47A8-A3D8-4A50136A7602}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
+		{BA6AA408-8948-47A8-A3D8-4A50136A7602}.Debug|Any CPU.Build.0 = Debug|Any CPU
+		{BA6AA408-8948-47A8-A3D8-4A50136A7602}.Release|Any CPU.ActiveCfg = Release|Any CPU
+		{BA6AA408-8948-47A8-A3D8-4A50136A7602}.Release|Any CPU.Build.0 = Release|Any CPU
 	EndGlobalSection
 	GlobalSection(SolutionProperties) = preSolution
 		HideSolutionNode = FALSE
";

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

        private readonly DataTable _emptyPendingChanges = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string)) { Caption = "Path" },
                new DataColumn("Status", typeof(string)),
            },
        };

        private readonly DataTable _refactorInternetPendingChanges = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string)) { Caption = "Path" },
                new DataColumn("Status", typeof(string)),
            },
            Rows =
            {
                {"Core/ISourceRepository.cs", "Merged"},
                {"Gui/Mock/SourceRepository.cs", "Copied"},
                {"Gui/app.config", "Branched"},
                {"Gui/Main.cs", "Modified"},
                {"Gui/Main.Designer.cs", "Added"},
                {"Gui/Main.resx", "Deleted"},
                {"Gui/Program.cs", "Renamed"},
            },
        };

        private readonly Dictionary<string, DataTable> _revisionTablesByBranchId;

        public SourceRepository()
        {
            _revisionTablesByBranchId = new Dictionary<string, DataTable>();
            var branchIdsToRevisionRepositories = new Dictionary<string, string>
            {
                {"root", "root.xml"},
                {"trunk", "suitability.xml"},
                {"123_DoFtp", "thesis.xml"},
                {"435_DoSftp", "todd.xml"},
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

        public DataTable LoadBranches()
        {
            return _branches;
        }

        public IList<MenuAction> GetBranchActions()
        {
            IList<MenuAction> actions = new[]
            {
                new MenuAction("seeInRepo", "&See branches in repository", true,
                    () => Debug.WriteLine("Showing branches")),
            };
            return actions;
        }

        public IList<MenuAction> GetBranchActions(object branchId)
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
                new MenuAction("delete", "&Delete", (string) row["Status"] == "synched",
                               () => Debug.WriteLine("Deleting branch {0}", new[] {id})),
            };
            return actions;
        }

        public void CreateBranch(object taskId)
        {
            Debug.WriteLine("Creating branch {0}...", taskId);
        }

        public DataTable GetPendingChanges(object branchId)
        {
            Debug.WriteLine("Scanning for changes in {0}...", branchId);
            switch (branchId.ToString())
            {
                case "436_RefactorInternet":
                    return _refactorInternetPendingChanges;
                default:
                    return _emptyPendingChanges;
            }
        }

        public string ComputePendingDifferences(IEnumerable<object> pendingChangeIds)
        {
            var numberOfPendingChanges = pendingChangeIds.Count();
            var suffix = (numberOfPendingChanges == 1 ? "" : "s");
            Debug.WriteLine("Computing pending differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return HardcodedDifferences;
        }

        public IList<MenuAction> GetActionsForPendingChanges(IEnumerable<object> pendingChangeIds)
        {
            var numberOfPendingChangeIds = pendingChangeIds.Count();
            var suffix = ((numberOfPendingChangeIds == 1) ? "" : "s");
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfPendingChangeIds, suffix)),
                new MenuAction("revert", "&Revert", true,
                    () => Debug.WriteLine("Reverting {0} change{1}", numberOfPendingChangeIds, suffix)),
            };
        }

        public DataTable LoadRevisions(object branchId)
        {
            return _revisionTablesByBranchId[(string) branchId];
        }

        public DataTable GetRevisionChanges(object revisionId)
        {
            Debug.WriteLine("Scanning for changes in revision {0}...", revisionId);
            return _refactorInternetPendingChanges;
        }

        public string GetRevisionMessage(object revisionId)
        {
            Debug.WriteLine("Obtaining the message for revision {0}...", revisionId);
            var table = _revisionTablesByBranchId["root"];
            var rows = table.Select("ID = '{0}'".FormatInvariant(revisionId));
            var row = rows[0];
            var value = row["Message"];
            return (string) value;
        }

        public string ComputeRevisionDifferences(IEnumerable<object> changeIds)
        {
            var numberOfPendingChanges = changeIds.Count();
            var suffix = ( numberOfPendingChanges == 1 ? "" : "s" );
            Debug.WriteLine("Computing revision differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return HardcodedDifferences;
        }

        public IList<MenuAction> GetActionsForRevisionChanges(IEnumerable<object> changeIds)
        {
            var numberOfPendingChangeIds = changeIds.Count();
            var suffix = ( ( numberOfPendingChangeIds == 1 ) ? "" : "s" );
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfPendingChangeIds, suffix)),
                new MenuAction("blame", "&Blame", true,
                    () => Debug.WriteLine("Launching blame for {0} change{1}", numberOfPendingChangeIds, suffix)),
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
