﻿using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Common;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Commit : Form
    {
        private readonly string _workingFolder;
        public string WorkingFolder
        {
            get
            {
                return _workingFolder;
            }
        }

        // TODO: TFS-specific, move to plug-in
        private readonly VersionControlServer _versionControlServer;
        private readonly Workspace _workspace;
        public Workspace Workspace
        {
            get
            {
                return _workspace;
            }
        }

        public Commit(string workingFolder)
        {
            _workingFolder = workingFolder;

            InitializeComponent();

            // TODO: TFS-specific, move to plug-in
            var wi = Workstation.Current.GetLocalWorkspaceInfo (_workingFolder);
            var tpc = new TfsTeamProjectCollection (wi.ServerUri);
            _versionControlServer = tpc.GetService<VersionControlServer> ();
            _workspace = _versionControlServer.GetWorkspace (wi);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Text = "Commit - " + _workingFolder;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            changeLog.Text = "";
        }

        // TODO: TFS-specific, move to plug-in
        internal string LoadDiff()
        {
            var lastModifiedDate = DateTime.Now;
            using (var ms = new MemoryStream ())
            using (var sw = new StreamWriter(ms))
            {
                var diffOptions = new DiffOptions
                {
                    Flags = DiffOptionFlags.IgnoreEndOfLineDifference,
                    // TODO: The following doesn't seem to work, as I still get
                    // "2009-03-07 1:39 PM" instead of "2009-03-07 13:39"
                    CultureInfo = CultureInfo.CurrentCulture,
                    StreamWriter = sw,
                };
                var changes = _workspace.GetPendingChangesEnumerable ();
                foreach (var change in changes)
                {
                    var relativePath = DifferenceLeft (change.LocalItem, _workingFolder);
                    var fixedRelativePath = StripLeadingSlash (relativePath);
                    var header = String.Format ("File: {0}", fixedRelativePath);

                    // TODO: cache the server version to avoid a round-trip for every diff
                    var fileVersion = new ChangesetVersionSpec (change.Version);
                    // TODO: "tf.exe diff" shows a relative path, while this produces a server path
                    var source = Difference.CreateTargetDiffItem (_versionControlServer, change, fileVersion);
                    var target = new DiffItemLocalFile (change.LocalItem, 0, lastModifiedDate, false);
                    Difference.DiffFiles (_versionControlServer, source, target, diffOptions, header, true);
                }
                sw.Flush ();
                ms.Seek (0, SeekOrigin.Begin);
                var sr = new StreamReader (ms);
                var result = sr.ReadToEnd ();
                return result;
            }
        }

        private const string DifferenceExpressionTemplate = "^{0}(.+)";
        internal static string DifferenceLeft (string hayStack, string needle)
        {
            var escapedNeedle = needle.Replace ("\\", "\\\\");
            var expression = String.Format (DifferenceExpressionTemplate, escapedNeedle);
            var regex = new Regex (expression);
            var match = regex.Match (hayStack);
            var result = match.Groups[1].Value;
            return result;
        }

        internal static string StripLeadingSlash (string input)
        {
            var result = input.StartsWith ("\\") ? input.Substring (1) : input;
            return result;
        }

        private void Commit_Load(object sender, EventArgs e)
        {
            RefreshDiff();
        }

        private void RefreshDiff()
        {
            patchText.Text = LoadDiff();
        }

        private void Commit_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.F5 == e.KeyCode)
            {
                RefreshDiff();
            }
        }
    }
}