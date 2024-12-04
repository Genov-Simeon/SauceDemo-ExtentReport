using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutStepTwoPage
    {
        public IWebElement FinishButton => Driver.FindElement(By.Id("finish"));        
    }
}
