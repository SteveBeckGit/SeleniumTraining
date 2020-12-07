using ExampleTraining.Reporting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.TestSuites
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            Reporter.Setup();
        }

        [SetUp]
        public void Setup()
        {
            Reporter.CreateTest();           
        }

        [TearDown]
        public void CleanUp()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Reporter.TestFailed(TestContext.CurrentContext.Result.Message);
            }
            else
            {
                Reporter.StepPassed("Test Complete");
            }
            Reporter.FlushTest();
        }
    }
}
