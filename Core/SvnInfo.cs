using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// An immutable data structure representing the information gathered about a local or remote item.
    /// </summary>
    public struct SvnInfo
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SvnInfo"/> with the provided values.
        /// </summary>
        /// 
        /// <param name="url">
        /// The URL requested or associated with the requested working copy folder.
        /// </param>
        /// 
        /// <param name="repositoryRoot">
        /// The URL of the Subversion repository root; the top-most valid location.
        /// </param>
        /// 
        /// <param name="repositoryUuid">
        /// The universally unique identifier for the repository.
        /// </param>
        /// 
        /// <param name="revision">
        /// The revision of the item.
        /// </param>
        public SvnInfo(Uri url, Uri repositoryRoot, Guid repositoryUuid, int revision)
        {
            _url = url;
            _repositoryRoot = repositoryRoot;
            _repositoryUuid = repositoryUuid;
            _revision = revision;
        }

        private Uri _url;
        /// <summary>
        /// The URL requested or associated with the requested working copy folder.
        /// </summary>
        public Uri Url
        {
            get
            {
                return _url;
            }
        }

        private Uri _repositoryRoot;
        /// <summary>
        /// The URL of the Subversion repository root; the top-most valid location.
        /// </summary>
        public Uri RepositoryRoot
        {
            get
            {
                return _repositoryRoot;
            }
        }

        private Guid _repositoryUuid;
        /// <summary>
        /// The universally unique identifier for the repository.
        /// </summary>
        public Guid RepositoryUuid
        {
            get
            {
                return _repositoryUuid;
            }
        }

        private int _revision;
        /// <summary>
        /// The revision of the item.
        /// </summary>
        public int Revision
        {
            get
            {
                return _revision;
            }
        }
    }
}
