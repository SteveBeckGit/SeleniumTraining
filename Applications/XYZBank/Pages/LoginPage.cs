using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Applications.XYZBank.Pages
{
    public class LoginPage
    {
        By welcomeMessage(string username) => By.XPath("//strong[contains(text(),'Welcome')]/span[text()='" + username + "']");
        By customerLoginBtn = By.XPath("//button[text()='Customer Login']");
        By customerSelect = By.Id("userSelect");
        By LoginBtn = By.XPath("//button[text()='Login']");

        private IWebDriver Driver { get; set; }

        public LoginPage(IWebDriver _driver) 
        {
            Driver = _driver;
        }

        public void Login(string username) 
        {
            Driver.Url = "http://www.way2automation.com/angularjs-protractor/banking/#/login";

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement loginBtn = wait.Until(drv => drv.FindElement(customerLoginBtn));

            loginBtn.Click();

            var ele = wait.Until(drv => drv.FindElement(customerSelect));
            SelectElement selectElement = new SelectElement(ele);
            selectElement.SelectByText(username);

            IWebElement realLoginBtn = wait.Until(drv => drv.FindElement(LoginBtn));
            realLoginBtn.Click();

            Assert.True(wait.Until(drv => drv.FindElement(welcomeMessage(username)).Displayed), "Dashboard not loaded");

        }
    }
}
