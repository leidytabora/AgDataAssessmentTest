using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation.Utilities
{
    public class CustomElementControl
    {

        public void ClickElement(IWebElement element, string webElementName = "webElement")
        {
            try
            {
                element.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"The '{webElementName}' element was not found.");
            }
            catch (ElementClickInterceptedException)
            {
                Console.WriteLine($"The '{webElementName}' element was not clickable.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while clicking '{webElementName}': {ex.Message}");
            }

        }


    }
}
