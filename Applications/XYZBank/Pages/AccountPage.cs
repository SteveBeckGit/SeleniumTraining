﻿using ExampleTraining.Applications.Models;
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
    public class AccountPage
    {
        IWebDriver Driver { get; set; }
        By AccountSelect = By.XPath("//select[@id='accountSelect']");
        By Balance = By.XPath("//div[contains(text(),'Account Number')]/strong[2]");
        By OpenDeposits = By.XPath("//button[contains(text(),'Deposit')][not(@type='submit')]");
        By Amount = By.XPath("//label[text()='Amount to be Deposited :']/following-sibling::input");
        By SubmitDeposit = By.XPath("//button[contains(text(),'Deposit')][@type='submit']");

        public AccountPage(IWebDriver webDriver) 
        {
            Driver = webDriver;
        }

        public void DepositAndValidate(DepositModel depositModel) 
        {
            SelectFromDropDown(AccountSelect, depositModel.AccountNumber);
            string startingAmount = GetText(Balance);
            Click(OpenDeposits);
            SendKeys(Amount, depositModel.DepositAmount, 15);
            Click(SubmitDeposit);
            string endingAmount = GetText(Balance);

            int startBalance = Convert.ToInt32(startingAmount);
            int endBalance = Convert.ToInt32(endingAmount);
            int validationAmount = Convert.ToInt32(depositModel.DepositAmount);
            int difference = endBalance - startBalance;

            Assert.That(difference.Equals(validationAmount), "Deposit Validation Failure, Expected: " + validationAmount + ", Found: " + difference);

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
    }
}
