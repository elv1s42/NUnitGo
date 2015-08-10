using System.Xml.Serialization;
using NUnit.Core;

namespace Utils.XmlTypes
{
    [XmlRoot("test")]
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
            if (test.Parent != null) Parent = new TestXml(test.Parent);
        }

        [XmlElement("class-name")]
        public string ClassName { get; set; }

        [XmlElement("method-name")]
        public string MethodName { get; set; }

        [XmlElement("test-type")]
        public string TestType { get; set; }

        [XmlElement("ignore-reason")]
        public string IgnoreReason { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("test-count")]
        public int TestCount { get; set; }

        [XmlElement("is-suite")]
        public bool IsSuite { get; set; }

        [XmlElement("parent")]
        public TestXml Parent { get; set; }

        [XmlElement("run-state")]
        public string RunState { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("full-name")]
        public string FullName { get; set; }
    }
}
