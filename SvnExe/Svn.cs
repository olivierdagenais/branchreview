﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Xml;

using SoftwareNinjas.BranchAndReviewTools.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe
{
    internal class Svn
    {
        public const int UnzipBlockSize = 2048;
        public static readonly string ConfigFile = Assembly.GetCallingAssembly().GetName().Name + ".dll.config";
        public static readonly Version CurrentVersion = new Version(1, 5, 6);
        private string _pathToExecutable = null;
        private ICapturedProcessFactory _capturedProcessFactory = new CapturedProcessFactory();

        public Svn() : this(CurrentVersion, ConfigFolderPath)
        {

        }

        /// <summary>
        /// Initializes an instance of the sub-process-based Subversion client system.
        /// </summary>
        /// 
        /// <param name="requestedVersion">
        /// The <see cref="Version"/> representing the Subversion release to look for.
        /// </param>
        /// 
        /// <param name="configFolderPath">
        /// The absolute path to a local (non-roaming) folder where the configuration file and optionally the Subversion
        /// binaries will be found.
        /// </param>
        public Svn(Version requestedVersion, string configFolderPath)
        {
            var versionStamp = DetermineSubversionVersionStamp(requestedVersion);
            _pathToExecutable = CheckConfiguration(versionStamp, configFolderPath);

            if (null == _pathToExecutable)
            {
                // TODO: scan PATH environment variable for binaries for this version
                // TODO: if found, update config
            }

            if (null == _pathToExecutable)
            {
                // TODO: check for evidence of installed Subversion binaries (MSI, APT, YUM, etc.) for this version
                // TODO: if found, update config
            }

            if (null == _pathToExecutable)
            {
                _pathToExecutable = ExtractSubversionBinaries(configFolderPath, versionStamp);
                SaveConfiguration(configFolderPath, versionStamp, _pathToExecutable);
            }
        }

        internal Svn(ICapturedProcessFactory capturedProcessFactory)
        {
            _capturedProcessFactory = capturedProcessFactory;
        }

        internal string PathToExecutable
        {
            get
            {
                return _pathToExecutable;
            }
        }

        internal static string ExtractSubversionBinaries(string configFolderPath, string versionStamp)
        {
            string result = null;
            var me = Assembly.GetCallingAssembly();
            var sourcePath = String.Format(CultureInfo.InvariantCulture, "Resources.{0}.zip", versionStamp);
            using (var ins = me.GetManifestResourceStream(typeof(Svn), sourcePath))
            {
                ExtractZip(ins, configFolderPath, (entry) => {
                    if (Path.GetFileName(entry.Name) == "svn.exe")
                    {
                        result = Path.Combine(configFolderPath, entry.Name);
                    }
                });
            }
            return result;
        }

        internal static void ExtractZip(Stream inputStream, string targetFolderPath, Action<ZipEntry> progress)
        {
            byte[] data = new byte[UnzipBlockSize];
            using (var zis = new ZipInputStream(inputStream))
            {
                ZipEntry entry;
                while (( entry = zis.GetNextEntry() ) != null)
                {
                    var fullPath = Path.Combine(targetFolderPath, entry.Name);
                    if (progress != null)
                    {
                        progress(entry);
                    }
                    if (entry.IsDirectory)
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else if (entry.IsFile)
                    {
                        var dirName = Path.GetDirectoryName(fullPath);
                        Directory.CreateDirectory(dirName);
                        using (var os = File.Open(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            int actuallyRead = 0;
                            while (( actuallyRead = zis.Read(data, 0, UnzipBlockSize) ) != 0)
                            {
                                os.Write(data, 0, actuallyRead);
                            }
                        }
                    }
                }
            }
        }

        internal static void SaveConfiguration(string configFolderPath, string versionStamp, string pathToExecutable)
        {
            Directory.CreateDirectory(configFolderPath);

            var pathToConfigFile = Path.Combine(configFolderPath, ConfigFile);
            using (var sw = new StreamWriter(pathToConfigFile))
            {
                var me = Assembly.GetCallingAssembly();
                using (var ins = me.GetManifestResourceStream(typeof(Svn), "Resources.template.config"))
                {
                    using (var sr = new StreamReader(ins))
                    {
                        string line = null;
                        while((line = sr.ReadLine()) != null)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
            var config = LoadConfiguration(pathToConfigFile);
            var element = config.AppSettings.Settings[versionStamp];
            element.Value = pathToExecutable;
            config.Save();
        }

        /// <summary>
        /// Makes use of the .NET <see cref="System.Configuration"/> API to load an arbitrary XML configuration file
        /// without having to parse the XML ourselves.
        /// </summary>
        /// 
        /// <param name="pathToConfigFile">
        /// The full path to the <c>.config</c> file.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="Configuration"/> instance, even if the file at the other end is corrupted!
        /// </returns>
        internal static Configuration LoadConfiguration(string pathToConfigFile)
        {
            var configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = pathToConfigFile;
            Configuration config = 
                ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            return config;
        }

        /// <summary>
        /// If there is a configuration file inside <paramref name="configFolderPath"/>, it's valid, it contains an
        /// entry for the specified <paramref name="versionStamp"/> and that entry exists, then returns said entry;
        /// otherwise <see langword="null"/>.
        /// </summary>
        /// 
        /// <param name="versionStamp">
        /// A version string commonly used to identify Subversion releases.
        /// </param>
        /// 
        /// <param name="configFolderPath">
        /// The absolute path to the folder to look into for the <c>.config</c> file.
        /// </param>
        /// 
        /// <returns>
        /// The path to a subversion binary if found; <see langword="null"/> otherwise.
        /// </returns>
        internal static string CheckConfiguration(string versionStamp, string configFolderPath)
        {
            string result = null;
            var pathToConfigFile = Path.Combine(configFolderPath, ConfigFile);
            if (File.Exists(pathToConfigFile))
            {
                var config = LoadConfiguration(pathToConfigFile);
                if (config != null)
                {
                    if (config.Sections["appSettings"] is AppSettingsSection)
                    {
                        var add = config.AppSettings.Settings[versionStamp];
                        if (add != null && add.Value != null)
                        {
                            var potentialValue = add.Value.Trim();
                            if (File.Exists(potentialValue))
                            {
                                result = potentialValue;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static string DetermineSubversionVersionStamp(Version requestedVersion)
        {
            // TODO: determine this from Environment or something
            string platform = "win32";
            string result = String.Format(CultureInfo.InvariantCulture, "svn-{0}-{1}.{2}.{3}", 
                platform, requestedVersion.Major, requestedVersion.Minor, requestedVersion.Build);
            return result;
        }

        public static string ConfigFolderPath
        {
            get
            {
                // Windows 2000 & XP:  C:\Documents and Settings\Oli\Local Settings\Application Data\
                // Windows Vista & 7:  C:\Users\Oli\AppData\Local\
                var localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var result = Path.Combine(localData, "Software Ninjas/Branch And Review Tools/");
                return result;
            }
        }

        /// <summary>
        /// Starts a sub-process to execute the requested Subversion <paramref name="subCommand"/> with the specified
        /// <paramref name="parameters"/>, with the intention of processing the result as XML.
        /// </summary>
        /// 
        /// <param name="subCommand">
        /// One of the supported <see cref="SubCommand"/>.
        /// </param>
        /// 
        /// <param name="parameters">
        /// The command-line arguments to provide to the <paramref name="subCommand"/>.
        /// </param>
        /// 
        /// <returns>
        /// A pair representing the <see cref="XmlDocument"/> that was constructed, if any, and the error message if
        /// the XML document could not be constructed due to an error.
        /// </returns>
        public Pair<XmlDocument, string> ExecuteXml(SubCommand subCommand, params object[] parameters)
        {
            List<string> outLines = new List<string>();
            List<string> errLines = new List<string>();
            Action<string> outProcessor = (outLine) =>
            {
                outLines.Add(outLine);
            };
            Action<string> errProcessor = (errLine) =>
            {
                errLines.Add(errLine);
            };

            int exitCode;
            // TODO: log subCommand, arguments and parameters and possibly an interception of stdout and stderr
            var arguments = EnumerableExtensions.Compose(subCommand.Arguments, parameters);
            using (var helper = 
                _capturedProcessFactory.Create(_pathToExecutable, arguments, outProcessor, errProcessor))
            {
                exitCode = helper.Run();
            }
            Pair<XmlDocument, string> result;
            if (exitCode != 0 || errLines.Count > 0)
            {
                result = new Pair<XmlDocument, string>(null, errLines.Join(Environment.NewLine));
            }
            else
            {
                // TODO: Instead of building an XmlDocument and returning it, provide an overload that will allow the
                // caller to supply a functor that operates on an XmlReader in the same thread that the process was
                // launched, which is waiting for the child process to complete anyway.
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(outLines.Join(Environment.NewLine));
                result = new Pair<XmlDocument, string>(doc, null);
            }
            return result;
        }
    }
}
