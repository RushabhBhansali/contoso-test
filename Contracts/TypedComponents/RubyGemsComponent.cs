using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// A Ruby gem as understood by Ruby.
    /// </summary>
    [DataContract]
    public class RubyGemsComponent
    {
        /// <summary>
        /// Name of the gem.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Version of the gem.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}