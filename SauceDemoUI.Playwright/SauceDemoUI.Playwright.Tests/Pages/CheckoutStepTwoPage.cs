using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class CheckoutStepTwoPage : BasePage
    {
        private const string FinishButton = "[data-test='finish']";

        public CheckoutStepTwoPage(IPage page) : base(page)
        {
        }

        public async Task FinishCheckout()
        {
            await Click(FinishButton);
            await WaitForPageLoad();
        }
    }
} 