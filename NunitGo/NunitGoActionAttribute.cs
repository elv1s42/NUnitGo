using System;
using System.IO;
using System.Linq;
using System.Threading;
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
        private NunitGoTest _test;

        public void BeforeTest(ITest test)
        {
            Helper.CreateDirectories();
            _test = new NunitGoTest
            {
                DateTimeStart = DateTime.Now,
                Guid = Guid.NewGuid()
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
            }

            var screens = NunitGoTestScreenshotHelper.GetScreenshots(Helper.Screenshots);
            _test.AddScreenshots(screens);

            _test.Save(_test.OutputPath + "test.xml");

            var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
            Log.Write("   -------------   Tests: " + tests.Count + 
                ", Good = " + tests.Count(x => x.IsSuccess()) + 
                ", Bad = " + tests.Count(x => !x.IsSuccess()) +
                "   -------------   ");
            foreach (var nunitGoTest in tests)
            {
                Log.Write(nunitGoTest.DateTimeStart.ToString("HH:mm:ss.fff") + " - " + 
                    nunitGoTest.DateTimeFinish.ToString("HH:mm:ss.fff") + " " +
                nunitGoTest.FullName + ": Result " + nunitGoTest.Result);
            }


        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }// | ActionTargets.Suite; }
        }
    }
}
