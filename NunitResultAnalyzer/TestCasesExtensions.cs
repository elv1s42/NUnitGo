using System.Collections.Generic;
using System.Linq;
using NunitResultAnalyzer.XmlClasses;

namespace NunitResultAnalyzer
{
    public static class TestCasesExtensions
    {
        public static string CountPassed(this List<TestCase> testCases)
        {
            var allCount = testCases.Count;
            var passedCount = testCases.Count(x => x.Result.Equals("Success"));
            return "(" + passedCount.ToString("D") + @"/" + allCount.ToString("D") + ")";
        }

        private static int CountAll(TestSuite testSuite, int count)
        {
            count += testSuite.Results.TestCases.Count();
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) => CountAll(innerTestSuite, current));
        }
        private static int CountPassed(TestSuite testSuite, int count)
        {
            count += testSuite.Results.TestCases.Count(x => x.Result.Equals("Success"));
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) => CountPassed(innerTestSuite, current));
        }

        public static string CountPassed(this TestSuite testSuite)
        {
            const int allCount = 0;
            const int passedCount = 0;

            var all = CountAll(testSuite, allCount);
            var passed = CountPassed(testSuite, passedCount);

            return "(" + passed.ToString("D") + @"/" + all.ToString("D") + ")";
        }
    }
}
