using System;
using System.Collections.Generic;
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
        private const string EqualSeparator = "===================================================================";

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
            InitializeComponent();

            // TODO: TFS-specific, move to plug-in
            var wi = Workstation.Current.GetLocalWorkspaceInfo (workingFolder);
            // TODO: scan wi.MappedPaths for best match to workingFolder, then set _workingFolder to one of those
            _workingFolder = workingFolder;
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
                foreach (ListViewItem listItem in changedFiles.SelectedItems)
                {
                    var change = (PendingChange) listItem.Tag;
                    var relativePath = DifferenceLeft (change.LocalItem, _workingFolder);
                    var fixedRelativePath = StripLeadingSlash (relativePath);
                    var header = String.Format ("File: {0}", fixedRelativePath);

                    if (ItemType.Folder == change.ItemType)
                    {
                        if (0 == change.Version)
                        {
                            sw.WriteLine("New folder: {0}", fixedRelativePath);
                        }
                        else if (change.IsRename)
                        {
                            sw.WriteLine("Folder: {0}", fixedRelativePath);
                            sw.WriteLine(EqualSeparator);
                            sw.WriteLine("Old name: {0}", GetLastPathItem(change.SourceServerItem));
                            sw.WriteLine("New name: {0}", GetLastPathItem(change.ServerItem));
                            sw.WriteLine(EqualSeparator);
                        }
                    }
                    else
                    {
                        if (0 == change.Version)
                        {
                            sw.WriteLine("New file: {0}", fixedRelativePath);
                        }
                        else
                        {
                            // TODO: cache the server version to avoid a round-trip for every diff
                            var fileVersion = new ChangesetVersionSpec(change.Version);
                            // TODO: "tf.exe diff" shows a relative path, while this produces a server path
                            var source = Difference.CreateTargetDiffItem(_versionControlServer, change, fileVersion);
                            var target = new DiffItemLocalFile(change.LocalItem, 0, lastModifiedDate, false);
                            Difference.DiffFiles(_versionControlServer, source, target, diffOptions, header, true);
                        }
                    }
                }
                sw.Flush ();
                ms.Seek (0, SeekOrigin.Begin);
                var sr = new StreamReader (ms);
                var result = sr.ReadToEnd ();
                return result;
            }
        }

        internal static string GetLastPathItem(string path)
        {
            var index = path.LastIndexOf('/');
            string result = index != -1 ? path.Substring(index + 1) : path;
            return result;
        }

        private const string DifferenceExpressionTemplate = "^{0}(.+)";
        internal static string DifferenceLeft (string hayStack, string needle)
        {
            var escapedNeedle = needle.Replace ("\\", "\\\\");
            var expression = String.Format (DifferenceExpressionTemplate, escapedNeedle);
            var regex = new Regex (expression, RegexOptions.IgnoreCase);
            var match = regex.Match (hayStack);
            var result = match.Success ? match.Groups[1].Value : hayStack;
            return result;
        }

        internal static string StripLeadingSlash (string input)
        {
            var result = input.StartsWith ("\\") ? input.Substring (1) : input;
            return result;
        }

        private void Commit_Load(object sender, EventArgs e)
        {
            patchText.Text = String.Empty;
            RefreshDiff();
        }

        private void RefreshDiff()
        {
            // TODO: also preserve focused item?
            var oldSelection = new Dictionary<string, string>();
            foreach (ListViewItem listViewItem in changedFiles.SelectedItems)
            {
                oldSelection.Add(listViewItem.SubItems[0].Text, null);
            }

            changedFiles.Items.Clear ();
            var changes = _workspace.GetPendingChangesEnumerable ();
            foreach (var change in changes)
            {
                var relativePath = DifferenceLeft (change.LocalItem, _workingFolder);
                var fixedRelativePath = StripLeadingSlash (relativePath);

                var listItem = new ListViewItem (new[] { fixedRelativePath, change.ChangeTypeName })
                {
                    Tag = change
                };
                changedFiles.Items.Add (listItem);
            }
            // TODO: restore selection, if any was preserved earlier, and that should restore patchText
            if (0 == changedFiles.Items.Count)
            {
                patchText.Text = String.Empty;
            }
            else
            {
                foreach (ListViewItem item in changedFiles.Items)
                {
                    var path = item.SubItems[0].Text;
                    if (oldSelection.ContainsKey(path))
                    {
                        item.Selected = true;
                        oldSelection.Remove(path);
                    }
                }
            }
            changeLog.Focus();
        }

        private void Commit_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.F5 == e.KeyCode)
            {
                RefreshDiff();
            }
        }

        private void LaunchDiff()
        {
            var lastModifiedDate = DateTime.Now;
            foreach (ListViewItem listItem in changedFiles.SelectedItems)
            {
                var change = (PendingChange) listItem.Tag;
                if (ItemType.Folder == change.ItemType)
                {
                    if (0 == change.Version)
                    {
                        // new folder, ignore
                    }
                    else if (change.IsRename)
                    {
                        // renamed folder, ignore
                    }
                }
                else
                {
                    if (0 == change.Version)
                    {
                        var source = new DiffItemLocalFile (String.Empty, 0, lastModifiedDate, true);
                        var target = new DiffItemLocalFile (change.LocalItem, 0, lastModifiedDate, false);
                        Difference.VisualDiffItems (_versionControlServer, source, target);
                    }
                    else
                    {
                        // TODO: cache the server version to avoid a round-trip for every diff
                        var fileVersion = new ChangesetVersionSpec (change.Version);
                        var source = Difference.CreateTargetDiffItem (_versionControlServer, change, fileVersion);
                        var target = new DiffItemLocalFile (change.LocalItem, 0, lastModifiedDate, false);
                        Difference.VisualDiffItems(_versionControlServer, source, target);
                    }
                }
            }
        }

        private void changedFiles_KeyUp (object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                LaunchDiff();
            }
        }

        private void changedFiles_DoubleClick (object sender, EventArgs e)
        {
            LaunchDiff();
        }

        private void changedFiles_SelectedIndexChanged (object sender, EventArgs e)
        {
            patchText.Text = LoadDiff();
        }

        private string DoCommit()
        {
            var changes = _workspace.GetPendingChanges();
            var changeSetNumber = _workspace.CheckIn(changes, changeLog.Text);
            string firstLine;
            using (var sr = new StringReader(changeLog.Text))
            {
                firstLine = sr.ReadLine();
            }
            changeLog.Text = String.Empty;
            RefreshDiff();
            var result = String.Format("C{0}: {1}", changeSetNumber, firstLine);
            return result;
        }

        private void changeLog_KeyDown (object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode && e.Control)
            {
                statusBarText.Text = DoCommit();
                e.SuppressKeyPress = true;
            }
        }
    }
}