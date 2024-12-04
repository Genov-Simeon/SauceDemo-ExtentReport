using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoUI.Customizations.Extensions;
using Unity;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutCompletePage : BasePage
    {
        public CheckoutCompletePage(IWebDriver driver, IUnityContainer container) : base(driver, container)
        {
        }

        public override void Open(string relativePath = "/checkout-complete.html")
        {
            base.Open(relativePath);
        }

        public string GetConfirmationMessage()
        {
            return CompleteHeader.Text;
        }
        
        public bool IsOrderComplete()
        {
            try
            {
                return CompleteHeader.IsDisplayed() &&
                       PonyExpressImage.IsDisplayed() &&
                       GetConfirmationMessage().Equals("Thank you for your order!");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
