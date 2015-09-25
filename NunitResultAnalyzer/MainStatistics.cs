using NunitResultAnalyzer.XmlClasses;

namespace NunitResultAnalyzer
{
    public class MainStatistics
    {
        public int TotalAll;
        public int TotalPassed;
        public int TotalBroken;
        public int TotalFailed;
        public int TotalIgnored;
        public int TotalInconclusive;
        public int TotalUnknown;

        public int TotalSuccessTrue;
        public int TotalSuccessFalse;

        public int TotalExecuted;

        public MainStatistics(TestSuite suite)
        {
            TotalAll = suite.CountAll();
            TotalIgnored = suite.CountByResult("Ignored");
            TotalPassed = suite.CountByResult("Success");
            TotalInconclusive = suite.CountByResult("Inconclusive");
            TotalBroken = suite.CountByResult("Error");
            TotalFailed = suite.CountByResult("Failure");
            TotalUnknown = TotalAll - TotalBroken - TotalFailed - TotalIgnored - TotalPassed - TotalInconclusive;

            TotalSuccessTrue = suite.CountBySuccess(true);
            TotalSuccessFalse = suite.CountBySuccess(false);

            TotalExecuted = suite.CountExecuted();
        }
    }
}
