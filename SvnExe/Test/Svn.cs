using System;

using Parent = SoftwareNinjas.BranchAndReviewTools.SvnExe;
using NUnit.Framework;
using System.IO;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.Svn"/>.
    /// </summary>
    [TestFixture]
    public class Svn
    {
        private string _inputFolder;
        private string _outputFolder;
        private string _versionStamp;

        /// <summary>
        /// Initializes a new instance for use with some short-lived files.
        /// </summary>
        public Svn()
        {
            _inputFolder = Path.Combine(Environment.CurrentDirectory, "../../../SvnExe/Test");
            _outputFolder = Path.Combine(Environment.CurrentDirectory, "../../../TestOutput");
            _versionStamp = Parent.Svn.DetermineSubversionVersionStamp(Parent.Svn.CurrentVersion);
        }

        /// <summary>
        /// Erases and re-creates output folder.
        /// </summary>
        [SetUp]
        public void PrepareOutputFolder()
        {
            ClearOutputFolder();
            Directory.CreateDirectory(_outputFolder);
        }

        /// <summary>
        /// Erases the files from the output folder.
        /// </summary>
        [TearDown]
        public void ClearOutputFolder()
        {
            if (Directory.Exists(_outputFolder))
            {
                Directory.Delete(_outputFolder, true);
            }
        }

        /// <summary>
        /// Convenience method to obtain the path to a folder in which files may be written to for testing purposes.
        /// </summary>
        /// 
        /// <param name="subFolderName">
        /// The name of a sub-folder that identifies the test scenario.
        /// </param>
        /// 
        /// <returns>
        /// The full path to a folder in which it is safe to create files.
        /// </returns>
        public string GetOutputFolderPath(string subFolderName)
        {
            string result = Path.Combine(_outputFolder, subFolderName);
            return result;
        }

        /// <summary>
        /// Creates a sub-folder named after the provided <paramref name="scenario"/> and copies the config file that
        /// was in the folder of the same name in the source tree as a base.
        /// </summary>
        /// 
        /// <param name="scenario">
        /// A short string identifying the test condition being exercised.
        /// </param>
        /// 
        /// <returns>
        /// The full path to the recently-created folder.
        /// </returns>
        public string PrepareTestFile(string scenario)
        {
            var sub = GetOutputFolderPath(scenario);
            Directory.CreateDirectory(sub);
            File.Copy(
                Path.Combine(_inputFolder, scenario + "/" + Parent.Svn.ConfigFile),
                sub + "/" + Parent.Svn.ConfigFile);
            string result = sub;
            return result;
        }

        /// <summary>
        /// Verifies the string formatting.
        /// </summary>
        [Test]
        public void DetermineSubversionVersionStamp()
        {
            Assert.AreEqual("svn-win32-1.5.6", Parent.Svn.DetermineSubversionVersionStamp(new Version(1, 5, 6)));
        }

        /// <summary>
        /// Should not fail on first run.
        /// </summary>
        [Test]
        public void CheckConfiguration_FolderDoesNotExist()
        {
            Assert.AreEqual(null, Parent.Svn.CheckConfiguration(null, GetOutputFolderPath("FolderDoesNotExist")));
        }

        /// <summary>
        /// Should not fail even if user deleted file but not folders.
        /// </summary>
        [Test]
        public void CheckConfiguration_FileDoesNotExist()
        {
            var sub = GetOutputFolderPath("FileDoesNotExist");
            Directory.CreateDirectory(sub);
            Assert.AreEqual(null, Parent.Svn.CheckConfiguration(null, sub));
        }

        /// <summary>
        /// Should not fail even if user removed the entry we're looking for.
        /// </summary>
        [Test]
        public void CheckConfiguration_NoEntryInFile()
        {
            Assert.AreEqual(
                null,
                Parent.Svn.CheckConfiguration(_versionStamp, PrepareTestFile("NoEntryInFile")));
        }

        /// <summary>
        /// Should not fail even if the configuration is outdated.
        /// </summary>
        [Test]
        public void CheckConfiguration_EntryPointsToInvalidLocation()
        {
            Assert.AreEqual(
                null,
                Parent.Svn.CheckConfiguration(_versionStamp, PrepareTestFile("EntryPointsToInvalidLocation")));
        }

        /// <summary>
        /// Manual/integration test that relies on the developer (i.e. you who is reading this right now) already having
        /// the config file where it would normally reside.
        /// </summary>
        public void CheckConfiguration_EntryPointsToValidLocation()
        {
            string result = Parent.Svn.CheckConfiguration(_versionStamp, Parent.Svn.ConfigFolderPath);
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(result));
        }

        /// <summary>
        /// Verifies the config can be saved and re-read.
        /// </summary>
        [Test]
        public void SaveConfiguration()
        {
            var sub = GetOutputFolderPath("SaveConfiguration");
            // recursive test is recursive (...and records the location of its own file)
            var expected = Path.Combine(sub, Parent.Svn.ConfigFile);
            Parent.Svn.SaveConfiguration(sub, _versionStamp, expected);
            string actual = Parent.Svn.CheckConfiguration(_versionStamp, sub);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Verifies the from-Assembly ZIP extraction.
        /// </summary>
        [Test]
        public void ExtractSubversionBinaries()
        {
            var sub = GetOutputFolderPath("ExtractSubversionBinaries");
            var pathToBin = Parent.Svn.ExtractSubversionBinaries(sub, _versionStamp);
            Assert.IsTrue(Directory.Exists(Path.Combine(sub, _versionStamp)));
            Assert.IsTrue(File.Exists(pathToBin));
            Assert.AreEqual("svn.exe", Path.GetFileName(pathToBin));
        }

        /// <summary>
        /// Tests <see cref="Parent.Svn.ExecuteXml(SubCommand,object[])"/> with a
        /// <see cref="SimulatedCapturedProcess"/> that returns a string representation of XML in stdout.
        /// </summary>
        [Test]
        public void ExecuteXml_Simulated_Success()
        {
            SimulatedCapturedProcess scp =
                new SimulatedCapturedProcess("<info />", null, 0);
            SimulatedCapturedProcessFactory factory = new SimulatedCapturedProcessFactory(scp);
            Parent.Svn svn = new Parent.Svn(factory);
            var actualPair = svn.ExecuteXml(Parent.SubCommand.Info, ".");
            Assert.IsNotNull(actualPair.First);
            Assert.IsNull(actualPair.Second);
            Assert.AreEqual("info", actualPair.First.DocumentElement.Name);
        }

        /// <summary>
        /// Tests <see cref="Parent.Svn.ExecuteXml(SubCommand,object[])"/> with a
        /// <see cref="SimulatedCapturedProcess"/> that returns an error message in stderr.
        /// </summary>
        [Test]
        public void ExecuteXml_Simulated_Failure()
        {
            var expectedError = "Error";
            SimulatedCapturedProcess scp =
                new SimulatedCapturedProcess(null, expectedError, 1);
            SimulatedCapturedProcessFactory factory = new SimulatedCapturedProcessFactory(scp);
            Parent.Svn svn = new Parent.Svn(factory);
            var actualPair = svn.ExecuteXml(Parent.SubCommand.Info, ".");
            Assert.IsNull(actualPair.First);
            Assert.AreEqual(expectedError, actualPair.Second);
        }

        /// <summary>
        /// Overall test of the glue that connects all the previously-tested parts.
        /// </summary>
        [Test]
        public void Integration()
        {
            var sub = GetOutputFolderPath("Integration");

            var svn = new Parent.Svn(Parent.Svn.CurrentVersion, sub);
            var actualBinaryPath = svn.PathToExecutable;

            Assert.IsTrue(File.Exists(actualBinaryPath));

            var folder = Path.GetDirectoryName(actualBinaryPath);
            var greatGrandParent = Path.Combine(folder, "../..");
            var configFile = Path.Combine(greatGrandParent, Parent.Svn.ConfigFile);
            Assert.IsTrue(File.Exists(configFile));
            string entireFile;
            using (var sr = new StreamReader(configFile))
            {
                entireFile = sr.ReadToEnd();
            }
            Assert.IsTrue(entireFile.Contains(actualBinaryPath));
            Assert.IsTrue(entireFile.Contains(_versionStamp));
        }
    }
}
