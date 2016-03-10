using System.Collections.Generic;
using System.Linq;
using NUnitGoCore.NunitGoItems;

namespace NUnitGoCore.Utils
{
    internal static class NunitGoSuiteHelper
    {
        public static NunitGoSuite GetSuite(this List<NunitGoTest> tests, string suiteName)
        {
            var suite = new NunitGoSuite(suiteName);
            var projects = new HashSet<string>();
            foreach (var test in tests)
            {
                projects.Add(test.ProjectName);
            }

            foreach (var project in projects)
            {
                var projectName = project;
                var projectSuite = new NunitGoSuite(projectName);
                var classes = new HashSet<string>();
                var projectTests = tests.Where(x => x.ProjectName.Equals(projectName)).ToList();
                foreach (var test in projectTests)
                {
                    classes.Add(test.ClassName);
                }

                foreach (var className in classes)
                {
                    var currentClassName = className;
                    var classSuite = new NunitGoSuite(className);
                    var classTests = projectTests.Where(x => x.ClassName.Equals(currentClassName));

                    foreach (var test in classTests)
                    {
                        classSuite.Tests.Add(test);
                    }
                    projectSuite.Suites.Add(classSuite);
                }
                suite.Suites.Add(projectSuite);

            }

            return suite;
        }
        
        public static List<NunitGoTest> GetTests(this NunitGoSuite mainSuite)
        {
            var tests = new List<NunitGoTest>();
            tests.AddRange(mainSuite.Tests);
            var suites = mainSuite.Suites;
            foreach (var suite in suites)
            {
                tests.AddRange(suite.Tests);
                var innerSuites = suite.Suites;
                foreach (var innerSuite in innerSuites)
                {
                    tests.AddRange(innerSuite.Tests);
                }
            }
            return tests;
        }

    }
}
