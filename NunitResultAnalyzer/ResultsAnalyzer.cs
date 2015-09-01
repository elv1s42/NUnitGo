using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NunitResultAnalyzer.XmlClasses;
using Utils;
using Utils.XmlTypes;

namespace NunitResultAnalyzer
{
    public static class ResultsAnalyzer
    {
        private static DateTime GetStartDate(TestSuite testSuite)
        {
            try
            {
                var notEmptyTestSuite = testSuite;
                while (!notEmptyTestSuite.Results.TestCases.Any())
                {
                    notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.First();
                }
                return notEmptyTestSuite.Results.TestCases.Any()
                    ? notEmptyTestSuite.Results.TestCases.First().StartDateTime : new DateTime();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                return new DateTime();
            }
        }

        private static DateTime GetFinishDate(TestSuite testSuite)
        {
            try
            {
                var notEmptyTestSuite = testSuite;
                while (!notEmptyTestSuite.Results.TestCases.Any())
                {
                    notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.Last();
                }
                return notEmptyTestSuite.Results.TestCases.Any()
                    ? notEmptyTestSuite.Results.TestCases.Last().EndDateTime : new DateTime();

            }
            catch (Exception e)
            {
                Log.Exception(e);
                return new DateTime();
            }
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
        
        private static Results AddDatesAndScreensToTestCases(Results results,
            List<Screenshot> screens, List<ExtraTestInfo> extraTestInfo)
        {
            var testSuites = results.TestSuites;
            var testCases = results.TestCases;

            foreach (var testCase in testCases)
            {
                testCase.Time = testCase.Time ?? "0.0";
                testCase.Screenshots = new List<Screenshot>();

                var extraInfo = extraTestInfo.First(x => x.FullTestName.Equals(testCase.Name));
                testCase.StartDateTime = extraInfo.StartDate;
                testCase.EndDateTime = extraInfo.FinishDate;
                testCase.Error = extraInfo.ErrorPath.Equals("") ? 
                    "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "error.txt";
                testCase.Out = extraInfo.OutPath.Equals("") ? 
                    "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "out.txt";
                testCase.Log = extraInfo.LogPath.Equals("") ? 
                    "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "log.txt";
                testCase.Trace = extraInfo.TracePath.Equals("") ? 
                    "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "trace.txt";
                testCase.Guid = extraInfo.Guid.ToString();

                var start = testCase.StartDateTime;
                var end = testCase.EndDateTime;
                foreach (var screen in screens.Where(screen => screen.Date >= start && screen.Date <= end))
                {
                    testCase.Screenshots.Add(screen);
                }
            }

            foreach (var suite in testSuites)
            {
                suite.Time = suite.Time ?? "0.0";

                var innerTestCases = suite.Results.TestCases;
                foreach (var testCase in innerTestCases)
                {
                    testCase.Time = testCase.Time ?? "0.0";
                    testCase.Screenshots = new List<Screenshot>();

                    var extraInfo = extraTestInfo.First(x => x.FullTestName.Equals(testCase.Name));
                    testCase.StartDateTime = extraInfo.StartDate;
                    testCase.EndDateTime = extraInfo.FinishDate;
                    testCase.Error = extraInfo.ErrorPath.Equals("") ? 
                        "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "error.txt";
                    testCase.Out = extraInfo.OutPath.Equals("") ? 
                        "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "out.txt";
                    testCase.Log = extraInfo.LogPath.Equals("") ? 
                        "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "log.txt";
                    testCase.Trace = extraInfo.TracePath.Equals("") ? 
                        "" : "Attachments" + @"/" + extraInfo.Guid + @"/" + "trace.txt";
                    testCase.Guid = extraInfo.Guid.ToString();

                    var start = testCase.StartDateTime;
                    var end = testCase.EndDateTime;
                    foreach (var screen in screens.Where(screen => screen.Date >= start && screen.Date <= end))
                    {
                        testCase.Screenshots.Add(screen);
                    }
                }
                if (suite.Results.TestSuites.Any())
                {
                    AddDatesAndScreensToTestCases(suite.Results, screens, extraTestInfo);
                }
            }
            return new Results{TestCases = testCases, TestSuites = testSuites};
        }

        private static Results AddDatesToTestSuites(Results results)
        {
            var testSuites = results.TestSuites;
            foreach (var suite in testSuites)
            {
                suite.StartDateTime = GetStartDate(suite);
                suite.EndDateTime = GetFinishDate(suite);

                if (suite.Results != null && suite.Results.TestSuites.Any())
                {
                    AddDatesToTestSuites(suite.Results);
                }
            }
            results.TestSuites = testSuites;
            return results;
        }

        public static TestResults GetFullSuite(TestResults results, List<ExtraTestInfo> extraTestInfos)
        {
            var testResults = results;
            var mainSuite = testResults.TestSuite;
            var suiteResults = mainSuite.Results;
            var screenshotsList = ScreenshotsHelper.GetScreenshots(Helper.Screenshots);

            if (!mainSuite.Results.TestSuites.Any()) mainSuite.Results.TestSuites = new List<TestSuite>();
            
            Log.Write("Adding dates and screenshots to test cases...");
            var resultsWithModifiedTestCases = AddDatesAndScreensToTestCases(suiteResults, screenshotsList, extraTestInfos);
            suiteResults = resultsWithModifiedTestCases;
            testResults.TestSuite.Results = suiteResults;
            Log.Write("Adding dates and screenshots to test cases: DONE.");

            Log.Write("Adding dates to test suites...");
            mainSuite = testResults.TestSuite;
            suiteResults = mainSuite.Results;
            var resultsWithModifiedTestSuites = AddDatesToTestSuites(suiteResults);
            testResults.TestSuite.Results = resultsWithModifiedTestSuites;
            mainSuite = testResults.TestSuite;
            mainSuite.StartDateTime = GetStartDate(mainSuite);
            mainSuite.EndDateTime = GetFinishDate(mainSuite);
            testResults.TestSuite = mainSuite;
            Log.Write("Adding dates to test suites: DONE.");

            return testResults;
        }
    }
}
