using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A Maven component as understood by Maven.
    /// </summary>
    [DataContract]
    public class MavenComponent
    {
        /// <summary>
        /// Group id of the component.
        /// </summary>
        [DataMember]
        public string GroupId { get; set; }

        /// <summary>
        /// Artifact id of the component.
        /// </summary>
        [DataMember]
        public string ArtifactId { get; set; }

        /// <summary>
        /// Version of the component.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}
