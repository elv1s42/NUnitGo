using System;
using System.IO;
using System.Xml;
using NUnit.Engine.Extensibility;
using NUnit.Framework.Interfaces;
using Utils;

namespace NunitGo.Sandbox
{
    /*[Extension(Description = "Ololo", Path = @"C:\NUnit3\nunit-console\addins\NunitGo.dll")]
    public class ExtensionPointTest1 : NUnit.Framework.Interfaces.
    {
        public void TestStarted(ITest test)
        {
            Console.WriteLine("STARTED!!!!!!!!!!");
        }

        public void TestFinished(ITestResult result)
        {
            Console.WriteLine("FINISHED!!!!!!!!!!!");
        }
    }*/

    //[Extension(Description = "Ololo", Path = @"C:\NUnit3\nunit-console\addins\NunitGo.dll")]
    [Extension(Description = "Ololo")]
    public class ExtensionPointTest : IResultWriter
    {
        //NUnit.Engine.Extensibility.I

        public void CheckWritability(string outputPath)
        {
            Console.WriteLine("CheckWritability!!!!!!!!!!!");
            Log.Write("OLOLO1");
        }

        public void WriteResultFile(XmlNode resultNode, string outputPath)
        {
            Console.WriteLine("WriteResultFile!!!!!!!!!!!");
            Log.Write("OLOLO2");
        }

        public void WriteResultFile(XmlNode resultNode, TextWriter writer)
        {
            Console.WriteLine("WriteResultFile!!!!!!!!!!!");
            Log.Write("OLOLO3");
        }
    }
}
