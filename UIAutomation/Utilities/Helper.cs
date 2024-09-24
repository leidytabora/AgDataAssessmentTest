using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation.Utilities
{
    public class Helper
    {
        private readonly IWebDriver driver;
     
        public Helper(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Retrieve Method Name
        public static string GetMethodName([CallerMemberName] string name = "") => name;


        public bool IsPageLoaded(string expectedPageTitle)
        {
            try
            {
                //Check if expected page title matches the current page title
                Assert.That(expectedPageTitle, Is.EqualTo(driver.Title));

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout: The expected page title was not found.");
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking the page title: {ex.Message}");
                return false;
            }
        }


    }
}
