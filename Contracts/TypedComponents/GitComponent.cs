using System;
using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A git component as understood by git.
    /// </summary>
    [DataContract]
    public class GitComponent
    {
        /// <summary>
        /// Url of the repository for the component.
        /// </summary>
        [DataMember]
        public Uri RepositoryUrl { get; set; }

        /// <summary>
        /// Commit hash of the repository for the component.
        /// </summary>
        [DataMember]
        public string CommitHash { get; set; }
    }
}
