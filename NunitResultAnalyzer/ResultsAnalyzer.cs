using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NunitResultAnalyzer.XmlClasses;
using ScreenshotsAnalyzer;
using Utils;
using Utils.XmlTypes;

namespace NunitResultAnalyzer
{
    public static class ResultsAnalyzer
    {
        private static DateTime GetStartDate(TestSuite testSuite)
        {
            var notEmptyTestSuite = testSuite;
            while (!notEmptyTestSuite.Results.TestCases.Any())
            {
                notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.First();
            }
            return notEmptyTestSuite.Results.TestCases.First().StartDateTime;
        }

        private static DateTime GetFinishDate(TestSuite testSuite)
        {
            var notEmptyTestSuite = testSuite;
            while (!notEmptyTestSuite.Results.TestCases.Any())
            {
                notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.Last();
            }
            return notEmptyTestSuite.Results.TestCases.Last().EndDateTime;
        }

        private static string ReadFromFile(string path)
        {
            var res = "";
            try
            {
                using (var streamReader = new StreamReader(path, Encoding.UTF8))
                {
                    res = streamReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
            }
            return res;
        }
        
        private static List<TestSuite> AddDatesAndScreensToTestCases(List<TestSuite> testSuites,
            Dictionary<string, DateTime> screensDict, List<ExtraTestInfo> extraTestInfo)
        {
            foreach (var suite in testSuites)
            {
                suite.Time = suite.Time ?? "0.0";

                var testCases = suite.Results.TestCases;
                foreach (var testCase in testCases)
                {
                    testCase.Time = testCase.Time ?? "0.0";
                    testCase.Screenshots = new Dictionary<string, DateTime>();

                    var extraInfo = extraTestInfo.First(x => x.FullTestName.Equals(testCase.Name));
                    testCase.StartDateTime = extraInfo.StartDate;
                    testCase.EndDateTime = extraInfo.FinishDate;
                    testCase.Error = ReadFromFile(extraInfo.Error);
                    testCase.Out = ReadFromFile(extraInfo.Out);
                    testCase.Log = ReadFromFile(extraInfo.Log);
                    testCase.Trace = ReadFromFile(extraInfo.Trace);
                    testCase.Guid = extraInfo.Guid.ToString();

                    var start = testCase.StartDateTime;
                    var end = testCase.EndDateTime;
                    foreach (var screen in screensDict.Where(screen => screen.Value >= start && screen.Value <= end))
                    {
                        testCase.Screenshots.Add(screen.Key, screen.Value);
                    }
                }
                
                if (suite.Results.TestSuites.Any())
                {
                    AddDatesAndScreensToTestCases(suite.Results.TestSuites, screensDict, extraTestInfo);
                }
            }
            return testSuites;
        }

        private static List<TestSuite> AddDatesToTestSuites(List<TestSuite> testSuites)
        {
            foreach (var suite in testSuites)
            {
                suite.StartDateTime = GetStartDate(suite);
                suite.EndDateTime = GetFinishDate(suite);

                if (suite.Results.TestSuites.Any())
                {
                    AddDatesToTestSuites(suite.Results.TestSuites);
                }
            }
            return testSuites;
        }

        public static TestResults GetFullSuite(TestResults testResults, List<ExtraTestInfo> extraTestInfos)
        {
            if (testResults == null)
            {
                Log.Write("Empty TestResults in GetFullSuite!");
                testResults = new TestResults();
            }
            var mainSuite = testResults.TestSuite;
            if (mainSuite == null)
            {
                Log.Write("Empty TestSuite in TestResults in GetFullSuite!");
                mainSuite = new TestSuite();
            }
            if (mainSuite.Results == null)
            {
                Log.Write("Empty Results in TestSuite in GetFullSuite!");
                mainSuite.Results = new Results();
            }
            var screenshotsDictionary = ScreenshotsHelper.GetScreenshots(Locator.Screenshots);

            if (!mainSuite.Results.TestSuites.Any()) mainSuite.Results.TestSuites = new List<TestSuite>();

            var suites = mainSuite.Results.TestSuites;

            suites = AddDatesAndScreensToTestCases(suites, screenshotsDictionary, extraTestInfos);
            suites = AddDatesToTestSuites(suites);

            testResults.TestSuite.Results.TestSuites = suites;

            mainSuite = testResults.TestSuite;

            mainSuite.StartDateTime = GetStartDate(mainSuite);
            mainSuite.EndDateTime = GetFinishDate(mainSuite);

            testResults.TestSuite = mainSuite;
            
            return testResults;
        }

        public static TestResults GetFullSuite(TestResultXml resultsXml, List<ExtraTestInfo> extraTestInfos)
        {
            var results = new TestResults(resultsXml);
            var mainSuite = results.TestSuite;
            if (mainSuite == null)
            {
                Log.Write("Empty TestSuite in TestResults in GetFullSuite!");
                mainSuite = new TestSuite();
            }
            if (mainSuite.Results == null)
            {
                Log.Write("Empty Results in TestSuite in GetFullSuite!");
                mainSuite.Results = new Results();
            }
            var screenshotsDictionary = ScreenshotsHelper.GetScreenshots(Locator.Screenshots);

            if (!mainSuite.Results.TestSuites.Any()) mainSuite.Results.TestSuites = new List<TestSuite>();

            var suites = mainSuite.Results.TestSuites;

            suites = AddDatesAndScreensToTestCases(suites, screenshotsDictionary, extraTestInfos);
            suites = AddDatesToTestSuites(suites);

            results.TestSuite.Results.TestSuites = suites;

            mainSuite = results.TestSuite;

            mainSuite.StartDateTime = GetStartDate(mainSuite);
            mainSuite.EndDateTime = GetFinishDate(mainSuite);

            results.TestSuite = mainSuite;

            return results;
        }
    }
}
