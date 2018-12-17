using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A Cargo component as understood by Cargo.
    /// </summary>
    [DataContract]
    public class CargoComponent
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