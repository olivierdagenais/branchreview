using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Mock
{
    internal class Data
    {
        internal const string HardcodedDifferences = @"Index: D:/Work/open source/tools/BART/trunk/BranchAndReviewTools.sln
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

        internal static readonly DataTable EmptyPendingChanges = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string)) { Caption = "Path" },
                new DataColumn("Status", typeof(string)),
            },
        };

        internal static readonly DataTable RefactorInternetPendingChanges = new DataTable
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
    }
}
