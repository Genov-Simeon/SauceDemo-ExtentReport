using OpenQA.Selenium;
using Unity;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CartPage : BasePage
    {
		public CartPage(IWebDriver driver, IUnityContainer container) : base(driver, container)
		{
		}

        public override void Open(string relativePath = "/cart.html")
        {
            base.Open(relativePath);
        }

        public List<string> GetCartItemsNames()
        {
            try
            {
                var cartItemDescriptionElements = Driver.FindElements(By.XPath("//div[@class='inventory_item_name']"));

                return cartItemDescriptionElements.Select(desc => desc.Text).ToList();
            }
            catch (NoSuchElementException ex)
            {
                throw new InvalidOperationException("No item descriptions were found in the cart.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while extracting item descriptions from the cart.", ex);
            }
        }
    }
}
