using ExampleTraining.Applications.XYZBank.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Applications.XYZBank
{
    public class XYZBankApplication
    {
        public LoginPage loginPage{get;set;}
        public AccountPage accountPage { get; set; }
        public XYZBankApplication(IWebDriver driver) 
        {
            loginPage = new LoginPage(driver);
            accountPage = new AccountPage(driver);
        }
    }
}
