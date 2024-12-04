using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutStepOnePage
    {
        public IWebElement FirstNameField => Driver.FindElement(By.Id("first-name"));
        
        public IWebElement LastNameField => Driver.FindElement(By.Id("last-name"));
        
        public IWebElement PostalCodeField => Driver.FindElement(By.Id("postal-code"));
        
        public IWebElement ContinueButton => Driver.FindElement(By.Id("continue"));
    }
}
