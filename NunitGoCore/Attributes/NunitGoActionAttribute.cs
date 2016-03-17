using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnitGoCore.CustomElements;
using NUnitGoCore.CustomElements.NunitTestHtml;
using NUnitGoCore.CustomElements.ReportSections.MainInformationSection;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.NunitGoItems.Events;
using NUnitGoCore.NunitGoItems.Screenshots;
using NUnitGoCore.NunitGoItems.Subscriptions;
using NUnitGoCore.Utils;

namespace NUnitGoCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        private Guid _guid;
        private readonly string _projectName;
        private readonly string _className;
        private readonly string _testName;
        private static NunitGoConfiguration _configuration;

        private static string _outputPath;
        private static string _screenshotsPath;
        private static string _attachmentsPath;

        private NunitGoTest _nunitGoTest;
        private DateTime _start;
        private DateTime _finish;
        private string _testOutput;

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
            
            _outputPath = _configuration.LocalOutputPath;
            _screenshotsPath = Output.GetScreenshotsPath(_outputPath);
            _attachmentsPath = Output.GetAttachmentsPath(_outputPath);
        }
        
        public void BeforeTest(ITest test)
        {
            CreateDirectories();
            NunitGo.SetUp();
            _start = DateTime.Now;
        }

        public void AfterTest(ITest test)
        {
            _finish = DateTime.Now;
            _guid = _guid.Equals(Guid.Empty) 
                ? (NunitGo.TestGuid.Equals(Guid.Empty) ? Guid.NewGuid() : NunitGo.TestGuid)
                : _guid;
            _testOutput = TestContext.Out.ToString();
            
            var context = TestContext.CurrentContext;
            var relativeTestHref = "Attachments" + @"/" + _guid + @"/" + Output.Files.GetTestHtmlName(_finish);
            
            _nunitGoTest = new NunitGoTest
            {
                DateTimeStart = _start,
                DateTimeFinish = _finish,
                TestDuration = (_finish - _start).TotalSeconds,
                FullName = test.FullName,
                ProjectName = _projectName.Equals("") ? test.FullName.Split('.').First() : _projectName,
                ClassName = _className.Equals("") ? test.FullName.Split('.').Skip(1).First() : _className,
                Name = _testName.Equals("") ? test.Name : _testName,
                TestStackTrace = context.Result.StackTrace ?? "",
                TestMessage = context.Result.Message ?? "",
                Result = context.Result.Outcome != null ? context.Result.Outcome.ToString() : "Unknown",
                Guid = _guid,
                HasOutput = !_testOutput.Equals(string.Empty),
                AttachmentsPath = _attachmentsPath + _guid + @"\",
                TestHrefRelative = relativeTestHref,
                TestHrefAbsolute = _configuration.ServerLink + relativeTestHref,
                Events = NunitGo.GetEvents()
            };

            Directory.CreateDirectory(_nunitGoTest.AttachmentsPath);
            
            TakeScreenshotIfFailed();
            AddScreenshots();
            SaveTestFiles();
            SendEmails(_nunitGoTest.IsSuccess(), test);
            SendEmailsForEvents(test);
            GenerateReport();
            Flush();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        internal void SetTestGuid(string guid)
        {
            _guid = new Guid(guid);
        }

        internal string GetTestGuid()
        {
            return _guid.ToString();
        }

        private void SaveTestFiles()
        {
            try
            {
                _nunitGoTest.SaveAsXml(_nunitGoTest.AttachmentsPath + Output.Files.GetTestXmlName(_nunitGoTest.DateTimeFinish));
                var testVersions = NunitGoTestHelper.GetTestsFromFolder(_nunitGoTest.AttachmentsPath);
                var chartId = Output.GetHistoryChartId(_nunitGoTest.Guid, _nunitGoTest.DateTimeFinish);
                var highstockHistory = new NunitGoJsHighstock(testVersions, chartId);
                highstockHistory.SaveScript(_nunitGoTest.AttachmentsPath);
                
                var testPath = _nunitGoTest.AttachmentsPath + Output.Files.GetTestHtmlName(_nunitGoTest.DateTimeFinish);
                _nunitGoTest.GenerateTestPage(testPath, _testOutput, Output.GetTestHistoryScriptName(_nunitGoTest.DateTimeFinish));
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in SaveTestFiles");
            }
        }

        private void SendEmails(bool isSuccess, ITest test)
        {
            try
            {
                if (!_configuration.SendEmails) return;

                var subs = test.Method.MethodInfo.GetCustomAttributes<SubscriptionAttribute>();
                foreach (var sub in subs)
                {
                    var sendCondition = (sub.UnsuccessfulOnly && !isSuccess) || (!sub.UnsuccessfulOnly);
                    if (!sendCondition) continue;

                    var subscription = _configuration.Subsciptions.FirstOrDefault(x => x.Name.Equals(sub.Name));
                    if (subscription != null)
                    {
                        EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                    else if (sub.FullPath != null)
                    {
                        subscription = XmlHelper.Load<Subsciption>(sub.FullPath);
                        EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                    else if (sub.Targets.Any())
                    {
                        EmailHelper.Send(_configuration.SendFromList, sub.Targets,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                }

                var singleSubs = test.Method.MethodInfo.GetCustomAttributes<SingleTestSubscriptionAttribute>();
                foreach (var singleSub in singleSubs)
                {
                    var sendCondition = (singleSub.UnsuccessfulOnly && !isSuccess) || (!singleSub.UnsuccessfulOnly);
                    if (!sendCondition) continue;

                    var singleTestSubscription =
                        _configuration.SingleTestSubscriptions.FirstOrDefault(x => x.TestGuid.Equals(_nunitGoTest.Guid));
                    if (singleTestSubscription != null)
                    {
                        EmailHelper.Send(_configuration.SendFromList, singleTestSubscription.TargetEmails,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                    else if (singleSub.FullPath != null)
                    {
                        var singleSubFromXml = XmlHelper.Load<SingleTestSubscription>(singleSub.FullPath);
                        EmailHelper.Send(_configuration.SendFromList, singleSubFromXml.TargetEmails,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                    else if (singleSub.Targets.Any())
                    {
                        EmailHelper.Send(_configuration.SendFromList, singleSub.Targets,
                            _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail);
                    }
                }

                var eventSubs = test.Method.MethodInfo.GetCustomAttributes<EventDurationSubscriptionAttribute>();
                foreach (var sub in eventSubs)
                {
                    var currentTestVersions = NunitGoTestHelper.GetTestsFromFolder(_nunitGoTest.AttachmentsPath);
                    var subscription = _configuration.EventDurationSubscriptions.FirstOrDefault(x => x.Name.Equals(sub.Name));
                    Log.Write("Count: " + currentTestVersions.Count);
                    if (currentTestVersions.Count > 1)
                    {
                        var previousTest = currentTestVersions
                            .OrderByDescending(x => x.DateTimeFinish)
                            .Skip(1)
                            .First(x => x.Events.Any(e => e.Name.Equals(sub.EventName)));
                        var previuosEvent = previousTest.Events.First(x => x.Name.Equals(sub.EventName));
                        var currentEvent = _nunitGoTest.Events.First(x => x.Name.Equals(sub.EventName));

                        if (Math.Abs(currentEvent.Duration - previuosEvent.Duration) > sub.MaxDifference)
                        {
                            if (subscription != null)
                            {
                                EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                            else if (sub.FullPath != null)
                            {
                                subscription = XmlHelper.Load<EventDurationSubscription>(sub.FullPath);
                                EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                            else if (sub.Targets.Any())
                            {
                                EmailHelper.Send(_configuration.SendFromList, sub.Targets,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in SendEmail");
            }

        }

        private void SendEmailsForEvents(ITest test)
        {
            try
            {
                if (!_configuration.SendEmails) return;

                var eventSubs = test.Method.MethodInfo.GetCustomAttributes<EventDurationSubscriptionAttribute>();
                foreach (var sub in eventSubs)
                {
                    var currentTestVersions = NunitGoTestHelper.GetTestsFromFolder(_nunitGoTest.AttachmentsPath);
                    var subscription = _configuration.EventDurationSubscriptions.FirstOrDefault(x => x.Name.Equals(sub.Name));
                    Log.Write("Count: " + currentTestVersions.Count);
                    if (currentTestVersions.Count > 1)
                    {
                        var previousTest = currentTestVersions
                            .OrderByDescending(x => x.DateTimeFinish)
                            .Skip(1)
                            .First(x => x.Events.Any(e => e.Name.Equals(sub.EventName)));
                        var previuosEvent = previousTest.Events.First(x => x.Name.Equals(sub.EventName));
                        var currentEvent = _nunitGoTest.Events.First(x => x.Name.Equals(sub.EventName));

                        if (currentEvent.Duration - previuosEvent.Duration > sub.MaxDifference)
                        {
                            if (subscription != null)
                            {
                                EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                            else if (sub.FullPath != null)
                            {
                                subscription = XmlHelper.Load<EventDurationSubscription>(sub.FullPath);
                                EmailHelper.Send(_configuration.SendFromList, subscription.TargetEmails,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                            else if (sub.Targets.Any())
                            {
                                EmailHelper.Send(_configuration.SendFromList, sub.Targets,
                                    _nunitGoTest, _screenshotsPath, _configuration.AddLinksInsideEmail,
                                    true, sub.EventName, previuosEvent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in SendEmailsForEvents");
            }

        }

        public void GenerateReport()
        {
            try
            {
                if (!_configuration.GenerateReport) return;

                PageGenerator.GenerateStyleFile(_outputPath);

                var tests = NunitGoTestHelper.GetNewestTests(_attachmentsPath).OrderBy(x => x.DateTimeFinish).ToList();
                var stats = new MainStatistics(tests);
                var statsChart = new MainInfoChart(stats, Output.GetStatsPieId());
                statsChart.SaveScript(_outputPath);
                tests.GenerateTimelinePage(Path.Combine(_outputPath, Output.Files.TimelineFile));
                stats.GenerateMainStatisticsPage(Path.Combine(_outputPath, Output.Files.TestStatisticsFile));
                tests.GenerateTestListPage(Path.Combine(_outputPath, Output.Files.TestListFile));
                tests.GenerateReportMainPage(_outputPath, stats);

            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in GenerateReport");
            }
        }

        private void TakeScreenshotIfFailed()
        {
            try
            {
                if (!_nunitGoTest.IsSuccess() && _configuration.TakeScreenshotAfterTestFailed)
                {
                    var now = DateTime.Now;
                    _nunitGoTest.Screenshots.Add(new Screenshot(now));
                    Taker.TakeScreenshot(_screenshotsPath, now);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in TakeScreenshot");
            }
        }

        private void AddScreenshots()
        {
            _nunitGoTest.Screenshots.AddRange(NunitGo.GetScreenshots());
        }

        private static void CreateDirectories()
        {
            Directory.CreateDirectory(_outputPath);
            Directory.CreateDirectory(_screenshotsPath);
            Directory.CreateDirectory(_attachmentsPath);
        }

        private void Flush()
        {
            _guid = Guid.Empty;
            _nunitGoTest = new NunitGoTest();
            _start = default(DateTime);
            _finish = default(DateTime);
            NunitGo.TearDown();
        }

    }
}
