using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Governance.BuildTask.PPETests.Contracts
{
    /// <summary>
    ///  Represents the component version as understood by its protocol. Provides access to protocol specific properties, for example, commit for GitHub or group id for Maven.
    /// </summary>
    [DataContract]
    public class TypedComponent
    {
        /// <summary>
        /// The type of the component.
        /// </summary>
        [DataMember]
        public ComponentType Type { get; set; }

        /// <summary>
        /// The NuGet definition of the component. Required only for NuGet components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public NuGetComponent NuGet { get; set; }

        /// <summary>
        /// The npm definition of the component. Required only for npm components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public NpmComponent Npm { get; set; }

        /// <summary>
        /// The Maven definition of the component. Required only for Maven components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public MavenComponent Maven { get; set; }

        /// <summary>
        /// The git definition of the component. Required only for git components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public GitComponent Git { get; set; }

        /// <summary>
        /// The definition for components whose type is not any of the supported component types by Governance. Required only for Other components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public OtherComponent Other { get; set; }

        /// <summary>
        /// The RubyGems definition of the component. Required only for RubyGems components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public RubyGemsComponent RubyGems { get; set; }

        /// <summary>
        /// The Cargo definition of the component. Required only for cargo components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public CargoComponent Cargo { get; set; }

        /// <summary>
        /// The Pip definition of the component. Required only for PyPi components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PipComponent Pip { get; set; }

        /// <summary>
        /// The Linux defition of the component. Required only for Linux components.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public LinuxComponent Linux { get; set; }

        public TypedComponent()
        {
        }


        public override string ToString()
        {
            switch (Type)
            {
                case ComponentType.NuGet:
                    return $"{NuGet.Name} {NuGet.Version} -{Type}";
                case ComponentType.Npm:
                    return $"{Npm.Name} {Npm.Version} -{Type}";
                case ComponentType.Maven:
                    return $"{Maven.ArtifactId}:{Maven.GroupId} {Maven.Version} -{Type}";
                case ComponentType.Git:
                    return $"{Git.RepositoryUrl}:{Git.CommitHash} -{Type}";
                case ComponentType.RubyGems:
                    return $"{RubyGems.Name} {RubyGems.Version} -{Type}";
                case ComponentType.Cargo:
                    return $"{Cargo.Name} {Cargo.Version} -{Type}";
                case ComponentType.Pip:
                    return $"{Pip.Name} {Pip.Version} -{Type}";
                case ComponentType.Other:
                    return $"{Other.Name} {Other.Version} -{Type}";
                case ComponentType.Linux:
                    return $"{Linux.Distribution} {Linux.Release} {Linux.Name} {Linux.Version} - {Type}";
                // not sure what the default could be here.
                default:
                    return Npm.Name + " " + Npm.Version + " -npm";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is TypedComponent component)
            {
                return component != null &&
                       this.ToString() == component.ToString();
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -1139460471;
            return hashCode * this.ToString().GetHashCode();
        }
    }
}
