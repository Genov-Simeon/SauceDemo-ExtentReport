using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutCompletePage
    {
        public IWebElement CompleteHeader => Driver.FindElement(By.ClassName("complete-header"));

        public IWebElement PonyExpressImage => Driver.FindElement(By.ClassName("pony_express"));
    }
}
