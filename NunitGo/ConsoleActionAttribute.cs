using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NunitGo
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class /*|
            AttributeTargets.Interface | AttributeTargets.Assembly*/,
            AllowMultiple = false)]
    public class ConsoleActionAttribute : NUnitAttribute, ITestAction
    {
        private readonly string _message;

        public ConsoleActionAttribute(string message)
        {
            Console.WriteLine("ConsoleActionAttr. Constructor");
            _message = message;
        }

        public void BeforeTest(ITest test)
        {
            Console.WriteLine("Before test");
            WriteToConsole("Before", test);
        }

        public void AfterTest(ITest test)
        {
            Console.WriteLine("After test");
            WriteToConsole("After", test);
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test | ActionTargets.Suite; }
        }

        private void WriteToConsole(string eventMessage, ITest details)
        {
            Console.WriteLine("{0} {1}: {2}, from {3}.{4}.",
                eventMessage,
                details.IsSuite ? "Suite" : "Case",
                _message,
                details.Fixture != null ? details.Fixture.GetType().Name : "{no fixture}",
                //details.FixtureType != null ? details.FixtureType.Name : "{no fixture}",
                details.Method != null ? details.Method.Name : "{no method}");
        }
    }
}
