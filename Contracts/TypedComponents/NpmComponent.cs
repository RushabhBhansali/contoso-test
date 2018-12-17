using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// An npm component as understood by npm.
    /// </summary>
    [DataContract]
    public class NpmComponent
    {
        /// <summary>
        /// Name of the component.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Version of the component.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}
