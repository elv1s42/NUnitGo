using System.Collections.Generic;

namespace NunitGoCore.NunitGoItems
{
    public class NunitGoSuite
    {
        public string Name;
        public List<NunitGoTest> Tests;
        public List<NunitGoSuite> Suites;

        public NunitGoSuite(string name)
        {
            Name = name;
            Tests = new List<NunitGoTest>();
            Suites = new List<NunitGoSuite>();
        }
    }
}
