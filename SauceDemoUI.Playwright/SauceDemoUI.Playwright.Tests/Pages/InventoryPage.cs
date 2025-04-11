using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class InventoryPage : BasePage
    {
        private const string ItemName = ".inventory_item_name";
        private const string ItemPrice = ".inventory_item_price";
        private const string AddToCartButton = "[data-test='add-to-cart']";
        private const string RemoveFromCartButton = "[data-test='remove']";
        private const string CartBadge = ".shopping_cart_badge";
        private const string SortDropdown = "[data-test='product_sort_container']";

        public InventoryPage(IPage page) : base(page)
        {
        }

        public async Task<string> GetFirstItemName()
        {
            return await GetText($"{ItemName}:first-child");
        }

        public async Task<string> GetLastItemName()
        {
            return await GetText($"{ItemName}:last-child");
        }

        public async Task<string> GetPreviousToLastItemName()
        {
            return await GetText($"{ItemName}:nth-last-child(2)");
        }

        public async Task AddItemToCart(string itemName)
        {
            var itemLocator = Locator($"{ItemName}:text('{itemName}')");
            var addButton = itemLocator.Locator("xpath=../..").Locator(AddToCartButton);
            await addButton.ClickAsync();
        }

        public async Task RemoveItemFromCart(string itemName)
        {
            var itemLocator = Locator($"{ItemName}:text('{itemName}')");
            var removeButton = itemLocator.Locator("xpath=../..").Locator(RemoveFromCartButton);
            await removeButton.ClickAsync();
        }

        public async Task<int> GetCartItemCount()
        {
            var badgeText = await GetText(CartBadge);
            return int.Parse(badgeText);
        }

        public async Task<List<double>> GetItemPricesAsDoubles()
        {
            var prices = await Locator(ItemPrice).AllTextContentsAsync();
            return prices.Select(p => double.Parse(p.Replace("$", ""))).ToList();
        }

        public async Task SortBy(string option)
        {
            await Locator(SortDropdown).SelectOptionAsync(new SelectOptionValue { Label = option });
            await WaitForPageLoad();
        }

        public async Task AssertThatItemsSortedDescending(List<double> prices)
        {
            for (int i = 0; i < prices.Count - 1; i++)
            {
                Assert.That(prices[i], Is.GreaterThanOrEqualTo(prices[i + 1]), 
                    $"Item at index {i} should be greater than or equal to item at index {i + 1}");
            }
        }

        public async Task LogOut()
        {
            await Click("#react-burger-menu-btn");
            await Click("#logout_sidebar_link");
        }
    }
} 