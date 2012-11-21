using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Mock
{
    /// <summary>
    /// An implementation of <see cref="IBuildRepository"/> with fake data.
    /// </summary>
    [Export(typeof(IBuildRepository))]
    public class BuildRepository : IBuildRepository
    {
        private readonly DataTable _builds = new DataTable
        {
            Columns =
            {
                new HiddenDataColumn("ID", typeof(string)),
                {"Status", typeof(bool)},
                {"Name", typeof(string)},
                new DataColumn("TeamProject", typeof(string)) { Caption = "Team Project" },
                new DataColumn("BuildDefinition", typeof(string)) { Caption = "Build Definition" },
                new DataColumn("DateCompleted", typeof(DateTime)) { Caption = "Date Completed" },
                {"Duration", typeof(TimeSpan)},
                new DataColumn("RequestedBy", typeof(string)) { Caption = "Requested By" },
                {"Agent", typeof(string)},
            },
            Rows =
            {
                {0, false, "Provocante - 42", "Succès", "Provocante", new DateTime(2012, 11, 20, 17, 53, 38), new TimeSpan(00, 03, 28), "Marjo", "iTunes"},
                {1, true, "Provocante - 41", "Succès", "Provocante", new DateTime(2012, 11, 20, 17, 38, 53), new TimeSpan(00, 05, 48), "Marjo", "iTunes"},
                {2, true, "Innocente - 18", "Succès", "Innocente", new DateTime(2012, 11, 20, 16, 22, 53), new TimeSpan(00, 01, 00), "Marjo", "iTunes"},
                {3, true, "C't'une Joke - 8", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 19, 32, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC1"},
                {4, false, "C't'une Joke - 7", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 19, 32, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC2"},
                {5, false, "C't'une Joke - 6", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 19, 22, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC1"},
                {6, false, "C't'une Joke - 5", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 19, 12, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC2"},
                {7, false, "C't'une Joke - 4", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 19, 02, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC1"},
                {8, false, "C't'une Joke - 3", "Série TV", "C't'une Joke", new DateTime(2008, 02, 01, 18, 52, 53), new TimeSpan(00, 23, 00), "Joe Bocan", "SRC2"},
            }
        };

        #region IBuildRepository Members

        /// <summary>
        /// The <see cref="ILog"/> to send messages to.
        /// </summary>
        public ILog Log { get; set; }

        private void Info(string message, int progressValue, int progressMaximum)
        {
            Debug.WriteLine(message);
            if (Log != null)
            {
                Log.Info(message, progressValue, progressMaximum);
            }
        }

        DataTable IBuildRepository.LoadBuilds()
        {
            Info("Loading builds...", 0, 0);
            return _builds;
        }

        IList<MenuAction> IBuildRepository.GetBuildActions(object buildId)
        {
            Info("Getting build actions...", 0, 0);
            return new[]
            {
                new MenuAction("donuts", "&Purchase donuts of shame", (int)buildId == 0, 
                    () => Debug.WriteLine("Donuts of shame have been ordered for build '{0}'", buildId)),
            };
        }

        string IBuildRepository.GetBuildLog(object buildId)
        {
            Info("Retrieving build log...", 0, 0);
            return @"Buildfile: file:///W:/Downloads/Dropbox/BART/clone/BranchAndReviewTools.build
Target framework: Microsoft .NET Framework 4.0
Target(s) specified: deploy

[loadtasks] Scanning assembly ""NAnt.Contrib.Tasks"" for extensions.
[loadtasks] Scanning assembly ""Textile.NAnt"" for extensions.
[loadtasks] Scanning assembly ""SoftwareNinjas.NAnt"" for extensions.

clean:

    [clean] Cleaning Core...
    [clean] Cleaning Gui...

customize:

[customizeAssembly] No customization performed.

compile:

  [msbuild]   Core -> W:\Downloads\Dropbox\BART\clone\Core\bin\Debug\SoftwareNinjas.BranchAndReviewTools.Core.dll
  [msbuild]   Gui -> W:\Downloads\Dropbox\BART\clone\Gui\bin\Debug\SoftwareNinjas.BranchAndReviewTools.Gui.exe
  [msbuild]   Core.Tests -> W:\Downloads\Dropbox\BART\clone\Core.Tests\bin\Debug\SoftwareNinjas.BranchAndReviewTools.Core.Tests.dll
  [msbuild]   Gui.Tests -> W:\Downloads\Dropbox\BART\clone\Gui.Tests\bin\Debug\SoftwareNinjas.BranchAndReviewTools.Gui.Tests.dll

test:

     [echo] Testing Core...
     [exec] Tests run: 1, Failures: 0, Not run: 0, Time: 0.114 seconds
     [echo] Testing Gui...
     [exec] Tests run: 83, Failures: 0, Not run: 0, Time: 0.402 seconds

documentation:


deploy:

  [xmlpeek] Peeking at 'W:\Downloads\Dropbox\BART\clone\Version.xml' with XPath expression '/version/@major'.
  [xmlpeek] Found '1' nodes with the XPath expression '/version/@major'.
  [xmlpeek] Peeking at 'W:\Downloads\Dropbox\BART\clone\Version.xml' with XPath expression '/version/@minor'.
  [xmlpeek] Found '1' nodes with the XPath expression '/version/@minor'.
   [delete] Deleting directory 'W:\Downloads\Dropbox\BART\clone\Deploy'.
    [mkdir] Creating directory 'W:\Downloads\Dropbox\BART\clone\Deploy\'.
    [mkdir] Creating directory 'W:\Downloads\Dropbox\BART\clone\Deploy\SoftwareNinjas.BranchAndReviewTools-1.0.private'.
     [copy] Copying 9 files to 'W:\Downloads\Dropbox\BART\clone\Deploy\SoftwareNinjas.BranchAndReviewTools-1.0.private'.
      [zip] Zipping 9 files to 'W:\Downloads\Dropbox\BART\clone\Deploy\SoftwareNinjas.BranchAndReviewTools-1.0.private.zip'.

BUILD SUCCEEDED

Total time: 5.6 seconds.

";
        }

        #endregion
    }
}
