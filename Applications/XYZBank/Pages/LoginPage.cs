using ExampleTraining.Reporting;
using ExampleTraining.Selenium;
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
    public class LoginPage : AbstractPage
    {
        By welcomeMessage(string username) => By.XPath("//strong[contains(text(),'Welcome')]/span[text()='" + username + "']");
        By customerLoginBtn = By.XPath("//button[text()='Customer Login']");
        By customerSelect = By.Id("userSelect");
        By LoginBtn = By.XPath("//button[text()='Login']");


        public LoginPage(IWebDriver _driver) : base(_driver)
        {
            Driver = _driver;
        }

        public void Login(string username) 
        {
            Driver.Url = "http://www.way2automation.com/angularjs-protractor/banking/#/login";

            Click(customerLoginBtn);          
            SelectFromDropDown(customerSelect, username);
            Click(LoginBtn);


            Assert.True(ValidateElementDisplayed(welcomeMessage(username)), "Dashboard not loaded");
            StepPassedWithScreenshot("Login Success!");

        }
    }
}
