using OpenQA.Selenium;
using SauceDemoUI.Customizations.Models;
using SauceDemoUI.Customizations.Extensions;
using Unity;
using AventStack.ExtentReports;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver, IUnityContainer container) : base(driver, container)
        {
        }

        public override void Open(string relativePath = "")
        {
            base.Open(relativePath);
        }

        public void Login(UserInfo userInfo)
        {
            UsernameField.SendKeys(userInfo.UserName);
            PasswordField.SendKeys(userInfo.Password);
            LoginButton.Click();

            ExtentTest.Log(Status.Info, $"Logging in");
        }

        public bool IsDisplayed()
        {
            try
            {
                return LoginButton.IsDisplayed() && 
                       UsernameField.IsDisplayed() && 
                       PasswordField.IsDisplayed();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
