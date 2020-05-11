using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A linux component as understood by common linux package managers.
    /// </summary>
    [DataContract]
    public class LinuxComponent
    {
        /// <summary>
        /// The linux distribution where this component was found.
        /// </summary>
        [DataMember]
        public LinuxDistribution Distribution { get; set; }

        /// <summary>
        /// The release version of the distribution.
        /// </summary>
        [DataMember]
        public string Release { get; set; }

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

    [DataContract]
    public enum LinuxDistribution
    {
        [EnumMember]
        Other = 0,
        [EnumMember]
        Debian = 1,
        [EnumMember]
        Alpine = 2,
        [EnumMember]
        Rhel = 3,
        [EnumMember]
        Centos = 4,
        [EnumMember]
        Fedora = 5,
        [EnumMember]
        Ubuntu = 6,
    }
}
