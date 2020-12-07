using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace ExampleTraining.Reporting
{
    public class Reporter
    {
        private static AventStack.ExtentReports.ExtentReports extentReports { get; set; }
        [ThreadStatic] public static ExtentTest CurrentTest;
        public static string ReportDir { get; set; }

        public static void Setup() 
        {
            if (extentReports == null) 
            {
                extentReports = new AventStack.ExtentReports.ExtentReports();
                string dir = Environment.CurrentDirectory;
                ReportDir = Directory.GetParent(dir).Parent.Parent.FullName;
                ReportDir += "\\Reports\\"+DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")+"\\";

                Directory.CreateDirectory(ReportDir);
                var html = new ExtentHtmlReporter(ReportDir);
                extentReports.AttachReporter(html);

                Console.WriteLine("Report Created at: " + ReportDir + "index.html - the Report will only generate after tests are added.");
            }
        }

        public static void CreateTest() 
        {
            string testName = TestContext.CurrentContext.Test.MethodName;
            CurrentTest = extentReports.CreateTest(testName);            
        }

        public static void FlushTest() 
        {
            extentReports.Flush();
        }

        //Pass
        public static void StepPassed(string msg, Screenshot screenshot = null) 
        {
            if (screenshot == null)
            {
                CurrentTest.Pass(msg);
            }
            else
            {
                var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(SaveScreenshot(screenshot)).Build();
                CurrentTest.Pass(msg, mediaModel);
            }
        }

        //Info
        public static void StepInfo(string msg)
        {
            CurrentTest.Info(msg);
        }

        public static void StepInfo(string msg, CodeLanguage formatType)
        {
            var formattedMessage = MarkupHelper.CreateCodeBlock(msg, formatType);
            CurrentTest.Info(formattedMessage);
        }

        //Warning
        public static void Warning(string msg)
        {
            CurrentTest.Warning(msg);
        }

        //Failure
        public static void TestFailed(string msg, Screenshot screenshot = null)
        {
            if (screenshot == null)
            {
                CurrentTest.Fail(msg);
            }
            else 
            {
                var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(SaveScreenshot(screenshot)).Build();
                CurrentTest.Fail(msg, mediaModel);
            }
            
        }

        public static string SaveScreenshot(Screenshot screenshot) 
        {
            string screenshotName = "\\" + DateTime.Now.ToString("ddMMyyyyHHmmssfff")+".png";
            string screenshotFolder = "Screenshots\\"+TestContext.CurrentContext.Test.MethodName;
            string screenshotPath = ReportDir+screenshotFolder;
            Directory.CreateDirectory(screenshotPath);
            screenshot.SaveAsFile(screenshotPath +"\\"+ screenshotName);
            return (".\\"+screenshotFolder + screenshotName);

        }


    }
}