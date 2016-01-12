using System.Collections.Generic;
using System.Linq;

namespace NunitGo.Utils
{
    public static class NunitGoSuiteHelper
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
                suite.Suites.Add(projectSuite);
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
                }

            }

            return suite;
        }
    }
}
