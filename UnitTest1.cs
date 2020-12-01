using System;
using ExampleTraining.Applications.Models;
using ExampleTraining.Applications.XYZBank;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ExampleTraining
{
    public class Tests
    {
        IWebDriver Driver {get;set;}
        [SetUp]
        public void Setup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-sandbox");
            Driver = new ChromeDriver(chromeOptions);
        }

        [Test]
        public void PageObjectModelTest() 
        {
            DepositModel deposit = new DepositModel() 
            {
                Username = "Hermoine Granger",
                AccountNumber = "1001",
                DepositAmount = "404"
            };

            XYZBankApplication bankApplication = new XYZBankApplication(Driver);
            bankApplication.loginPage.Login(deposit.Username);
            bankApplication.accountPage.DepositAndValidate(deposit);
        }
       

        [TearDown]
        public void CleanUp()
        {          
            Driver.Dispose();
        }
    }
}