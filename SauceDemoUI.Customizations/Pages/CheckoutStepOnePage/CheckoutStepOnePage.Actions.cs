using AventStack.ExtentReports;
using OpenQA.Selenium;
using SauceDemoUI.Customizations.Factories;
using SauceDemoUI.Customizations.Models;
using Unity;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutStepOnePage : BasePage
    {
        public CheckoutStepOnePage(IWebDriver driver, IUnityContainer container) : base(driver, container)
        {
        }

        public override void Open(string relativePath = "/checkout-step-one.html")
        {
            base.Open(relativePath);
        }

        public void FillAllFields()
        {
            UserData userData = UserDataFactory.Generate();
            FirstNameField.SendKeys(userData.FirstName);
            LastNameField.SendKeys(userData.LastName);
            PostalCodeField.SendKeys(userData.PostalCode);
        }
    }
}
