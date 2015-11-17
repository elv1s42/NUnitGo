using System.Linq;
using NunitResultAnalyzer.TestResultClasses;

namespace NunitResultAnalyzer
{
    public static class TestSuiteExtensions
    {
        private static int CountAll(TestSuite testSuite, int count)
        {
            count += testSuite.Results.TestCases.Count();
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) => 
                CountAll(innerTestSuite, current));
        }

        private static int CountPassed(TestSuite testSuite, int count)
        {
            count += testSuite.Results.TestCases.Count(x => x.Result.Equals("Success"));
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) => 
                CountPassed(innerTestSuite, current));
        }

        private static int Count(this TestSuite testSuite, int count, string result)
        {
            count += testSuite.Results.TestCases.Count(x => x.Result.Equals(result));
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) =>
                Count(innerTestSuite, current, result));
        }

        private static int CountSuccess(this TestSuite testSuite, int count, bool success)
        {
            count += testSuite.Results.TestCases.Count(x => x.Executed.Equals("True") && x.Success.Equals(success.ToString()));
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) =>
                CountSuccess(innerTestSuite, current, success));
        }

        private static int CountExecuted(this TestSuite testSuite, int count)
        {
            count += testSuite.Results.TestCases.Count(x => x.Executed.Equals("True"));
            return testSuite.Results.TestSuites.Aggregate(count, (current, innerTestSuite) =>
                CountExecuted(innerTestSuite, current));
        }

        public static string CountPassed(this TestSuite testSuite)
        {
            const int allCount = 0;
            const int passedCount = 0;

            var all = CountAll(testSuite, allCount);
            var passed = CountPassed(testSuite, passedCount);

            return "(" + passed.ToString("D") + @"/" + all.ToString("D") + ")";
        }

        public static int CountByResult(this TestSuite testSuite, string res)
        {
            const int resultCount = 0;
            var result = Count(testSuite, resultCount, res);
            return result;
        }

        public static int CountBySuccess(this TestSuite testSuite, bool success)
        {
            const int resultCount = 0;
            var result = CountSuccess(testSuite, resultCount, success);
            return result;
        }

        public static int CountExecuted(this TestSuite testSuite)
        {
            const int resultCount = 0;
            var result = CountExecuted(testSuite, resultCount);
            return result;
        }

        public static int CountAll(this TestSuite testSuite)
        {
            const int resultCount = 0;
            var result = CountAll(testSuite, resultCount);
            return result;
        }
    }
}
