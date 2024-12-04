using AventStack.ExtentReports;
using OpenQA.Selenium;
using Unity;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutStepTwoPage : BasePage
    {
        public CheckoutStepTwoPage(IWebDriver driver, IUnityContainer container) : base(driver, container)
        {
        }

        public override void Open(string relativePath = "/checkout-step-two.html")
        {
            base.Open(relativePath);
        }
    }
}
