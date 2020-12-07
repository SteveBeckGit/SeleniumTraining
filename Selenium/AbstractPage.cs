using System;
using ExampleTraining.Reporting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExampleTraining.Selenium
{
    public abstract class AbstractPage : ReportHandler
    {
        public AbstractPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement FindOne(By by, int timeout = 5) 
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(drv => drv.FindElement(by));
        }

        public string GetText(By by, int timeout = 5) 
        {
            return FindOne(by, timeout).Text;
        }

        public void Click(By by, int timeout = 5) 
        {
            FindOne(by, timeout).Click();
        }

        public void SendKeys(By by, string text, int timeout = 5)
        {
            FindOne(by, timeout).SendKeys(text);
        }

        public void SelectFromDropDown(By by, string text, int timeout = 5)
        {
            var select = new SelectElement(FindOne(by, timeout));
            select.SelectByText(text);
        }

        public bool ValidateElementDisplayed(By by, int timeout = 5)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(drv => drv.FindElement(by).Displayed);
        }
    }
}