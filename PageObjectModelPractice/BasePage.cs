using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium_BootCamp_POM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageObjectModelPractice
{
    internal class BasePage 
    {
        public static IWebDriver driver;
        public TestContext testContext;
        public static void SeleniumInit()
        {
           driver = new ChromeDriver();
        }
        public static void SeleniumClose()
        {
            driver.Close();
        }

        public static void CloseDriver()
        {
            driver.Close();
        }

        public void AssertElement(By by, string expectedText)
        {
            try
            {
                string actualtext = GetElementText(by);
                Assert.AreEqual(expectedText, actualtext);
            }
            catch (Exception ex)
            {
                TakeScreenshot(Status.Fail, "Assert Failed: " + ex.ToString());
                Assert.Fail(ex.ToString());
            }
        }

        public void Write(By by, string value)
        {
            try
            {
                WaitforElement(by).SendKeys(value);
                TakeScreenshot(Status.Pass, "Enter Text");
            }
            catch (Exception ex)
            {

                TakeScreenshot(Status.Fail, "Enter Text: " + ex.ToString());
                Assert.Fail(ex.ToString());
            }
        }

        public void Click(By by)
        {
            try
            {
                WaitforElement(by).Click();
                TakeScreenshot(Status.Pass, "Click Element");
            }
            catch (Exception ex)
            {
                TakeScreenshot(Status.Fail, "Click Element: " + ex.ToString());
                Assert.Fail(ex.ToString());
            }
        }

        public void Clear(By by)
        {
            driver.FindElement(by).Clear();
            TakeScreenshot(Status.Pass, "Clear Element");
        }

        public static void OpenUrl(string url)
        {
            driver.Url = url;
            TakeScreenshot(Status.Pass, "Open Url");
        }

        public string GetElementText(By by)
        {
            string text;
            try
            {
                text = driver.FindElement(by).Text;
            }
            catch
            {
                try
                {
                    text = driver.FindElement(by).GetAttribute("value");
                }
                catch
                {
                    text = driver.FindElement(by).GetAttribute("innerHTML");
                }
            }
            return text;
        }

        public string GetElementState(By by)
        {
            string elementState = driver.FindElement(by).GetAttribute("Disabled");

            if (elementState == null)
            {
                elementState = "enabled";
            }
            else if (elementState == "true")
            {
                elementState = "disabled";
            }
            return elementState;
        }

        public static void PlaybackWait(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
        }

        public static string ExecuteJavaScriptCode(string javascriptCode)
        {
            string value = null;
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                value = (string)js.ExecuteScript(javascriptCode);
            }
            catch (Exception)
            {

            }
            return value;
        }

        //public static void TakeScreenshot(string stepDetail)
        //{
        //    string path = @"C:\TestExecutionReports\" + "TestExecLog_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //    Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
        //    image.SaveAsFile(path + ".png", ScreenshotImageFormat.Png);
        //    ExtentReport.exChildTest.Log(Status.Pass, stepDetail, MediaEntityBuilder.CreateScreenCaptureFromPath(path + ".png").Build());
        //}

        public static void TakeScreenshot(Status status, string stepDetail)
        {
            string path = @"C:\TestExecutionReports\" + "TestExecLog_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Screenshot image_username = ((ITakesScreenshot)driver).GetScreenshot();
            image_username.SaveAsFile(path + ".png", ScreenshotImageFormat.Png);
            ExtentReport.exChildTest.Log(status, stepDetail, MediaEntityBuilder
                .CreateScreenCaptureFromPath(path + ".png").Build());
        }



        #region Private Methods       
        private bool IsElementTextField(By by)
        {
            try
            {
                bool resultText = Convert.ToBoolean(driver.FindElement(by).GetAttribute("type").Equals("text"));
                bool resultPass = Convert.ToBoolean(driver.FindElement(by).GetAttribute("type").Equals("password"));
                if (resultText == true || resultPass == true)
                { return true; }
                else
                { return false; }
            }
            catch
            {
                return false;
            }
        }

        private IWebElement WaitforElement(By by, int timeToReadyElement = 0)
        {
            IWebElement element = null;
            try
            {
                if (timeToReadyElement != 0 && timeToReadyElement.ToString() != null)
                {
                    PlaybackWait(timeToReadyElement * 1000);
                }
                element = driver.FindElement(by);
            }
            catch
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.Until(driver => IsPageReady(driver) == true && IsElementVisible(by) == true && IsClickable(by) == true);
                element = driver.FindElement(by);
            }
            return element;
        }

        private bool IsPageReady(IWebDriver driver)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
        }

        private bool IsElementVisible(By by)
        {
            if (driver.FindElement(by).Displayed || driver.FindElement(by).Enabled)
            {
                return true;
            }
            else
            { return false; }
        }

        private bool IsClickable(By by)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
    #endregion
}

