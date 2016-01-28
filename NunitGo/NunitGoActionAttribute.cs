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
        private readonly NunitGoConfiguration _configuration;
        private NunitGoTest _nunitGoTest;
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
            _configuration = NunitGoHelper.Configuration;
        }
        
        public void BeforeTest(ITest test)
        {
            //Log.Write("START:" + test.FullName);
            if (!_configuration.GenerateReport) return;

            _start = DateTime.Now;
        }

        public void AfterTest(ITest test)
        {
            //Log.Write("FINISH: " + test.FullName);
            
            _finish = DateTime.Now;
            _guid = _guid.Equals(Guid.Empty) 
                ? (TestGuid.Equals(Guid.Empty) ? Guid.NewGuid() : TestGuid)
                : _guid;

            var context = TestContext.CurrentContext;
            var outputPath = _configuration.LocalOutputPath;
            var screenshotsPath = outputPath + @"\Screenshots\";
            var attachmentsPath = outputPath + @"\Attachments\";

            _nunitGoTest = new NunitGoTest
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
                Screenshots = new List<Screenshot>(),
                HasOutput = !TestContext.Out.ToString().Equals(String.Empty),
                AttachmentsPath = attachmentsPath + _guid + @"\",
                TestHref = "Attachments" + @"/" + _guid + @"/" + Output.Outputs.TestHtml,
                LogHref = Output.Outputs.Out
            };

            CreateDirectories();
            
            TakeScreenshot(screenshotsPath);
            
            _nunitGoTest.AddScreenshots(ScreenshotHelper.GetScreenshots(screenshotsPath));
            _nunitGoTest.Save(_nunitGoTest.AttachmentsPath + Output.Outputs.TestXml);

            SendEmails(_nunitGoTest.IsSuccess(), test, screenshotsPath);
            
            GenerateReport();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        private void SendEmails(bool isSuccess, ITest test, string screenshotsPath)
        {
            try
            {
                if (!_configuration.SendEmails) return;

                var subs = test.Method.MethodInfo.GetCustomAttributes<NunitGoSubscriptionAttribute>();
                foreach (var sub in subs)
                {
                    var subscription = _configuration.Subsciptions.FirstOrDefault(x => x.Name.Equals(sub.Name));
                    if (subscription != null)
                    {
                        if ((sub.UnsuccessfulOnly && !isSuccess) || (!sub.UnsuccessfulOnly))
                            EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails, _nunitGoTest, screenshotsPath);
                    }

                    if (sub.FullPath != null)
                    {
                        subscription = XmlHelper.Load<Subsciption>(sub.FullPath);
                        if ((sub.UnsuccessfulOnly && !isSuccess) || (!sub.UnsuccessfulOnly))
                            EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails, _nunitGoTest, screenshotsPath);
                    }
                }

                var singleSubs = test.Method.MethodInfo.GetCustomAttributes<NunitGoSingleSubscriptionAttribute>();
                foreach (var singleSub in singleSubs)
                {
                    var singleTestSubscription =
                        _configuration.SingleTestSubscriptions.FirstOrDefault(x => x.TestGuid.Equals(_nunitGoTest.Guid));
                    if (singleTestSubscription != null)
                    {
                        if ((singleSub.UnsuccessfulOnly && !isSuccess) || (!singleSub.UnsuccessfulOnly))
                            EmailHelper.Send(_configuration.SendFromList, singleTestSubscription.TargetEmails, _nunitGoTest, screenshotsPath);
                    }
                    else
                    {
                        if (singleSub.FullPath != null)
                        {
                            var singleSubFromXml = XmlHelper.Load<SingleTestSubscription>(singleSub.FullPath);
                            if ((singleSub.UnsuccessfulOnly && !isSuccess) || (!singleSub.UnsuccessfulOnly))
                                EmailHelper.Send(_configuration.SendFromList, singleSubFromXml.TargetEmails, _nunitGoTest, screenshotsPath);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in SendEmail");
            }
            
        }

        private void GenerateReport()
        {
            try
            {
                if (!_configuration.GenerateReport) return;

                if (_nunitGoTest.HasOutput)
                {
                    var testOutputPath = _nunitGoTest.AttachmentsPath + Output.Outputs.Out;
                    PageGenerator.GenerateTestOutputPage(testOutputPath, TestContext.Out.ToString(), "./../../" + _nunitGoTest.TestHref);
                    _nunitGoTest.HasOutput = true;
                }
                var testPath = _nunitGoTest.AttachmentsPath + Output.Outputs.TestHtml;
                _nunitGoTest.GenerateTestPage(testPath);

                var outputPath = _configuration.LocalOutputPath;
                PageGenerator.GenerateStyleFile(outputPath);

                var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
                var stats = new MainStatistics(tests);
                tests.GenerateTimelinePage(Path.Combine(outputPath, Output.Outputs.Timeline));
                stats.GenerateMainStatisticsPage(Path.Combine(outputPath, Output.Outputs.TestStatistics));
                tests.GenerateTestListPage(Path.Combine(outputPath, Output.Outputs.TestList));
                tests.GenerateReportMainPage(outputPath, stats);

            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in GenerateReport");
            }
        }

        private void TakeScreenshot(string screenshotsPath)
        {
            try
            {
                if (!_nunitGoTest.IsSuccess() && _configuration.TakeScreenshotAfterTestFailed)
                    _nunitGoTest.TakeScreenshot(screenshotsPath);
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in TakeScreenshot");
            }
        }

        private void CreateDirectories()
        {
            var localOutput = _configuration.LocalOutputPath;
            Directory.CreateDirectory(localOutput);
            Directory.CreateDirectory(localOutput + @"\Screenshots");
            Directory.CreateDirectory(localOutput + @"\Attachments");
            Directory.CreateDirectory(_nunitGoTest.AttachmentsPath);
        }
    }
}
