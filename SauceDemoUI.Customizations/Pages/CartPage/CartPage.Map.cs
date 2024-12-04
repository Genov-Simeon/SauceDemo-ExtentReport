using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CartPage
    {        
        public IWebElement CheckoutButton => Driver.FindElement(By.Id("checkout"));
    }
}
