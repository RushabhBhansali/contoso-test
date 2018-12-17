using System;
using System.Collections.Generic;
using System.Text;

namespace Governance.BuildTask.PPETests.Contracts
{
    public class RegistrationRequest
    {
        public TypedComponent Component { get; set; }

        public string ForgeId { get; set; }

        public string ComponentVersionHash { get; set; }

        public IDictionary<string, IDictionary<string, string>> PolicyProperties { get; set; }

        public bool IsManual { get; set; }

        public IEnumerable<TypedComponent> DependencyRoots { get; set; }

        public IEnumerable<string> DetectedComponentLocations { get; set; }

        public bool? DevelopmentDependency { get; set; }
    }
}
