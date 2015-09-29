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
    }
}
