using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NunitGo.CustomElements;
using NunitGo.NunitGoItems;
using NunitGo.NunitGoItems.Subscriptions;
using NunitGo.Utils;
using ScreenshotTaker;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        private Guid _testGuid;
        private readonly string _projectName;
        private readonly string _className;
        private readonly string _testName;
        private readonly Subsciption _subscription;
        private NunitGoTest _test;
        private DateTime _start;
        private DateTime _finish;

        public static Guid TestGuid = Guid.Empty;

        public NunitGoActionAttribute(string testGuidString = "", string projectName = "", string className = "", 
            string testName = "", string subscription = "")
        {
            _testGuid = testGuidString.Equals("")
                    ? Guid.Empty
                    : new Guid(testGuidString);
            _projectName = projectName;
            _className = className;
            _testName = testName;
            _subscription = (subscription == "")
                ? null
                : NunitGoHelper.Configuration.Subsciptions.First(s => s.Name.Equals(subscription));
        }

        public NunitGoActionAttribute(Guid testGuid, string projectName = "", string className = "",
            string testName = "", string subscription = "")
        {
            _testGuid = testGuid;
            _projectName = projectName;
            _className = className;
            _testName = testName;
            _subscription = (subscription == "")
                ? null
                : NunitGoHelper.Configuration.Subsciptions.First(s => s.Name.Equals(subscription));
        }

        public void BeforeTest(ITest test)
        {
            if (!NunitGoHelper.GenerateReport) return;

            NunitGoHelper.CreateDirectories();
            _start = DateTime.Now;
            //Log.Write("START:" + test.FullName);
        }

        public void AfterTest(ITest test)
        {
            if (!NunitGoHelper.GenerateReport) return;

            _finish = DateTime.Now;
            var context = TestContext.CurrentContext;

            if (_testGuid.Equals(Guid.Empty))
            {
                _testGuid =  TestGuid.Equals(Guid.Empty) ? new Guid() : TestGuid;
            }

            _test = new NunitGoTest
            {
                DateTimeStart = _start,
                DateTimeFinish = DateTime.Now,
                TestDuration = (_finish - _start).TotalSeconds,
                FullName = test.FullName,
                ProjectName = (_projectName.Equals("")) ? test.FullName.Split(new []{'.'}).First() : _projectName,
                ClassName = (_className.Equals("")) ? test.FullName.Split(new[] { '.' }).Skip(1).First() : _className,
                Name = (_testName.Equals("")) ? test.Name : _testName,
                TestStackTrace = context.Result.StackTrace ?? "",
                TestMessage = context.Result.Message ?? "",
                Result = context.Result.Outcome != null ? context.Result.Outcome.ToString() : "Unknown",
                Guid = _testGuid,//_testGuid.Equals(Guid.Empty) ? TestGuid : Guid.NewGuid(),
                Screenshots = new List<Screenshot>()
            };

            //Log.Write("FINISH: " + test.FullName);
            
            if(!_test.IsSuccess() && NunitGoHelper.TakeScreenshotAfterTestFailed) _test.TakeScreenshot();

            _test.AttachmentsPath = NunitGoHelper.Output + @"\" + "Attachments" + @"\" + _test.Guid + @"\";
            Directory.CreateDirectory(_test.AttachmentsPath);
            _test.TestHref = "Attachments" + @"/" + _test.Guid + @"/" + Output.Outputs.TestHtml;
            _test.LogHref = Output.Outputs.Out;
            var output = TestContext.Out.ToString();
            if (!output.Equals(String.Empty))
            {
                var outputPath = _test.AttachmentsPath + Output.Outputs.Out;
                PageGenerator.GenerateTestOutputPage(outputPath, output, "./../../" + _test.TestHref);
                _test.HasOutput = true;
            }
            _test.AddScreenshots(ScreenshotHelper.GetScreenshots(NunitGoHelper.Screenshots));

            var testPath = _test.AttachmentsPath + Output.Outputs.TestHtml;
            var testHtml = _test.GenerateTestPage(testPath);
            _test.Save(_test.AttachmentsPath + Output.Outputs.TestXml);

            if ((_test.IsBroken() || _test.IsFailed()) && NunitGoHelper.Configuration.SendEmails 
                && _subscription != null)
            {
                var mailSubject = String.Format("Test '{0}' was broken or failed", _test.Name);
                EmailHelper.Send(NunitGoHelper.Configuration.MailFromList, _subscription.TargetEmails, mailSubject, testHtml);
                
            }

            PageGenerator.GenerateStyleFile(NunitGoHelper.Output);

            var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
            var stats = new MainStatistics(tests);
            tests.GenerateTimelinePage(Path.Combine(NunitGoHelper.Output, Output.Outputs.Timeline));
            stats.GenerateMainStatisticsPage(Path.Combine(NunitGoHelper.Output, Output.Outputs.TestStatistics));
            tests.GenerateTestListPage(Path.Combine(NunitGoHelper.Output, Output.Outputs.TestList));
            tests.GenerateReportMainPage(NunitGoHelper.Output, stats);

        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

    }
}
