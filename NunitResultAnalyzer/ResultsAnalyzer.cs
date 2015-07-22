using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NunitResultAnalyzer.XmlClasses;
using ScreenshotsAnalyzer;

namespace NunitResultAnalyzer
{
    public class ResultsAnalyzer
    {
        public ResultsAnalyzer(TestResults testResults, string screenshotsPath)
        {
            _screenshotsPath = screenshotsPath;
            _testResults = testResults;
            _startDate = DateTime.ParseExact(testResults.Date + " " + testResults.Time,
                "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                .AddSeconds(-Double.Parse(testResults.TestSuite.Time,
                        CultureInfo.InvariantCulture));
            _currentTestTemplateDate = _startDate;
        }

        private readonly string _screenshotsPath;
        private readonly TestResults _testResults;
        private readonly DateTime _startDate;
        private DateTime _currentTestTemplateDate;

        private List<TestSuite> AddDatesAndScreensToTestSuites(List<TestSuite> testSuites,
            Dictionary<string, DateTime> screensDict)
        {
            foreach (var suite in testSuites)
            {
                suite.StartDateTime = _currentTestTemplateDate;
                suite.Time = suite.Time ?? "0.0";
                _currentTestTemplateDate = _currentTestTemplateDate.AddSeconds(Double.Parse(suite.Time,
                        CultureInfo.InvariantCulture));
                suite.EndDateTime = _currentTestTemplateDate;

                var testCaseTemplateDate = suite.StartDateTime;
                var testCases = suite.Results.TestCases;
                foreach (var testCase in testCases)
                {
                    testCase.StartDateTime = testCaseTemplateDate;
                    testCase.Time = testCase.Time ?? "0.0";
                    testCaseTemplateDate = testCaseTemplateDate.AddSeconds(Double.Parse(testCase.Time,
                            CultureInfo.InvariantCulture));
                    testCase.EndDateTime = testCaseTemplateDate;
                    testCase.Screenshots = new Dictionary<string, DateTime>();

                    var start = testCase.StartDateTime;
                    var end = testCase.EndDateTime;
                    foreach (var screen in screensDict.Where(screen => screen.Value >= start && screen.Value <= end))
                    {
                        testCase.Screenshots.Add(screen.Key, screen.Value);
                    }
                }

                if (suite.Results.TestSuites.Any())
                {
                    _currentTestTemplateDate = suite.StartDateTime;
                    AddDatesAndScreensToTestSuites(suite.Results.TestSuites, screensDict);
                }
            }
            return testSuites;
        }
        
        private TestSuite AddDatesToSuites(TestSuite testSuite)
        {
            testSuite.StartDateTime = _currentTestTemplateDate;
            testSuite.Time = testSuite.Time ?? "0.0";
            testSuite.EndDateTime = _currentTestTemplateDate.AddSeconds(Double.Parse(testSuite.Time,
                    CultureInfo.InvariantCulture));

            var testSuites = testSuite.Results.TestSuites;
            foreach (var suite in testSuites)
            {
                suite.StartDateTime = new[]{_currentTestTemplateDate, testSuite.EndDateTime}.Min();
                suite.Time = suite.Time ?? "0.0";
                _currentTestTemplateDate = _currentTestTemplateDate.AddSeconds(Double.Parse(suite.Time,
                        CultureInfo.InvariantCulture));
                suite.EndDateTime = _currentTestTemplateDate;

                if (suite.Results.TestSuites.Any())
                {
                    _currentTestTemplateDate = suite.StartDateTime;
                    AddDatesToSuites(suite);
                }
            }
            return testSuite;
        }
        
        public TestResults GetFullSuite()
        {
            if (_testResults == null) throw new Exception("Empty TestResults!");
            var mainSuite = _testResults.TestSuite;
            if (mainSuite == null) throw new Exception("Empty TestSuite in TestResults!");
            if (mainSuite.Results == null) throw new Exception("Empty Results in TestSuite!");

            var screenshotsDictionary = ScreenshotsHelper.GetScreensots(_screenshotsPath);

            if (!mainSuite.Results.TestSuites.Any()) return _testResults;

            mainSuite.StartDateTime = _startDate;
            var suites = mainSuite.Results.TestSuites;
            
            //suites = AddDatesToSuites(suites);//, screenshotsDictionary);
            mainSuite = AddDatesToSuites(mainSuite);

            _testResults.TestSuite = mainSuite;
            //_testResults.TestSuite.Results.TestSuites = suites;
            
            return _testResults;
        }
    }
}
