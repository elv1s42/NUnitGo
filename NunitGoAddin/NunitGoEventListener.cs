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
        private static string _outputPath = Helper.Output;
        private readonly List<ExtraTestInfo> _allTests = new List<ExtraTestInfo>();
        private ExtraTestInfo _currentTest;
        private string _mainName;
        private List<Guid> _guids;
        private TestResult _fullTestListResult;
        private List<TestResult> _listOfResults;

        static NunitGoEventListener()
        {
            try
            {
                Log.Clean();
                CreateDirectories();
            }
            catch (Exception e)
            {
                Log.Write("Exception! " + e.Message + " " + e.StackTrace);
            }
        }

        private TestResult GenerateResultFromList(IEnumerable<TestResult> listOfResults)
        {
            var testName = new TestName
            {
                Name = _mainName,
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
            while (_guids.Any() && _guids.Any(x => x.Equals(guid)))
            {
                guid = Guid.NewGuid();
            }
            _guids.Add(guid);
            return guid;
        }

        private static void CreateDirectories()
        {
            try
            {
                _outputPath = Helper.Output;
                if (Directory.Exists(_outputPath))
                {
                    Log.Write("Directory " + _outputPath + " already exists, deleting it...");
                    Directory.Delete(_outputPath, true);
                }
                Log.Write("Creating directory: " + _outputPath);
                Directory.CreateDirectory(_outputPath);
                Directory.CreateDirectory(_outputPath + @"\Attachments");
                Directory.CreateDirectory(_outputPath + @"\Screenshots");
            }
            catch (Exception ex)
            {
                Log.Write("Exception in CreateDirectories! " + ex.Message + ", " + ex.StackTrace);
            }
        }

        private void GenerateReport(TestResult result)
        {
            var saveOutput = Helper.SaveOutput;
            var outputPath = Helper.Output;
            Log.Write("Generating report...");
            if(saveOutput) _allTests.Save(_outputPath + @"\" + "ExtraInfo.xml");
            var xmlResultList = new TestResultXml(_fullTestListResult);
            if (saveOutput) xmlResultList.Save(outputPath + @"\" + "_fullTestListResult.xml");
            var xmlResult = new TestResultXml(result);
            if (saveOutput) xmlResult.Save(outputPath + @"\" + "xmlResult.xml");
            var testResults = new TestResults(xmlResult);
            if (saveOutput) NunitXmlReader.Save(testResults, outputPath + @"\" + "testResults.xml");
            var fullSuite = ResultsAnalyzer.GetFullSuite(testResults, _allTests);
            if (saveOutput) NunitXmlReader.Save(fullSuite, outputPath + @"\" + "FullSuite.xml");
            PageGenerator.GenerateReport(fullSuite, _allTests, outputPath);
            Log.Write("Generating report: DONE.");
        }
        
        public override void RunStarted(string name, int testCount)
        {
            try
            {
                _guids = new List<Guid>();
                _listOfResults = new List<TestResult>();
                _mainName = name;
                Log.Write("RunStarted: " + _mainName + ", testCount = " + testCount);
                CreateDirectories();
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
                _allTests.Save(_outputPath + @"\" + "ExtraInfo.xml");
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
                if (_currentTest.FullTestName.Equals("")) 
                    _currentTest.FullTestName = result.FullName;
                
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
            var testAttachPath = _outputPath + @"\" + "Attachments" + @"\" + _currentTest.Guid + @"\";

            Directory.CreateDirectory(testAttachPath);

            var xmlResult = new TestResultXml(result);
            xmlResult.Save(testAttachPath + "Result.xml");

            if(_out.Length > 0)
            {
                _currentTest.OutPath = testAttachPath + Structs.Outputs.Out;
                PageGenerator.GenerateOutputPage(_currentTest.OutPath, _out.ToString());
                /*var sw = File.AppendText(_currentTest.OutPath);
                try
                {
                    sw.WriteLine(_out.ToString());
                }
                finally
                {
                    sw.Close();
                }*/
            }
            if (_trace.Length > 0)
            {
                _currentTest.TracePath = testAttachPath + Structs.Outputs.Trace;
                var sw = File.AppendText(_currentTest.TracePath);
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
                _currentTest.LogPath = testAttachPath + Structs.Outputs.Log;
                var sw = File.AppendText(_currentTest.LogPath);
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
                _currentTest.ErrorPath = testAttachPath + Structs.Outputs.Error;
                var sw = File.AppendText(_currentTest.ErrorPath);
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
