using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class CheckoutCompletePage : BasePage
    {
        private const string CompleteHeader = ".complete-header";
        private const string CompleteText = ".complete-text";
        private const string BackHomeButton = "[data-test='back-to-products']";

        public CheckoutCompletePage(IPage page) : base(page)
        {
        }

        public async Task AssertThatOrderIsComplete()
        {
            var headerText = await GetText(CompleteHeader);
            Assert.That(headerText, Is.EqualTo("Thank you for your order!"), "Order completion header should match");

            var completeText = await GetText(CompleteText);
            Assert.That(completeText, Is.EqualTo("Your order has been dispatched, and will arrive just as fast as the pony can get there!"), 
                "Order completion text should match");
        }

        public async Task LogOut()
        {
            await Click("#react-burger-menu-btn");
            await Click("#logout_sidebar_link");
        }
    }
} 