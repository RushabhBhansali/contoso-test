using Governance.BuildTask.PPETests.Contracts;

namespace Governance.BuildTask.PPETests
{
    public static class TestUtilities
    {
        public static bool MetadatasEqualIgnoreDetectors(GovernanceDetectionMetadata first, GovernanceDetectionMetadata second)
        {
            return first == second || first != null && second != null &&
                   SnapshotsEqualIgnoreDetectors(first.SnapshotInformation, second.SnapshotInformation) &&
                   first.GovernedRepositoryNameOrId == second.GovernedRepositoryNameOrId &&
                   first.ProductNameOrId == second.ProductNameOrId;
        }


        public static bool SnapshotsEqualIgnoreDetectors(SnapshotInformation first, SnapshotInformation second)
        {
            return  first == second ||
                    first != null && second != null &&
                    first.BuildIdentifier == second.BuildIdentifier &&
                    first.BuildDisplayIdentifier == second.BuildDisplayIdentifier &&
                    first.SourceIdentifier == second.SourceIdentifier &&
                    first.SourceDisplayIdentifier == second.SourceDisplayIdentifier &&
                    first.IsTransient == second.IsTransient &&
                    first.PullRequestId == second.PullRequestId &&
                    first.BuildType == second.BuildType &&
                    first.BuildDisplayType == second.BuildDisplayType &&
                    first.SourceType == second.SourceType &&
                    first.SourceDisplayType == second.SourceDisplayType;
        }
    }
}
