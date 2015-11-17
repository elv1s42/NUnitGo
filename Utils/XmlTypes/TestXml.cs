using NUnit.Core;

namespace Utils.XmlTypes
{
    public class TestXml
    {
        public TestXml()
        {
        }

        public TestXml(ITest test)
        {
            ClassName = test.ClassName;
            MethodName = test.MethodName;
            TestCount = test.TestCount;
            TestType = test.TestType;
            IgnoreReason = test.IgnoreReason;
            Description = test.Description;
            IsSuite = test.IsSuite;
            RunState = test.RunState.ToString();
            Name = test.TestName.Name;
            FullName = test.TestName.FullName;
            UniqueName = test.TestName.UniqueName;
            if (test.Parent != null) Parent = new TestXml(test.Parent);
        }

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string TestType { get; set; }
        public string IgnoreReason { get; set; }
        public string Description { get; set; }
        public int TestCount { get; set; }
        public bool IsSuite { get; set; }
        public TestXml Parent { get; set; }
        public string RunState { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string UniqueName { get; set; }
    }
}
