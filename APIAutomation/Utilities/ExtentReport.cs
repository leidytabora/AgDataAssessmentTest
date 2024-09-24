using System;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace APIAutomation.Utilities
{
    public class ExtentReport
    {
        private static ExtentReports _extentReports;
        private static readonly ThreadLocal<ExtentTest> _extentTest = new ThreadLocal<ExtentTest>();
        private static string _reportPath;

        static ExtentReport()
        {
            //Initialize report path for each thread
            _reportPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, $"ExtentReport_{Guid.NewGuid()}.html");
        }

        //Method to start reporting
        public static ExtentReports StartReporting()
        {
            if (_extentReports == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_reportPath));

                _extentReports = new ExtentReports();
                var htmlReporter = new ExtentHtmlReporter(_reportPath);

                _extentReports.AttachReporter(htmlReporter);
            }

            return _extentReports;
        }

        //Method to create a test
        public static void CreateTest(string testName)
        {
            var extentTest = StartReporting().CreateTest(testName);
            _extentTest.Value = extentTest;
        }

        //Method to log information
        public static void LogInfo(string message)
        {
            _extentTest.Value.Info(message);

        }

        //Method to log success
        public static void LogPass(string message)
        {
            _extentTest.Value.Pass(message);
        }

        //Method to log failure
        public static void LogFail(string message)
        {
            _extentTest.Value.Fail(message);
        }

        //Method to flush reports
        public static void EndReporting()
        {
            StartReporting().Flush();
        }

    }
}
