using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NunitGo.Attributes;
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
        private Guid _guid;
        private readonly string _projectName;
        private readonly string _className;
        private readonly string _testName;
        private NunitGoTest _test;
        private DateTime _start;
        private DateTime _finish;

        public static Guid TestGuid = Guid.Empty;

        public NunitGoActionAttribute(string testGuidString = "", string projectName = "", string className = "", 
            string testName = "")
        {
            _guid = testGuidString.Equals("")
                    ? Guid.Empty
                    : new Guid(testGuidString);
            _projectName = projectName;
            _className = className;
            _testName = testName;
        }

        public NunitGoActionAttribute(Guid guid, string projectName = "", string className = "",
            string testName = "")
        {
            _guid = guid;
            _projectName = projectName;
            _className = className;
            _testName = testName;
        }

        public void BeforeTest(ITest test)
        {
            //Log.Write("START:" + test.FullName);
            if (!NunitGoHelper.GenerateReport) return;

            NunitGoHelper.CreateDirectories();
            _start = DateTime.Now;
        }

        public void AfterTest(ITest test)
        {
            //Log.Write("FINISH: " + test.FullName);

            _finish = DateTime.Now;
            var context = TestContext.CurrentContext;
            var outputPath = NunitGoHelper.Output;
            var configuration = NunitGoHelper.Configuration;
            var doGenerate = NunitGoHelper.GenerateReport;

            if (_guid.Equals(Guid.Empty))
            {
                _guid =  TestGuid.Equals(Guid.Empty) ? Guid.NewGuid() : TestGuid;
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
                Guid = _guid,
                Screenshots = new List<Screenshot>()
            };

            var isSuccess = _test.IsSuccess();
            
            if(!isSuccess && NunitGoHelper.TakeScreenshotAfterTestFailed) _test.TakeScreenshot();

            _test.AttachmentsPath = outputPath + @"\" + "Attachments" + @"\" + _test.Guid + @"\";
            Directory.CreateDirectory(_test.AttachmentsPath);
            _test.TestHref = "Attachments" + @"/" + _test.Guid + @"/" + Output.Outputs.TestHtml;
            _test.LogHref = Output.Outputs.Out;
            var testContextOutput = TestContext.Out.ToString();
            if (!testContextOutput.Equals(String.Empty))
            {
                var testOutputPath = _test.AttachmentsPath + Output.Outputs.Out;
                PageGenerator.GenerateTestOutputPage(testOutputPath, testContextOutput, "./../../" + _test.TestHref);
                _test.HasOutput = true;
            }
            _test.AddScreenshots(ScreenshotHelper.GetScreenshots(NunitGoHelper.Screenshots));

            var testPath = _test.AttachmentsPath + Output.Outputs.TestHtml;
             _test.GenerateTestPage(testPath);
            _test.Save(_test.AttachmentsPath + Output.Outputs.TestXml);

            var subs = test.Method.MethodInfo.GetCustomAttributes<NunitGoSubscriptionAttribute>();
            foreach (var sub in subs)
            {
                var subscription = configuration.Subsciptions.FirstOrDefault(x => x.Name.Equals(sub.Name));
                if (subscription != null)
                {
                    if((sub.UnsuccessfulOnly && !isSuccess) || (!sub.UnsuccessfulOnly))
                        EmailHelper.Send(configuration.SendFromList, subscription.TargetEmails, _test);
                }
                
                if (sub.FullPath != null)
                {
                    subscription = XmlHelper.Load<Subsciption>(sub.FullPath); 
                    if ((sub.UnsuccessfulOnly && !isSuccess) || (!sub.UnsuccessfulOnly))
                        EmailHelper.Send(configuration.SendFromList, subscription.TargetEmails, _test);
                }
            }

            var singleSubs = test.Method.MethodInfo.GetCustomAttributes<NunitGoSingleSubscriptionAttribute>();
            foreach (var singleSub in singleSubs)
            {
                var singleTestSubscription =
                    configuration.SingleTestSubscriptions.FirstOrDefault(x => x.TestGuid.Equals(_test.Guid));
                if (singleTestSubscription != null)
                {
                    if ((singleSub.UnsuccessfulOnly && !isSuccess) || (!singleSub.UnsuccessfulOnly))
                        EmailHelper.Send(configuration.SendFromList, singleTestSubscription.TargetEmails, _test); 
                }
                else
                {
                    if (singleSub.FullPath != null)
                    {
                        var singleSubFromXml = XmlHelper.Load<SingleTestSubscription>(singleSub.FullPath);
                        if ((singleSub.UnsuccessfulOnly && !isSuccess) || (!singleSub.UnsuccessfulOnly))
                            EmailHelper.Send(configuration.SendFromList, singleSubFromXml.TargetEmails, _test);
                    } 
                }
                
            }
            if (doGenerate)
            {
                PageGenerator.GenerateStyleFile(outputPath);

                var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
                var stats = new MainStatistics(tests);
                tests.GenerateTimelinePage(Path.Combine(outputPath, Output.Outputs.Timeline));
                stats.GenerateMainStatisticsPage(Path.Combine(outputPath, Output.Outputs.TestStatistics));
                tests.GenerateTestListPage(Path.Combine(outputPath, Output.Outputs.TestList));
                tests.GenerateReportMainPage(outputPath, stats);
            }
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

    }
}
