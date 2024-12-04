using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Globalization;
using Unity;
using AventStack.ExtentReports;
using NUnit.Framework;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class InventoryPage : BasePage
    {
        public InventoryPage(IWebDriver driver, IUnityContainer container) : base(driver, container)
        {
        }

        public override void Open(string relativePath = "/inventory.html")
        {
            base.Open(relativePath);
        }
  
        public void AddItemToCart(string productName)
        {
            try
            {
                var addToCartButton = GetProductContainer(productName).FindElement(By.XPath(".//button[contains(text(), 'Add to cart')]"));
                addToCartButton.Click();
                ExtentTest.Log(Status.Info, $"Item {productName} added to Cart");
            }
            catch (NoSuchElementException)
            {
                throw new InvalidOperationException($"The product '{productName}' could not be found or the 'Add to Cart' button is missing.");
            }
        }

        public void RemoveItemFromCart(string productName)
        {
            try
            {
                var removeButton = GetProductContainer(productName).FindElement(By.XPath(".//button[contains(text(), 'Remove')]"));
                removeButton.Click();
                ExtentTest.Log(Status.Info, $"Item {productName} removed from Cart");
            }
            catch (NoSuchElementException)
            {
                throw new InvalidOperationException($"No product with the name '{productName}' could be found, or the 'Remove' button is missing.");
            }
        }

        public int GetCartItemCount()
        {
            IWebElement CartBadge = Driver.FindElement(By.ClassName("shopping_cart_badge"));

            try
            {
                string cartItemCountText = CartBadge.Text;
                return int.Parse(cartItemCountText);
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
        }

        public string GetFirstItemName()
        {
            var inventoryItems = GetAllInventoryItemsNames();
            if (!inventoryItems.Any())
            {
                throw new InvalidOperationException("No inventory items found on the page.");
            }
            return inventoryItems.First();
        }

        public string GetLastItemName()
        {
            var inventoryItems = GetAllInventoryItemsNames();
            if (!inventoryItems.Any())
            {
                throw new InvalidOperationException("No inventory items found on the page.");
            }
            return inventoryItems.Last();
        }

        public string GetPreviousToLastItemName()
        {
            var inventoryItems = GetAllInventoryItemsNames();
            if (inventoryItems.Count < 2)
            {
                throw new InvalidOperationException("Not enough items to get second to last item");
            }
            return inventoryItems[inventoryItems.Count - 2];
        }

        public List<string> GetAllInventoryItemsNames()
        {
            var inventoryItemNames = new List<string>();

            foreach (var item in InventoryItemNames)
            {
                inventoryItemNames.Add(item.Text);
            }

            return inventoryItemNames;
        }

        public void SortBy(SortOptions option)
        {
            string sortText = option switch
            {
                SortOptions.NameAToZ => "Name (A to Z)",
                SortOptions.NameZToA => "Name (Z to A)",
                SortOptions.PriceLowToHigh => "Price (low to high)",
                SortOptions.PriceHighToLow => "Price (high to low)",
                _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
            };

            GetSortDropDown().SelectByText(sortText);

            ExtentTest.Log(Status.Info, $"Sorting items");
        }

        public List<double> GetItemPricesAsDoubles()
        {
            return ItemPrices
                .Select(item => double.Parse(
                    item.Text.Replace("$", ""),
                    CultureInfo.InvariantCulture))
                .ToList();
        }

        public bool IsSortedDescending(List<double> prices)
        {
            for (int i = 0; i < prices.Count - 1; i++)
            {
                if (prices[i] < prices[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        private SelectElement GetSortDropDown()
        {
            return new SelectElement(SortDropDown);
        }

        public void AssertThatItemsSortedDescending(List<double> sortedPrices)
        {
            Assert.That(IsSortedDescending(sortedPrices), Is.True, "The prices are not sorted in descending order");
            ExtentTest.Log(Status.Info, $"Items are sorted in descending order");
        }
    }
}
