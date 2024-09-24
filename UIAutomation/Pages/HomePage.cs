using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomation.Utilities;

namespace UIAutomation.Pages
{
    public class HomePage
    {

        private readonly IWebDriver _driver;
        private readonly CustomElementControl _customElementControl;

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
            this._customElementControl = new CustomElementControl();
        }

        IWebElement MnuCompany => _driver.FindElement(By.Id("menu-item-992"));

        IWebElement MnuOverview => _driver.FindElement(By.Id("menu-item-829"));

        public void ClickCompany()
        {
            //MnuCompany.Click();
            _customElementControl.ClickElement(MnuCompany, "Company");
        }

        public void ClickOverview()
        {
            //MnuOverview.Click();
            _customElementControl.ClickElement(MnuOverview, "Overview");
        }


    }
}
