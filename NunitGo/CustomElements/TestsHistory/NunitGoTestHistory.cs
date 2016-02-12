using System.Collections.Generic;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.TestsHistory
{
    public static class NunitGoTestHistory
    {
        public static void BuildHistoryJsFile(this List<NunitGoTest> nunitGoTests, string testsPath, string id)
        {
            var highstock = new NunitGoJsHighstock(nunitGoTests, id);
            var jsString = highstock.JsCode;
            highstock.SaveScript(jsString, testsPath);
        }
    }
}
