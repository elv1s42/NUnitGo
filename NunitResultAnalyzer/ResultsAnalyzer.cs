using System;
using System.Collections.Generic;
using System.Linq;
using NunitGoAddin;
using NunitResultAnalyzer.XmlClasses;
using ScreenshotsAnalyzer;
using Utils;

namespace NunitResultAnalyzer
{
    public class ResultsAnalyzer
    {
        public ResultsAnalyzer(TestResults testResults, List<ExtraTestInfo> extraTestInfos)
        {
            _testResults = testResults;
            _extraTestInfos = extraTestInfos;
        }

        private readonly TestResults _testResults;
        private readonly List<ExtraTestInfo> _extraTestInfos;

        private DateTime GetStartDate(TestSuite testSuite)
        {
            var notEmptyTestSuite = testSuite;
            while (!notEmptyTestSuite.Results.TestCases.Any())
            {
                notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.First();
            }
            return notEmptyTestSuite.Results.TestCases.First().StartDateTime;
        }

        private DateTime GetFinishDate(TestSuite testSuite)
        {
            var notEmptyTestSuite = testSuite;
            while (!notEmptyTestSuite.Results.TestCases.Any())
            {
                notEmptyTestSuite = notEmptyTestSuite.Results.TestSuites.Last();
            }
            return notEmptyTestSuite.Results.TestCases.Last().EndDateTime;
        }

        private List<TestSuite> AddDatesAndScreensToTestSuites(List<TestSuite> testSuites,
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

                    var start = testCase.StartDateTime;
                    var end = testCase.EndDateTime;
                    foreach (var screen in screensDict.Where(screen => screen.Value >= start && screen.Value <= end))
                    {
                        testCase.Screenshots.Add(screen.Key, screen.Value);
                    }
                }
                
                if (suite.Results.TestSuites.Any())
                {
                    AddDatesAndScreensToTestSuites(suite.Results.TestSuites, screensDict, extraTestInfo);
                }
            }
            return testSuites;
        }

        private List<TestSuite> AddDatesToTestSuites(List<TestSuite> testSuites)
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
        
        public TestResults GetFullSuite()
        {
            if (_testResults == null) throw new Exception("Empty TestResults!");
            var mainSuite = _testResults.TestSuite;
            if (mainSuite == null) throw new Exception("Empty TestSuite in TestResults!");
            if (mainSuite.Results == null) throw new Exception("Empty Results in TestSuite!");

            var screenshotsDictionary = ScreenshotsHelper.GetScreenshots(Locator.Screenshots);

            if (!mainSuite.Results.TestSuites.Any()) return _testResults;

            var suites = mainSuite.Results.TestSuites;

            suites = AddDatesAndScreensToTestSuites(suites, screenshotsDictionary, _extraTestInfos);
            suites = AddDatesToTestSuites(suites);

            _testResults.TestSuite.Results.TestSuites = suites;

            mainSuite = _testResults.TestSuite;

            mainSuite.StartDateTime = GetStartDate(mainSuite);
            mainSuite.EndDateTime = GetFinishDate(mainSuite);

            _testResults.TestSuite = mainSuite;
            
            return _testResults;
        }
    }
}
