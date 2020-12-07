using System;
using System.Collections.Generic;
using AventStack.ExtentReports.MarkupUtils;
using ExampleTraining.Applications.Models;
using ExampleTraining.Applications.XYZBank;
using ExampleTraining.Reporting;
using ExampleTraining.TestSuites;
using ExampleTraining.Utilties;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ExampleTraining
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests : TestBase
    {
        
        [SetUp]
        public void TestSetup()
        {           
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-sandbox");
            Driver = new ChromeDriver(chromeOptions);
        }

        [TestCase("Hermoine Granger","1001","404")]
        [TestCase("Hermoine Granger", "1003", "404")]
        [TestCase("Harry Potter","1004","404")]
        public void PageObjectModelTest(string customer, string accountID, string amount) 
        {


            DepositModel deposit = new DepositModel() 
            {
                Username = customer,
                AccountNumber = accountID,
                DepositAmount = amount
            };

            Reporter.StepInfo(JsonConvert.SerializeObject(deposit), CodeLanguage.Json);

            XYZBankApplication bankApplication = new XYZBankApplication(Driver);
            bankApplication.loginPage.Login(deposit.Username);
            bankApplication.accountPage.DepositAndValidate(deposit);
        }

        public static List<DepositModel> deposits = DepositModel.MockData();
        [TestCaseSource(nameof(deposits))]
        public void IterativeTest(DepositModel deposit)
        {
            Reporter.StepInfo(JsonConvert.SerializeObject(deposit), CodeLanguage.Json);

            XYZBankApplication bankApplication = new XYZBankApplication(Driver);
            bankApplication.loginPage.Login(deposit.Username);
            bankApplication.accountPage.DepositAndValidate(deposit);
        }



    }
}