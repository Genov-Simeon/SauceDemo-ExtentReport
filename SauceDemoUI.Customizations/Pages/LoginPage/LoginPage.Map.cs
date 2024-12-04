using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class LoginPage
    {
        private IWebElement UsernameField => Driver.FindElement(By.Id("user-name"));
        
        private IWebElement PasswordField => Driver.FindElement(By.Id("password"));
        
        private IWebElement LoginButton => Driver.FindElement(By.Id("login-button"));
        
        private IWebElement ErrorMessage => Driver.FindElement(By.ClassName("error-message-container"));
    }
}
