using System;
using System.Collections.Generic;

using SoftwareNinjas.BranchAndReviewTools.Core;
using System.IO;
using System.Threading;
using System.Xml;
using SoftwareNinjas.Core.Process;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe
{
    /// <summary>
    /// An implementation of <see cref="ISubversionClient"/> that launches Subversion executables as sub-processes to
    /// perform the requested functions.
    /// </summary>
    public class SubversionClient : ISubversionClient
    {
        private Svn _svn;

        /// <summary>
        /// Initializes a new instance of <see cref="SubversionClient"/>.
        /// </summary>
        public SubversionClient()
        {
            _svn = new Svn();
        }

        internal SubversionClient(ICapturedProcessFactory factory)
        {
            _svn = new Svn(factory);
        }

        internal static SvnInfo Create(XmlNode inputNode)
        {
            var urlNode = inputNode.SelectSingleNode("/info/entry/url");
            Uri url = new Uri(urlNode.InnerText);

            var repositoryRootNode = inputNode.SelectSingleNode("/info/entry/repository/root");
            Uri repositoryRoot = new Uri(repositoryRootNode.InnerText);

            var repositoryUuidNode = inputNode.SelectSingleNode("/info/entry/repository/uuid");
            Guid repositoryUuid = new Guid(repositoryUuidNode.InnerText);

            var revisionNode = inputNode.SelectSingleNode("/info/entry/@revision");
            int revision = Convert.ToInt32(revisionNode.Value, 10);

            SvnInfo result = new SvnInfo(url, repositoryRoot, repositoryUuid, revision);
            return result;
        }

        #region ISubversionClient Members

        SvnInfo ISubversionClient.Info(string workingCopyPath)
        {
            var pair = _svn.ExecuteXml(SubCommand.Info, workingCopyPath);
            SvnInfo result;
            if (pair.First != null)
            {
                var doc = pair.First;
                result = Create(doc);
            }
            else
            {
                throw new ApplicationException(pair.Second);
            }
            return result;
        }

        #endregion
    }
}
