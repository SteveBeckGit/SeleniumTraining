using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Reporting
{
    public abstract class ReportHandler
    {
        public  IWebDriver Driver;

        public Screenshot TakeScreenshot() 
        {
           return ((ITakesScreenshot)Driver).GetScreenshot();
        }

        public void StepPassedWithScreenshot(string msg) 
        {
            Reporter.StepPassed(msg,TakeScreenshot());
        }

        public void TestFailed(string msg) 
        {
            Reporter.TestFailed(msg, TakeScreenshot());

        }
    }
}
