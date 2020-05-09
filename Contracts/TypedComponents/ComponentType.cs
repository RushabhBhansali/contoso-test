using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    /// Enumerates existing component types.
    /// </summary>
    [DataContract]
    public enum ComponentType : byte
    {
        /// <summary>
        /// Indicates the component is a NuGet package.
        /// </summary>
        [EnumMember]
        NuGet = 1,

        /// <summary>
        /// Indicates the component is an Npm package.
        /// </summary>
        [EnumMember]
        Npm = 2,

        /// <summary>
        /// Indicates the component is a Maven artifact.
        /// </summary>
        [EnumMember]
        Maven = 3,

        /// <summary>
        /// Indicates the component is a Git repository.
        /// </summary>
        [EnumMember]
        Git = 4,

        /// <summary>
        /// Indicates the component is not any of the supported component types by Governance.
        /// </summary>
        [EnumMember]
        Other = 5,

        /// <summary>
        /// Indicates the component is a Ruby gem.
        /// </summary>
        [EnumMember]
        RubyGems = 6,

        /// <summary>
        /// Indicates the component is a Cargo package.
        /// </summary>
        [EnumMember]
        Cargo = 7,
        
        /// <summary>
        /// Indicates the component is a Pip package.
        /// </summary>
        [EnumMember]
        Pip = 8,

        [EnumMember]
        Linux = 13,
    }
}