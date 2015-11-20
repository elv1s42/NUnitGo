using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Utils;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class 
        | AttributeTargets.Interface | AttributeTargets.Assembly, AllowMultiple = false)]
    public class NunitGoActionAttribute : NUnitAttribute, ITestAction
    {
        public NunitGoActionAttribute()
        {
            //Log.Write("NunitGoActionAttr. Constructor");
        }

        public void BeforeTest(ITest test)
        {
            LogDetails("Before", test);
        }

        public void AfterTest(ITest test)
        {
            LogDetails("After", test);
            var parent = test;
            var count = 0;
            while (parent != null)
            {
                var currentTest = parent;

                count++;

                Log.Write(String.Format(count + " Item: {0}, {1}, {2}, {3}, {4}",
                currentTest.FullName,
                currentTest.TypeInfo != null ? currentTest.TypeInfo.Name : "{no type info}",
                currentTest.IsSuite ? "Suite" : "Case",
                currentTest.Fixture != null ? currentTest.Fixture.GetType().Name : "{no fixture}",
                currentTest.Method != null ? currentTest.Method.Name : "{no method}"));

                parent = currentTest.Parent;
            }
            Log.Write("   A: " + TestContext.CurrentContext.Result.Outcome.Status);
            Log.Write("   B: " + TestContext.CurrentContext.Test.FullName);
            Log.Write("   Out: " + TestContext.Out);
            Log.Write("   C: " + TestContext.CurrentContext.TestDirectory);
            Log.Write("   C: " + TestContext.CurrentContext.Result.Outcome.Site);
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test | ActionTargets.Suite; }
        }

        private static void LogDetails(string eventMessage, ITest details)
        {
            Log.Write(String.Format("{0} {1}: {2}, from {3}.{4}.",
                eventMessage,
                details.IsSuite ? "Suite" : "Case",
                "--------",
                details.Fixture != null ? details.Fixture.GetType().Name : "{no fixture}",
                details.Method != null ? details.Method.Name : "{no method}"));
            
            /*foreach (var key in details.Properties.Keys)
            {
                Log.Write("Key: " + key + ", " + details.Properties.Get(key));
            }
            Log.Write("MESSAGE: " + TestContext.CurrentContext.Result.Message);
            Log.Write(String.Format("{0}: {1}, from {2}. {3}.",
                details.Fixture,
                details.FullName,
                details.TestCaseCount,
                details.Tests.Count));*/

        }
    }
}
