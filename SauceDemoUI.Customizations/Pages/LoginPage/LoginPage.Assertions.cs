using AventStack.ExtentReports;
using NUnit.Framework;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class LoginPage
    {
        public void AssertLoginPageIsDiplayed()
        {
            Assert.That(IsDisplayed(), Is.True, "Log out was not successful");
            ExtentTest.Log(Status.Info, $"Log out was successful");
        }
    }
}
