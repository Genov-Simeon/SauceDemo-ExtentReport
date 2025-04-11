using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class CartPage : BasePage
    {
        private const string CartItemName = ".inventory_item_name";
        private const string CheckoutButton = "[data-test='checkout']";

        public CartPage(IPage page) : base(page)
        {
        }

        public async Task Open()
        {
            await Click(".shopping_cart_link");
            await WaitForPageLoad();
        }

        public async Task<List<string>> GetCartItemsNames()
        {
            return (List<string>)await Locator(CartItemName).AllTextContentsAsync();
        }

        public async Task ProceedToCheckout()
        {
            await Click(CheckoutButton);
            await WaitForPageLoad();
        }
    }
} 