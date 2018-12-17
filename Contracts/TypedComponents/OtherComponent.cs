using System;
using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A component whose type is not any of the supported component types by Governance.
    /// </summary>
    [DataContract]
    public class OtherComponent
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

        /// <summary>
        /// Download url of the component.
        /// </summary>
        [DataMember]
        public Uri DownloadUrl { get; set; }

        /// <summary>
        /// Hash of the component. Optional.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Hash { get; set; }
    }
}
