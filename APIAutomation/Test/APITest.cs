using APIAutomation.Methods;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using APIAutomation.Utilities;
using NUnit.Framework.Interfaces;

namespace APIAutomation.Test
{

    
    [Parallelizable(ParallelScope.All)]
    public class APITest
    {

        private APIMethod _httpMethod;
        
        [SetUp]
        public void Setup()
        {
                       
            _httpMethod = new APIMethod();

        }

        
        [Test]
        [Category("APITests")]
        public async Task TC001_GetData()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //GET request to API endpoint
                var reasonPhrase = await _httpMethod.GetData(Helper.GetMethodName(), DataCategory.Posts);

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.OK.ToString()));

            }
            catch (Exception ex)
            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");

            }
        }

 
        [Test]
        [Category("APITests")]
        public async Task TC002_CreateData()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //POST request to API endpoint
                var reasonPhrase = await _httpMethod.CreateDataByTestcase(Helper.GetMethodName(), DataCategory.Posts);

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.Created.ToString()));
            }
            catch (Exception ex)

            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }

        [Test]
        [Category("APITests")]
        public async Task TC003_UpdateData()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //PUT request to API endpoint
                var reasonPhrase = await _httpMethod.UpdateDataByTestcase(Helper.GetMethodName());

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.OK.ToString()));
            }
            catch (Exception ex)
            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }


        [Test]
        [Category("APITests")]
        public async Task TC004_DeleteData()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //DELETE request to API endpoint
                var reasonPhrase = await _httpMethod.DeleteData(Helper.GetMethodName());

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.OK.ToString()));
            }
            catch (Exception ex)
            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }

        [Test]
        [Category("APITests")]
        public async Task TC005_CreateDataByID()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //POST request to API endpoint
                var reasonPhrase = await _httpMethod.CreateDataByTestcase(Helper.GetMethodName(), DataCategory.Comments);

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.Created.ToString()));
            }
            catch (Exception ex)
            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }

        [Test]
        [Category("APITests")]
        public async Task TC006_GetDataByID()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                //GET request to API endpoint
                var reasonPhrase = await _httpMethod.GetData(Helper.GetMethodName(), DataCategory.Comments);

                Assert.That(reasonPhrase, Is.EqualTo(HttpStatusCode.OK.ToString()));
            }
            catch (Exception ex)
            {
                ExtentReport.LogFail($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            EndTest();
            ExtentReport.EndReporting();
        }

        private void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Passed:
                    ExtentReport.LogPass("Test has Passed");
                    break;

                case TestStatus.Failed:
                    ExtentReport.LogFail($"Test has Failed : {message}"); 
                    break;

                case TestStatus.Skipped:
                    ExtentReport.LogInfo($"Test has Skipped : {message}");
                    break;

                default:
                    break;                   

            }

            ExtentReport.LogInfo("End of Testing");
        }


    }
}
