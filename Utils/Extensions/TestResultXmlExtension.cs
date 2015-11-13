using System.Linq;
using Utils.XmlTypes;

namespace Utils.Extensions
{
    public static class TestResultXmlExtension
    {
        public static int CountTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountTests(current));
        }

        public static int CountErrorTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite && result.IsError) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountErrorTests(current));
        }

        public static int CountFailureTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite && result.IsFailure) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountFailureTests(current));
        }
    }
}
