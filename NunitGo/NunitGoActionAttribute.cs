using System;
using System.IO;
using System.Linq;
using HtmlCustomElements;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Utils;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class 
        | AttributeTargets.Interface | AttributeTargets.Assembly, AllowMultiple = false)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        private readonly string _guid;
        private NunitGoTest _test;

        public NunitGoActionAttribute(string guid = "")
        {
            _guid = guid;
        }

        public void BeforeTest(ITest test)
        {
            Helper.CreateDirectories();
            _test = new NunitGoTest
            {
                DateTimeStart = DateTime.Now,
                Guid = _guid.Equals("") ? Guid.NewGuid() : new Guid(_guid)
            };
        }

        public void AfterTest(ITest test)
        {
            var context = TestContext.CurrentContext;

            _test.DateTimeFinish = DateTime.Now;
            _test.TestDuration = (_test.DateTimeFinish - _test.DateTimeStart).TotalSeconds;
            _test.FullName = test.FullName;
            _test.Id = test.Id;
            _test.FailureStackTrace = context.Result.StackTrace ?? "";
            _test.FailureMessage = context.Result.Message ?? "";
            _test.Result = context.Result.Outcome != null ? context.Result.Outcome.ToString() : "Unknown";

            //Log.Write("Name: " + _test.FullName + ", Res: " + context.Result.Outcome);

            if(!_test.IsSuccess()) Helper.TakeScreenshot(DateTime.Now);

            _test.OutputPath = Helper.Output + @"\" + "Attachments" + @"\" + _test.Guid + @"\";
            Directory.CreateDirectory(_test.OutputPath);
            var output = TestContext.Out.ToString();
            if (!output.Equals(String.Empty))
            {
                var outputPath = _test.OutputPath + Structs.Outputs.Out;
                PageGenerator.GenerateOutputPage(outputPath, output);
                _test.HasOutput = true;
            }

            _test.AddScreenshots(NunitGoTestScreenshotHelper.GetScreenshots());
            _test.Save(_test.OutputPath + "test.xml");

            var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
            Log.Write("   -------------   Tests: " + tests.Count + 
                ", Good = " + tests.Count(x => x.IsSuccess()) + 
                ", Bad = " + tests.Count(x => !x.IsSuccess()) +
                "   -------------   ");
            Log.Write("AAAAA: " + test.Properties.Get(""));
            foreach (var nunitGoTest in tests)
            {
                Log.Write(nunitGoTest.DateTimeStart.ToString("HH:mm:ss.fff") + " - " + 
                    nunitGoTest.DateTimeFinish.ToString("HH:mm:ss.fff") + " " +
                nunitGoTest.FullName + ": Result " + nunitGoTest.Result);
            }

            PageGenerator.GenerateReport(tests, Helper.Output);

        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }// | ActionTargets.Suite; }
        }
    }
}
