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

        private readonly DataTable _doSftpPendingChanges = new DataTable
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
                    new MenuAction("pull", "Pu&ll", true, () => Debug.WriteLine("Pulling {0}", new[] { id })),
                    new MenuAction("push", "Pu&sh", (string) row["Status"] == "ready for push",
                                   () => Debug.WriteLine("Pushing {0}", new[] { id })),
                    new MenuAction("sep1", MenuAction.Separator, true, null),
                    new MenuAction("delete", "&Delete", (string) row["Status"] == "synched",
                                    () => Debug.WriteLine("Deleting branch {0}", new[] { id })),
                };
            }
            return actions;
        }

        public DataTable GetPendingChanges(object branchId)
        {
            Debug.WriteLine("Scanning for changes in {0}...", branchId);
            switch (branchId.ToString())
            {
                case "435_DoSftp":
                    return _doSftpPendingChanges;
                case "436_RefactorInternet":
                    return _refactorInternetPendingChanges;
                default:
                    throw new ArgumentException();
            }
        }

        public string ComputeDifferences(IEnumerable<object> pendingChangeIds)
        {
            var numberOfPendingChanges = pendingChangeIds.Count();
            var suffix = (numberOfPendingChanges == 1 ? "" : "s");
            Debug.WriteLine("Computing differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return
                @"Index: D:/Work/open source/tools/BART/trunk/BranchAndReviewTools.sln
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
        }

        public IList<MenuAction> GetActionsForPendingChanges(IEnumerable<object> changeIds)
        {
            var numberOfChangeIds = changeIds.Count();
            var suffix = ((numberOfChangeIds == 1) ? "" : "s");
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfChangeIds, suffix)),
                new MenuAction("revert", "&Revert", true,
                    () => Debug.WriteLine("Reverting {0} change{1}", numberOfChangeIds, suffix)),
            };
        }
    }
}
