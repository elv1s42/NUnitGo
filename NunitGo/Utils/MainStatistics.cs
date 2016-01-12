using System.Collections.Generic;
using System.Linq;

namespace NunitGo.Utils
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

        public MainStatistics(List<NunitGoTest> tests)
        {
            TotalAll = tests.Count;
            TotalIgnored = tests.Count(x => x.IsIgnored());
            TotalPassed = tests.Count(x => x.IsSuccess());
            TotalInconclusive = tests.Count(x => x.IsInconclusive());
            TotalBroken = tests.Count(x => x.IsBroken());
            TotalFailed = tests.Count(x => x.IsFailed());
            TotalUnknown = TotalAll - TotalBroken - TotalFailed - TotalIgnored - TotalPassed - TotalInconclusive;

            TotalSuccessTrue = tests.Count(x => x.IsSuccess());
            TotalSuccessFalse = tests.Count(x => !x.IsSuccess());

            TotalExecuted = TotalAll;
        }
    }
}
