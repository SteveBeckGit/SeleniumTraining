using ExampleTraining.Databases;
using ExampleTraining.Reporting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.TestSuites
{
    public class TestBase
    {
        [ThreadStatic] public static IWebDriver Driver;
        [ThreadStatic] private Stopwatch sw;

        [OneTimeSetUp]
        public void Init()
        {
            Reporter.Setup();
        }

        [SetUp]
        public void Setup()
        {
            sw = new Stopwatch();
            sw.Start();
            Reporter.CreateTest();           
        }

        [TearDown]
        public void CleanUp()
        {
            sw.Stop();
            /*MSQLController controller = new MSQLController("connString");
            string name = TestContext.CurrentContext.Test.Name;


            string sql = "insert into {table} (TestName, Status, Message, Duration) values ('"+name+"',"+
                "'"+ TestContext.CurrentContext.Result.Outcome + "','"+ TestContext.CurrentContext.Result.Message + "',"+
                "'"+sw.Elapsed.TotalSeconds+"');";
            controller.ExecuteStatement(sql); */

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                if (Driver != null)
                {
                    Reporter.TestFailed(TestContext.CurrentContext.Result.Message,
                    ((ITakesScreenshot)Driver).GetScreenshot());
                }
                else 
                {
                    Reporter.TestFailed(TestContext.CurrentContext.Result.Message);
                }
                
            }
            else
            {
                Reporter.StepPassed("Test Complete");
            }
            Reporter.FlushTest();
            Driver.Dispose();
        }
    }
}
