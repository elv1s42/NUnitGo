using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NunitGo.HtmlCustomElements;
using NunitGo.Utils;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        private readonly string _guid;
        private readonly string _projectName;
        private readonly string _className;
        private readonly string _testName;
        private NunitGoTest _test;
        private DateTime _start;
        private DateTime _finish;
        public static Guid TestGuid = Guid.Empty;
        
        public NunitGoActionAttribute(string guid = "", string projectName = "", string className = "", string testName = "")
        {
            _guid = guid;
            _projectName = projectName;
            _className = className;
            _testName = testName;
        }

        public void BeforeTest(ITest test)
        {
            Helper.CreateDirectories();
            _start = DateTime.Now;
            Log.Write("START");
        }

        public void AfterTest(ITest test)
        {
            _finish = DateTime.Now;
            var context = TestContext.CurrentContext;
            
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
                Guid = !_guid.Equals("")
                    ? new Guid(_guid)
                    : (!TestGuid.Equals(Guid.Empty) ? TestGuid : Guid.NewGuid()),
                Screenshots = new List<NunitGoTestScreenshot>()
            };
            
            Log.Write("FINISH: " + test.FullName + ", " + _test.Guid);
            
            if(!_test.IsSuccess()) _test.TakeScreenshot();

            _test.OutputPath = Helper.Output + @"\" + "Attachments" + @"\" + _test.Guid + @"\";
            Directory.CreateDirectory(_test.OutputPath);
            var output = TestContext.Out.ToString();
            if (!output.Equals(String.Empty))
            {
                var outputPath = _test.OutputPath + Structs.Outputs.Out;
                PageGenerator.GenerateOutputPage(outputPath, output);
                _test.HasOutput = true;
            }

            _test.AddScreenshots(NunitGoTestScreenshotHelper.GetScreenshots());
            var testPath = _test.OutputPath + Structs.Outputs.Test;
            _test.GenerateTestPage(testPath);
            _test.Save(_test.OutputPath + "test.xml");
            
            var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
            PageGenerator.GenerateReport(tests, Helper.Output);

        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

    }
}
