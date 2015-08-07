using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Logger;
using NUnit.Core;
using Utils;

namespace NunitGoAddin
{
    public class NunitGoEventListener : EventListener
    {
        private StringBuilder _log = new StringBuilder();
        private StringBuilder _error = new StringBuilder();
        private StringBuilder _out = new StringBuilder();
        private StringBuilder _trace = new StringBuilder();
        private static readonly string OutputPath = Locator.Output;
        private readonly List<ExtraTestInfo> _allTests = new List<ExtraTestInfo>();
        private ExtraTestInfo _currentTest;

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
                Log.Write("RunStarted: " + name + ", testCount = " + testCount);
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in RunStarted {0}, {1}, {2}", name, e.Message, e.StackTrace));
            }
        }

        private void AfterRunActions()
        {
            var xs = new XmlSerializer(typeof(List<ExtraTestInfo>));
            var sw = new StreamWriter(OutputPath + @"\" + "ExtraInfo.xml");
            xs.Serialize(sw, _allTests);
            //Program.GenerateReport();
        }

        public override void RunFinished(TestResult result)
        {
            try
            {
                Log.Write("RunFinished :)");
                AfterRunActions();
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
                Log.Write("RunFinished with exception: " + exception.Message + ", Trace = " + exception.StackTrace);
                AfterRunActions();
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
                var guid = Guid.NewGuid();
                while (_allTests.Any() && _allTests.Any(x => x.Guid.Equals(guid)))
                {
                    guid = Guid.NewGuid();
                }
                _currentTest.Guid = guid;
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
                try
                {
                    _currentTest.AssertCount = result.AssertCount;

                }
                catch (Exception)
                {
                    Log.Write("TestFinished: Error in _currentTest " + result.StackTrace + " " + result.Message);
                }
                
                if (result.IsError)
                {
                    TakeScreenshot(_currentTest.FinishDate);
                    Log.Write("TestFinished: Error " + result.StackTrace + " " + result.Message);
                }
                else if (result.IsFailure)
                {
                    TakeScreenshot(_currentTest.FinishDate);
                    Log.Write("TestFinished: Failure " + result.StackTrace + " " + result.Message);
                }
                else if (!result.Executed)
                {
                    if (result.ResultState == ResultState.Cancelled)
                    {
                        TakeScreenshot(_currentTest.FinishDate);
                        Log.Write("TestFinished: Cancelled " + result.StackTrace + " " + result.Message);
                    }
                    else
                    {
                        TakeScreenshot(_currentTest.FinishDate);
                        Log.Write("TestFinished: Pending " + result.StackTrace + " " + result.Message);
                    }
                }
                _allTests.Add(_currentTest);
                Log.Write("TestFinished! tests count: " + _allTests.Count);
                WriteOutputToAttachment();
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
                Log.Write("SuiteStarted: " + testName);
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
                Log.Write("SuiteFinished: " + result);
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

        private void WriteOutputToAttachment()
        {
            var testAttachPath = OutputPath + @"\Attachments\" + _currentTest.Guid + @"\";
            _currentTest.Log = testAttachPath + "log.txt";
            _currentTest.Trace = testAttachPath + "trace.txt";
            _currentTest.Error = testAttachPath + "error.txt";
            _currentTest.Out = testAttachPath + "out.txt";

            Directory.CreateDirectory(testAttachPath);
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

        private string GetScreenName(DateTime now, ImageFormat format = null)
        {
            format = format ?? ImageFormat.Png;
            return String.Format("screenshot_{0}.{1}", now.ToString("yyyyMMddHHmmssfff"), format.ToString().ToLower());
        }

        private void TakeScreenshot(DateTime creationTime = default(DateTime))
        {
            var format = ImageFormat.Png;
            var now = DateTime.Now;
            var screenPath = Locator.Screenshots + @"\";
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
