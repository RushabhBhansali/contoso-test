using System;

namespace Governance.BuildTask.PPETests.Contracts
{
    [Serializable]
    public class GovernanceDetectionMetadata
    {
        public SnapshotInformation SnapshotInformation { get; set; }
        public string GovernedRepositoryNameOrId { get; set; }
        public string ProductNameOrId { get; set; }
    }

    [Serializable]
    public class SnapshotInformation
    {
        public int? BuildIdentifier { get; set; }
        public string BuildDisplayIdentifier { get; set; }
        public string SourceIdentifier { get; set; }
        public string SourceDisplayIdentifier { get; set; }
        public bool? IsTransient { get; set; }
        public ComponentDetector[] ComponentDetectors { get; set; }
        public int? PullRequestId { get; set; }
        public string BuildType { get; set; }
        public string BuildDisplayType { get; set; }
        public string SourceType { get; set; }
        public string SourceDisplayType { get; set; }
        public bool? IsValid { get; set; }
    }

    [Serializable]
    public class ComponentDetector
    {
        public string DetectorId { get; set; }
        public int[] SupportedComponentTypes { get; set; }
        public int Version { get; set; }
    }
}
