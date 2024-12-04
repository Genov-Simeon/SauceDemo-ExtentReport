using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class InventoryPage
    {
        public IWebElement SortDropDown => Driver.FindElement(By.ClassName("product_sort_container"));

        public List<IWebElement> ItemPrices => Driver.FindElements(By.ClassName("inventory_item_price")).ToList();

        public List<IWebElement> InventoryItemNames => Driver.FindElements(By.XPath("//div[@class='inventory_item_name ']")).ToList();

        private IWebElement GetProductContainer(string productName)
        {
            try
            {
                return Driver.FindElement(By.XPath($"//div[@class='inventory_item' and .//div[@class='inventory_item_name ' and text()='{productName}']]"));
            }
            catch (NoSuchElementException)
            {
                throw new InvalidOperationException($"Product with name '{productName}' could not be found.");
            }
        }
    }
}