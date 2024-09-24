using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace UIAutomation
{
    public class DriverManager
    {

        private IWebDriver _driver;

        //IWebDriver property
        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("WebDriver not initialized. Call InitializeDriver first.");
                }
                return _driver;
            }
        }

        //Initialize WebDriver based on the browser type
        public void InitializeDriver(string browserType)
        {
            try
            {
                switch (browserType.ToLower())
                {
                    case "chrome":
                        _driver = new ChromeDriver();
                        break;
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    case "edge":
                        _driver = new EdgeDriver();
                        break;
                    default:
                        throw new ArgumentException("Unsupported browser type: " + browserType);
                }

                _driver.Manage().Window.Maximize();
            }
            catch (WebDriverException ex)
            {
                // Log error details
                Console.WriteLine($"WebDriver error: {ex.Message}");
                throw; // Rethrow to fail the test
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Rethrow to fail the test
            }
        }

        //Navigates to the specified URL
        public void NavigateTo(string url)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("WebDriver not initialized. Call InitializeDriver first.");
            }
            _driver.Navigate().GoToUrl(url);
        }


        //Closes and quits the driver
        public void QuitDriver()
        {
            _driver.Quit();
            _driver = null; //Reset the driver instance
        }

    }

}

