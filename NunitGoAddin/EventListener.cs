using System;
using NUnit.Core;

namespace NunitGoAddin
{
    public class EventListener : NUnit.Core.EventListener
    {
        public virtual void RunStarted(string name, int testCount)
        {
        }

        public virtual void RunFinished(TestResult result)
        {
        }

        public virtual void RunFinished(Exception exception)
        {
        }

        public virtual void TestStarted(TestName testName)
        {
        }

        public virtual void TestFinished(TestResult result)
        {
        }

        public virtual void SuiteStarted(TestName testName)
        {
        }

        public virtual void SuiteFinished(TestResult result)
        {
        }

        public virtual void UnhandledException(Exception exception)
        {
        }

        public virtual void TestOutput(TestOutput testOutput)
        {
        }
    }
}
