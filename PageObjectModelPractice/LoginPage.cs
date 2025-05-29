using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPractice
{
    internal class LoginPage : BasePage
    {
        By usernameTxt = By.Id("username");
        By passwordTxt = By.Id("password");
        By LoginBtn = By.Id("login");
       
        public void Login (string url, string username, string password)
        {

            ExtentReport.exChildTest = ExtentReport.exParentTest.CreateNode("Login");
            Write(usernameTxt,username);
            Write(passwordTxt,password);
            Click(LoginBtn);
        }
    }
}
