using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    [DataContract]
    public class PipComponent
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
