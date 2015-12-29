using System.Collections.Generic;
using System.Linq;

namespace NunitGo.Utils
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

        public static NunitGoSuite GetSuite(string suiteName, List<NunitGoTest> tests)
        {
            var suite = new NunitGoSuite(suiteName);
            var projects = new HashSet<string>();
            foreach (var test in tests)
            {
                projects.Add(test.ProjectName);
            }
            return suite;
        }
    }
}
