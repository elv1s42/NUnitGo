using System;
using System.IO;
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
                DateTimeStart = DateTime.Now
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
            _test.Result = context.Result.Outcome != null ? context.Result.Outcome.Status.ToString() : "Unknown";
            _test.Guid = Guid.NewGuid();
            
            if(!_test.Result.Equals("Passed")) Helper.TakeScreenshot(DateTime.Now);

            var testPath = Helper.Output + @"\" + "Attachments" + @"\" + _test.Guid + @"\";
            Directory.CreateDirectory(testPath);
            var output = TestContext.Out.ToString();
            if (!output.Equals(String.Empty))
            {
                var outputPath = testPath + Structs.Outputs.Out;
                PageGenerator.GenerateOutputPage(outputPath, output);
            }

            _test.Save(testPath + "test.xml");
            
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }// | ActionTargets.Suite; }
        }
    }
}
