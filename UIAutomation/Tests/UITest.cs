using OpenQA.Selenium;
using System;
using System.Threading.Tasks;
using UIAutomation.Pages;
using UIAutomation.Utilities;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UIAutomation.Config;

namespace UIAutomation.Tests
{

    [Parallelizable(ParallelScope.All)]
    public class UITest
    {
        private DriverManager _driverManager;
        private static readonly AppConfig appConfig = new AppConfig();


        [SetUp]
        public void SetUp()
        {

            _driverManager = new DriverManager();
            _driverManager.InitializeDriver(appConfig.Browser);
        }


        [Test]
        [Category("UITests")]
        public void TC001_ValidateContactPage()
        {
            try
            {
                ExtentReport.CreateTest(Helper.GetMethodName());
                ExtentReport.LogInfo("Start of Test");

                ExtentReport.LogInfo($"Browser Type: {appConfig.Browser}");

                IWebDriver webDriver = _driverManager.Driver;

                //1. Open a browser and navigate to "www.agdata.com"
                ExtentReport.LogInfo("1. Open a browser and navigate to \"www.agdata.com\"");
                _driverManager.NavigateTo(appConfig.BaseURL);

                HomePage homePage = new HomePage(webDriver);
                CompanyPage companyPage = new CompanyPage(webDriver);

                //2. On the top navigation menu click on "Company" > "Overview"
                ExtentReport.LogInfo("2. On the top navigation menu click on Company > Overview");
                homePage.ClickCompany();
                homePage.ClickOverview();

                //3. On the "https://www.agdata.com/company/" page, get back all the Values on the page in a LIST
                ExtentReport.LogInfo("3. On the \"https://www.agdata.com/company/\" page, get back all the Values on the page in a LIST");
                List<string> values = companyPage.ListAllUniqueElements();

                for (int i = 0; i < values.Count; i++)
                {
                    Console.WriteLine($"Text #{i + 1} : {values[i]}");
                    ExtentReport.LogInfo($"Text #{i + 1} : {values[i]}");
                }


                //4. Click on the "Let's Get Started" button at the bottom
                ExtentReport.LogInfo("4. Click on the \"Let's Get Started\" button at the bottom");
                companyPage.ClickBtnLetsGetStarted();

                //5. Validate that the 'Contact' page is displayed / loaded
                ExtentReport.LogInfo("5. Validate that the 'Contact' page is displayed / loaded");
                Helper helper = new Helper(webDriver);

                string expectedPageTitle = "Contact - AGDATA";
                helper.IsPageLoaded(expectedPageTitle);

                ExtentReport.LogInfo("'Contact' page is loaded");
                Console.WriteLine("'Contact' page is loaded");
            }
            catch (Exception ex)
            {
                ExtentReport.LogFail(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }


        [TearDown]
        public void TearDown()
        {
            EndTest();
            ExtentReport.EndReporting();
            _driverManager.QuitDriver();
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
