using System.Collections.Generic;
using System.Linq;
using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    internal class MainStatistics
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

        public string StartDate;
        public string EndDate;
        public string Duration;

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

            StartDate = tests.GetStartDate().ToString("dd.MM.yyyy HH:mm:ss.ff");
            EndDate = tests.GetFinishDate().ToString("dd.MM.yyyy HH:mm:ss.ff");
            Duration = tests.Duration().ToString(@"hh\:mm\:ss\:fff");
        }
    }
}
