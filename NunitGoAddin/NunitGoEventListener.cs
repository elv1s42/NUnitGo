using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlCustomElements;
using NUnit.Core;
using NunitResultAnalyzer;
using NunitResultAnalyzer.XmlClasses;
using Utils;
using Utils.Extensions;
using Utils.XmlTypes;

namespace NunitGoAddin
{
    public class NunitGoEventListener : EventListener
    {
        private StringBuilder _log = new StringBuilder();
        private StringBuilder _error = new StringBuilder();
        private StringBuilder _out = new StringBuilder();
        private StringBuilder _trace = new StringBuilder();
        private static readonly string OutputPath = Helper.Output;
        private readonly List<ExtraTestInfo> _allTests = new List<ExtraTestInfo>();
        private ExtraTestInfo _currentTest;
        private string _mainName;
        private List<Guid> _guids;
        private TestResult _fullTestListResult;
        private List<TestResult> _listOfResults;

        private TestResult GenerateResultFromList(IEnumerable<TestResult> listOfResults)
        {
            var testName = new TestName
            {
                FullName = _mainName
            };
            var result = new TestResult(testName);
            foreach (var testResult in listOfResults)
            {
                result.AddResult(testResult);
            }
            return result;
        }

        private Guid GetGuid()
        {
            var guid = Guid.NewGuid();
            while (_guids.Any(x => x.Equals(guid)))
            {
                guid = Guid.NewGuid();
            }
            _guids.Add(guid);
            return guid;
        }

        private void GenerateReport(TestResult result)
        {
            Log.Write("Generating report...");
            _allTests.Save(OutputPath + @"\" + "ExtraInfo.xml");
            var xmlResultList = new TestResultXml(_fullTestListResult);
            xmlResultList.Save(Helper.Output + @"\" + "ListResult.xml");
            var xmlResult = new TestResultXml(result);
            xmlResult.Save(Helper.Output + @"\" + "Result.xml");
            var fullSuite = ResultsAnalyzer.GetFullSuite(new TestResults(xmlResult), _allTests);
            NunitXmlReader.Save(fullSuite, Helper.Output + @"\" + "FullSuite.xml");
            PageGenerator.GenerateReport(fullSuite, Helper.Output);
            Log.Write("Generating report: DONE.");
        }

        static NunitGoEventListener()
        {
            try
            {
                if (Directory.Exists(OutputPath))
                {
                    Directory.Delete(OutputPath, true);
                }
                Directory.CreateDirectory(OutputPath);
                Directory.CreateDirectory(OutputPath + @"\Attachments");
                Log.Clean();
            }
            catch (Exception e)
            {
                Log.Write("Exception! " + e.Message + " " + e.StackTrace);
            }
        }

        public override void RunStarted(string name, int testCount)
        {
            try
            {
                _guids = new List<Guid>();
                _listOfResults = new List<TestResult>();
                _mainName = name;
                Log.Write("RunStarted: " + _mainName + ", testCount = " + testCount);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in RunStarted {0}, {1}, {2}", name, e.Message, e.StackTrace));
            }
        }

        public override void RunFinished(TestResult result)
        {
            try
            {
                Log.Write("RunFinished :)");
                GenerateReport(result);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in RunFinished '{0}', {1}, {2}", result.Name, e.Message, e.StackTrace));
            }
        }

        public override void RunFinished(Exception exception)
        {
            try
            {
                _allTests.Save(OutputPath + @"\" + "ExtraInfo.xml");
                Log.Write("RunFinished with exception: " + exception.Message + ", Trace = " + exception.StackTrace);
                GenerateReport(_fullTestListResult);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in RunFinished {0}, {1}, {2}", e, e.Message, e.StackTrace));
            }
        }

        public override void TestStarted(TestName testName)
        {
            try
            {
                _currentTest = new ExtraTestInfo();
                Log.Write("TestStarted: " + testName.FullName);
                _currentTest.Guid = GetGuid();
                _currentTest.TestName = testName.Name;
                _currentTest.StartDate = DateTime.Now;
                _currentTest.FullTestName = testName.FullName;
                _currentTest.UniqueTestName = testName.UniqueName;
                _currentTest.TestId = testName.TestID.ToString();
                _currentTest.RunnerId = testName.RunnerID.ToString("D");
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in TestStarted {0}, {1}, {2}", testName, e.Message, e.StackTrace));
            }
        }

        public override void TestFinished(TestResult result)
        {
            try
            {
                _currentTest.FinishDate = DateTime.Now;
                _listOfResults.Add(result);
                
                try
                {
                    _currentTest.AssertCount = result.AssertCount;

                }
                catch (Exception)
                {
                    Log.Write("      TestFinished: Error in _currentTest " + result.StackTrace + " " + result.Message);
                }
                
                if (result.IsError)
                {
                    TakeScreenshot(_currentTest.FinishDate);
                    Log.Write("      TestFinished: Error " + result.StackTrace + " " + result.Message);
                }
                else if (result.IsFailure)
                {
                    TakeScreenshot(_currentTest.FinishDate);
                    Log.Write("      TestFinished: Failure " + result.StackTrace + " " + result.Message);
                }
                else if (!result.Executed)
                {
                    if (result.ResultState == ResultState.Cancelled)
                    {
                        TakeScreenshot(_currentTest.FinishDate);
                        Log.Write("      TestFinished: Cancelled " + result.StackTrace + " " + result.Message);
                    }
                    else
                    {
                        TakeScreenshot(_currentTest.FinishDate);
                        Log.Write("      TestFinished: Pending " + result.StackTrace + " " + result.Message);
                    }
                }
                Log.Write("      TestFinished! Tests done: " + _allTests.Count);
                WriteOutputToAttachment(result);
                _allTests.Add(_currentTest);
                _fullTestListResult = GenerateResultFromList(_listOfResults);
                if (Helper.AfterTestGeneration)
                    GenerateReport(_fullTestListResult);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in TestFinished {0}, {1}, {2}", result, e.Message, e.StackTrace));
            }
        }

        public override void SuiteStarted(TestName testName)
        {
            try
            {
                Log.Write("   SuiteStarted: " + testName);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in SuiteStarted {0}, {1}, {2}", testName, e.Message, e.StackTrace));
            }
        }

        public override void SuiteFinished(TestResult result)
        {
            try
            {
                Log.Write("   SuiteFinished: " + result.Test.TestType + ", " + result.FullName);
                
                if (Helper.AfterSuiteGeneration) GenerateReport(_fullTestListResult);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in SuiteFinished {0}, {1}, {2}", result, e.Message, e.StackTrace));
            }
        }

        public override void TestOutput(TestOutput testOutput)
        {
            switch (testOutput.Type)
            {
                case TestOutputType.Out:
                    _out.Append(testOutput.Text);
                    break;
                case TestOutputType.Trace:
                    _trace.Append(testOutput.Text);
                    break;
                case TestOutputType.Log:
                    _log.Append(testOutput.Text);
                    break;
                case TestOutputType.Error:
                    _error.Append(testOutput.Text);
                    break;
            }
        }

        private void WriteOutputToAttachment(TestResult result)
        {
            var testAttachPath = OutputPath + @"\Attachments\" + _currentTest.Guid + @"\";
            _currentTest.Log = testAttachPath + "log.txt";
            _currentTest.Trace = testAttachPath + "trace.txt";
            _currentTest.Error = testAttachPath + "error.txt";
            _currentTest.Out = testAttachPath + "out.txt";

            Directory.CreateDirectory(testAttachPath);

            var xmlResult = new TestResultXml(result);
            xmlResult.Save(testAttachPath + "Result.xml");

            if(_out.Length > 0)
            {
                var sw = File.AppendText(_currentTest.Out);
                try
                {
                    sw.WriteLine(_out.ToString());
                }
                finally
                {
                    sw.Close();
                }
            }
            if (_trace.Length > 0)
            {
                var sw = File.AppendText(_currentTest.Trace);
                try
                {
                    sw.WriteLine(_trace.ToString());
                }
                finally
                {
                    sw.Close();
                }
            }
            if (_log.Length > 0)
            {
                var sw = File.AppendText(_currentTest.Log);
                try
                {
                    sw.WriteLine(_log.ToString());
                }
                finally
                {
                    sw.Close();
                }
            }
            if (_error.Length > 0)
            {
                var sw = File.AppendText(_currentTest.Error);
                try
                {
                    sw.WriteLine(_error.ToString());
                }
                finally
                {
                    sw.Close();
                }
            }

            _out = new StringBuilder();
            _trace = new StringBuilder();
            _log = new StringBuilder();
            _error = new StringBuilder();
        }

        private static string GetScreenName(DateTime now, ImageFormat format = null)
        {
            format = format ?? ImageFormat.Png;
            return String.Format("screenshot_{0}.{1}", now.ToString("yyyyMMddHHmmssfff"), format.ToString().ToLower());
        }

        private static void TakeScreenshot(DateTime creationTime = default(DateTime))
        {
            var format = ImageFormat.Png;
            var now = DateTime.Now;
            var screenPath = Helper.Screenshots + @"\";
            Directory.CreateDirectory(screenPath);

            creationTime = creationTime.Equals(default(DateTime)) ? now : creationTime;

            var screenName = GetScreenName(creationTime, format);

            using (var bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);
                    Log.Write("Saving... " + screenPath + screenName);

                    var file = screenPath + screenName;

                    bmpScreenCapture.Save(file, format);

                    var fileInfo = new FileInfo(file);
                    fileInfo.Refresh();
                    fileInfo.CreationTime = creationTime;

                }
            }
        }
    }
}
