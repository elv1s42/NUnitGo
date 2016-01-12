using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NunitGo.HtmlCustomElements;
using NunitGo.Utils;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method/* | AttributeTargets.Class 
        | AttributeTargets.Interface | AttributeTargets.Assembly*/, AllowMultiple = false)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        private readonly string _guid;
        private readonly string _projectName;
        private readonly string _className;
        private NunitGoTest _test;
        private DateTime _start;
        private DateTime _finish;
        public static Guid TestGuid = Guid.Empty;
        
        public NunitGoActionAttribute(string guid = "", string projectName = "", string className = "")
        {
            _guid = guid;
            _projectName = projectName;
            _className = className;
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
                Name = test.Name,
                FailureStackTrace = context.Result.StackTrace ?? "",
                FailureMessage = context.Result.Message ?? "",
                Result = context.Result.Outcome != null ? context.Result.Outcome.ToString() : "Unknown",
                Guid = !_guid.Equals("")
                    ? new Guid(_guid)
                    : (!TestGuid.Equals(Guid.Empty) ? TestGuid : Guid.NewGuid())
            };
            
            Log.Write("FINISH: " + test.FullName + ", " + _test.Guid);
            
            if(!_test.IsSuccess()) Helper.TakeScreenshot(DateTime.Now);

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
            _test.Save(_test.OutputPath + "test.xml");
            var tests = NunitGoTestHelper.GetTests().OrderBy(x => x.DateTimeFinish).ToList();
            PageGenerator.GenerateReport(tests, Helper.Output);

        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }// | ActionTargets.Suite; }
        }

    }
}
