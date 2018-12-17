using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A NuGet component as understood by NuGet.
    /// </summary>
    [DataContract]
    public class NuGetComponent
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
