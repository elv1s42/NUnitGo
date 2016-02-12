using System.Collections.Generic;
using System.Linq;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.TestsHistory
{
    public static class NunitGoTestHistory
    {
        public static void BuildHistory(this List<NunitGoTest> nunitGoTests)
        {
            var orderedList = nunitGoTests.OrderBy(x => x.DateTimeFinish);

        }
    }
}
