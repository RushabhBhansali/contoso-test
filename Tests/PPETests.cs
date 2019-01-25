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
            string oldManifestFileContents;
            string newManifestFileContents;
            string oldMetadataFileContents;
            string newMetadataFileContents;
            try{
                this.newLogFileContents = File.ReadAllText(files[0]);
                this.oldLogFileContents = File.ReadAllText(files[1]);
                newManifestFileContents = File.ReadAllText(files[2]);
                oldManifestFileContents = File.ReadAllText(files[3]);
                newMetadataFileContents = File.ReadAllText(files[4]);
                oldMetadataFileContents = File.ReadAllText(files[5]);           
            }
            catch(Exception){
                throw new Exception("The detector did not publish expected log/metadata files to the correct location");
            }

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
            Dictionary<string, RegistrationRequest> componentDictionary = this.NewManifestRegistrations.ToDictionary(x => x.Component.Type == ComponentType.Pip ? x.Component.ToString().ToLowerInvariant() : x.Component.ToString(), x => x);
            foreach (var oldRegistrationRequest in OldManifestRegistrations)
            {
                var oldRegistrationString = oldRegistrationRequest.Component.Type == ComponentType.Pip ? oldRegistrationRequest.Component.ToString().ToLowerInvariant() : oldRegistrationRequest.Component.ToString();
                Assert.IsTrue(componentDictionary.TryGetValue(oldRegistrationString, out var registrationRequest), $"The registration request for {oldRegistrationRequest.Component.ToString()} was not present in the manifest file. Verify this is expected behavior before proceeding");
                if (oldRegistrationRequest.DevelopmentDependency != null)
                {
                    Assert.AreEqual(oldRegistrationRequest.DevelopmentDependency, registrationRequest.DevelopmentDependency, $"Registration for component: {registrationRequest.Component} has a different \"DevelopmentDependency\".");
                }
                Assert.AreEqual(oldRegistrationRequest.ForgeId, registrationRequest.ForgeId, $"Registration for component: {registrationRequest.Component} has a different \"Forge Id\" than before");
                Assert.AreEqual(oldRegistrationRequest.IsManual, registrationRequest.IsManual, $"Registration for component: {registrationRequest.Component} has a different \"IsManual\" field than before");
            }
        }

        [TestMethod]
        public void CheckDetectorsRunTimesAndCounts()
        {
            // makes sure that all detectors have the same number of components found.
            // if some are lost, error. 
            // if some are new, check if version of detector is updated. if it isn't error 
            // Run times should be fairly close to identical. errors if there is an increase of more than 5%
            string failureMessage = "";
            bool failed = ProcessDetectorVersions(out failureMessage);
            string regexPattern = @"Detection time: (\w+\.\w+) seconds. |(\w+ *[\w()]+) *\|(\w+\.*\w*) seconds *\|(\d+)";
            var oldMatches = Regex.Matches(this.oldLogFileContents, regexPattern);
            var newMatches = Regex.Matches(this.newLogFileContents, regexPattern);
            if (!(newMatches.Count >= oldMatches.Count))
            {
                failed = true;
                failureMessage += "A detector was lost, make sure this was intentional.";
            }
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
            // fail at the end to gather all failures instead of just the first.
            
           
            foreach (Match match in newMatches)
            {
                // for each detector and overall, make sure the time doesn't increase by more than 10%
                // for each detector make sure component counts do not change. if they increase, make sure the version of the detector was bumped.
                if (!match.Groups[2].Success)
                {
                    detectorTimes.TryGetValue("TotalTime", out var oldTime);
                    float newTime = float.Parse(match.Groups[1].Value);
                    if (newTime > oldTime + Math.Max(5, oldTime * 0.10))
                    {
                        failed = true;
                        failureMessage += $"Total Time take increased by a large amount. Please verify before continuing. old time: {oldTime}, new time: {newTime} \n";
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
                        if (detectorId == "Pip" || detectorId == "Total")
                            continue; 
                        if (newCount < oldCount)
                        {
                            failed = true;
                            failureMessage += $"\n {oldCount-newCount} Components were lost for detector {detectorId}. Verify this is expected behavior. \n Old Count: {oldCount}, PPE Count: {newCount}";
                        }
                        if (newCount > oldCount && !bumpedDetectorVersions.Contains(detectorId))
                        {
                            failed = true;
                            failureMessage += $"\n {newCount-oldCount} New Components were found for detector { detectorId }, but the detector version was not updated.";
                        }
                    }
                }
            }
            Assert.IsFalse(failed, failureMessage);
        }

        [TestMethod]
        public void ValidateNewMetadataFile()
        {
            // make sure all the info in the metadata file is identical, ignoring detectors for possibly updated detector counts/versions
            Assert.IsTrue(TestUtilities.MetadatasEqualIgnoreDetectors(OldMetadata,NewMetadata), "Unexpected change in metadata file. Verify before proceeding.");
        }


        private bool ProcessDetectorVersions(out string failureMessage)
        {
            bool failed = false;
            failureMessage = "";
            var oldDetectors = this.OldMetadata.SnapshotInformation.ComponentDetectors;
            if (this.NewMetadata == null || this.NewMetadata.SnapshotInformation == null || this.NewMetadata.SnapshotInformation.ComponentDetectors == null)
            {
                failed = true;
                failureMessage += "New metadata file is corrupted, could not process detector versions";
                return failed;
            }
            var newDetectors = this.NewMetadata.SnapshotInformation.ComponentDetectors;
            bumpedDetectorVersions = new List<string>();
            foreach (ComponentDetector cd in oldDetectors)
            {
                var newDetector = newDetectors.FirstOrDefault(det => det.DetectorId == cd.DetectorId);
                if (newDetector == null) {
                    failed = true;
                    failureMessage += $"the detector {cd.DetectorId} was lost, verify this is expected behavior";
                    continue;
                }
                if (newDetector.Version < cd.Version){
                    failed = true;
                    failureMessage += $"the version for detector {cd.DetectorId} was unexpectedly reduced. please check all detector versions and verify this behavior.";
                    continue;
                } 
                if (newDetector.Version > cd.Version)
                {
                    bumpedDetectorVersions.Add(cd.DetectorId);
                }
                if (!cd.SupportedComponentTypes.All(type => newDetector.SupportedComponentTypes.Contains(type))){
                    failed = true;
                    failureMessage += $"the detector {cd.DetectorId} has lost suppported component types. Verify this is expected behavior.";
                    continue;
                };
            }
            return failed;
        }
    }
}
