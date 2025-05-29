using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace PageObjectModelPractice
{

    [TestClass]


    public class TestExecution : ExtentReport
    {
        public TestContext instance;
        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }
        [TestInitialize]
        public void Testint()
        {
          
        BasePage.SeleniumInit();
        }
        [TestCleanup]
        public void Testcleanup()
        {
            BasePage.SeleniumClose();
        }

        [TestMethod]
        public void TestMethod1()
        {

            LoginPage loginPage = new LoginPage();
            ExtentReport.exChildTest = ExtentReport.exParentTest.CreateNode("login");
            loginPage.Login("https://adactinhotelapp.com/", "AmirImam", "AmirImam");
        }
      
    }
}
