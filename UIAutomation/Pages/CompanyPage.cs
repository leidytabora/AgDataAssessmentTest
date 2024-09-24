using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomation.Utilities;

namespace UIAutomation.Pages
{
    public class CompanyPage
    {
        private readonly IWebDriver _driver;
        private readonly CustomElementControl _customElementControl;

        public CompanyPage(IWebDriver driver)
        {
            this._driver = driver;
            this._customElementControl = new CustomElementControl();
        }

        IReadOnlyCollection<IWebElement> allElements => _driver.FindElements(By.XPath("//*[contains(text(),'')]")); // Replace with actual selector

        IWebElement btnLetsGetStarted => _driver.FindElement(By.XPath("//a[@href='/contact']"));

        public List<string> ListAllUniqueElements()
        {
            var values = allElements.Select(element => element.Text).Distinct().ToList();

            return values;
        }

        public void ClickBtnLetsGetStarted()
        {
            _customElementControl.ClickElement(btnLetsGetStarted, "Let's Get Started");
        }

    }
}
