using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Governance.BuildTask.PPETests.Contracts;

namespace Governance.BuildTask.PPETests
{
    [TestClass]
    public class TestComponentDetection
    {
        private string oldLogFileContents;
        private string newLogFileContents;
        private GovernanceDetectionMetadata OldMetadata;
        private GovernanceDetectionMetadata NewMetadata;
        private RegistrationRequest[] OldManifestRegistrations;
        private RegistrationRequest[] NewManifestRegistrations;
        private List<string> bumpedDetectorVersions;

        [TestInitialize]
        public void GatherResources()
        {
            string artifactsDir = Environment.GetEnvironmentVariable("SYSTEM_ARTIFACTSDIRECTORY") ?? "C:\\";
            string[] files = Directory.GetFiles(artifactsDir);
            files = files.Where(x => x.Contains("GovCompDisc_")).ToArray();
            Array.Sort(files);
            // cg files are sorted by timestamp/and since we have extra copies in the case of multiple detection runs in one build, the ones we want are at these indexes:
            // logs = 0,1 ; manifest = 2,3 ; metadata = 4,5
            this.oldLogFileContents = File.ReadAllText(files[0]);
            this.newLogFileContents = File.ReadAllText(files[1]);
            string oldManifestFileContents = File.ReadAllText(files[2]);
            string newManifestFileContents = File.ReadAllText(files[3]);
            string oldMetadataFileContents = File.ReadAllText(files[4]);
            string newMetadataFileContents = File.ReadAllText(files[5]);

            this.OldManifestRegistrations = JsonConvert.DeserializeObject<RegistrationRequest[]>(oldManifestFileContents);
            this.NewManifestRegistrations = JsonConvert.DeserializeObject<RegistrationRequest[]>(newManifestFileContents);
            this.OldMetadata = JsonConvert.DeserializeObject<GovernanceDetectionMetadata>(oldMetadataFileContents);
            this.NewMetadata = JsonConvert.DeserializeObject<GovernanceDetectionMetadata>(newMetadataFileContents);
        }

        [TestMethod]
        public void LogFileHasNoErrors()
        {
            // make sure the new log does not contain any error messages.
            int errorIndex = this.newLogFileContents.IndexOf("[ERROR]");
            if (errorIndex >= 0)
            {
                // prints out the line that the error occured.
                string errorMessage = $"An Error was found: {this.newLogFileContents.Substring(errorIndex, 200)}";
                throw new Exception(errorMessage);
            }
        }

        [TestMethod]
        public void CheckManifestFiles()
        {
            // can't just compare contents since the order of detectors is non deterministic.
            // Parse out array of registrations
            // make sure each component id has identical fields.
            // if any are lost, error, new ones should come with a bumped detector version, which is checked during the detectors counts test.
            Dictionary<string, RegistrationRequest> componentDictionary = this.NewManifestRegistrations.ToDictionary(x => x.Component.ToString(), x => x);
            foreach (var oldRegistrationRequest in OldManifestRegistrations)
            {
                Assert.IsTrue(componentDictionary.TryGetValue(oldRegistrationRequest.Component.ToString(), out var registrationRequest), $"The registration request for {oldRegistrationRequest.Component.ToString()} was not present in the manifest file. Verify this is expected behavior before proceeding");
                Assert.AreEqual(registrationRequest.Component.ToString(), oldRegistrationRequest.Component.ToString(), $"Registration for component: {registrationRequest.Component} does not match");
                Assert.AreEqual(registrationRequest.DevelopmentDependency, oldRegistrationRequest.DevelopmentDependency, $"Registration for component: {registrationRequest.Component} does not match");
                Assert.AreEqual(registrationRequest.ForgeId, oldRegistrationRequest.ForgeId, $"Registration for component: {registrationRequest.Component} does not match");
                Assert.AreEqual(registrationRequest.IsManual, oldRegistrationRequest.IsManual, $"Registration for component: {registrationRequest.Component} does not match");
            }
        }

        [TestMethod]
        public void CheckDetectorsRunTimesAndCounts()
        {
            // makes sure that all detectors have the same number of components found.
            // if some are lost, error. 
            // if some are new, check if version of detector is updated. if it isn't error 
            // Run times should be fairly close to identical. errors if there is an increase of more than 5%
            ProcessDetectorVersions();
            string regexPattern = @"Detection time: (\w+\.\w+) seconds. |(\w+) *\|(\w+\.*\w*) seconds *\|(\d+)";
            var oldMatches = Regex.Matches(this.oldLogFileContents, regexPattern);
            var newMatches = Regex.Matches(this.newLogFileContents, regexPattern);
            Assert.IsTrue(newMatches.Count >= oldMatches.Count, "A detector was lost, make sure this was intentional.");
            var detectorTimes = new Dictionary<string, float>();
            var detectorCounts = new Dictionary<string, int>();
            foreach (Match match in oldMatches)
            {
                if (!match.Groups[2].Success)
                {
                    detectorTimes.Add("TotalTime", float.Parse(match.Groups[1].Value));
                }
                else
                {
                    string detectorId = match.Groups[2].Value;
                    detectorTimes.Add(detectorId, float.Parse(match.Groups[3].Value));
                    detectorCounts.Add(detectorId, int.Parse(match.Groups[4].Value));
                }
            }

            foreach (Match match in newMatches)
            {
                // for each detector and overall, make sure the time doesn't increase by more than 10%
                // for each detector make sure component counts do not change. if they increase, make sure the version of the detector was bumped.
                if (!match.Groups[2].Success)
                {
                    detectorTimes.TryGetValue("TotalTime", out var oldTime);
                    float newTime = float.Parse(match.Groups[1].Value);
                    if (newTime > oldTime)
                    {
                        Assert.AreEqual(oldTime, newTime, Math.Max(2, oldTime * 0.10), "Total Time taken increased by a large amount. Please verify before continuing.");
                    }
                }
                else
                {
                    string detectorId = match.Groups[2].Value;
                    
                    // if (detectorTimes.TryGetValue(detectorId, out var oldTime))
                    //{
                    //   float newTime = float.Parse(match.Groups[3].Value);
                    //    if (newTime > oldTime)
                    //    {
                    //        Assert.AreEqual(oldTime, float.Parse(match.Groups[3].Value), Math.Max(5, oldTime * 0.10), $"the time taken for detector {detectorId} increased by a large amount. Please verify this is expected before continuing.");
                    //    }
                    // }
                    int newCount = int.Parse(match.Groups[4].Value);
                    if (detectorCounts.TryGetValue(detectorId, out var oldCount))
                    {
                        Assert.IsTrue(newCount >= oldCount, $"Components were lost for detector {detectorId}. Verify this is expected behavior.");
                        if (newCount > oldCount)
                        {
                            Assert.IsTrue(bumpedDetectorVersions.Contains(detectorId), $"New Components were found for detector { detectorId }, but the detector version was not updated.");
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void ValidateNewMetadataFile()
        {
            // make sure all the info in the metadata file is identical, ignoring detectors for possibly updated detector counts/versions
            Assert.IsTrue(TestUtilities.MetadatasEqualIgnoreDetectors(OldMetadata,NewMetadata), "Unexpected change in metadata file. Verify before proceeding.");
        }


        private void ProcessDetectorVersions()
        {
            var oldDetectors = this.OldMetadata.SnapshotInformation.ComponentDetectors;
            var newDetectors = this.NewMetadata.SnapshotInformation.ComponentDetectors;
            bumpedDetectorVersions = new List<string>();
            foreach (ComponentDetector cd in oldDetectors)
            {
                var newDetector = newDetectors.FirstOrDefault(det => det.DetectorId == cd.DetectorId);
                Assert.IsNotNull(newDetector, $"the detector {cd.DetectorId} was lost, verify this is expected behavior");
                Assert.IsTrue(newDetector.Version >= cd.Version, $"the version for detector {cd.DetectorId} should not have been reduced. please check all detector versions and verify this behavior.");
                if (newDetector.Version > cd.Version)
                {
                    bumpedDetectorVersions.Add(cd.DetectorId);
                }
                Assert.IsTrue(cd.SupportedComponentTypes.All(type => newDetector.SupportedComponentTypes.Contains(type)), $"the detector {cd.DetectorId} has lost suppported component types. Verify this is expected behavior.");
            }
        }
    }
}
