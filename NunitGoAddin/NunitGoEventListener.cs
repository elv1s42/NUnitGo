using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using NUnit.Core;

namespace NunitGoAddin
{
    public class NunitGoEventListener : EventListener
    {

        private StringBuilder _log = new StringBuilder();
        private StringBuilder _stdErr = new StringBuilder();
        private StringBuilder _stdOut = new StringBuilder();
        private StringBuilder _trace = new StringBuilder();
        private static string _outputPath;

        static NunitGoEventListener()
        {
            try
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

                _outputPath =
                    XDocument.Load(path + "/config.xml")
                        .Descendants()
                        .First(x => x.Name.LocalName.Equals("output-path"))
                        .Value + "/";

                if (Directory.Exists(_outputPath))
                {
                    Directory.Delete(_outputPath, true);
                }
                Directory.CreateDirectory(_outputPath);
            }
            catch (Exception e)
            {
                Log.Write("Exception! " + e.Message + " " + e.StackTrace);
            }
        }

        public override void TestStarted(TestName testName)
        {
            try
            {
                Log.Write("TestStarted: " + testName);

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
                if (result.IsError)
                {
                    TakeScreenshot();
                    Log.Write("TestFinished: Error " + result.StackTrace + " " + result.Message);
                }
                else if (result.IsFailure)
                {
                    TakeScreenshot();
                    Log.Write("TestFinished: Failure " + result.StackTrace + " " + result.Message);
                }
                else if (!result.Executed)
                {
                    if (result.ResultState == ResultState.Cancelled)
                    {
                        TakeScreenshot();
                        Log.Write("TestFinished: Cancelled " + result.StackTrace + " " + result.Message);
                    }
                    else
                    {
                        TakeScreenshot();
                        Log.Write("TestFinished: Pending " + result.StackTrace + " " + result.Message);
                    }
                }
                //TODO: write output!
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
                    _stdOut.Append(testOutput.Text);
                    break;
                case TestOutputType.Trace:
                    _trace.Append(testOutput.Text);
                    break;
                case TestOutputType.Log:
                    _log.Append(testOutput.Text);
                    break;
                case TestOutputType.Error:
                    _stdErr.Append(testOutput.Text);
                    break;
            }
        }

        private void WriteOutputToAttachment()
        {
            //TODO: add output
            
            _stdOut = new StringBuilder();
            _trace = new StringBuilder();
            _log = new StringBuilder();
            _stdErr = new StringBuilder();
        }

        private void TakeScreenshot()
        {
            //TODO: take screenshot
        }
    }
}
