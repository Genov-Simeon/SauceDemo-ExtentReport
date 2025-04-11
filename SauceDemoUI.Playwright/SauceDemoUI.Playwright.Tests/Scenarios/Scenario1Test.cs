using AventStack.ExtentReports;
using SauceDemoUI.Playwright.Tests.Pages;

namespace SauceDemoUI.Playwright.Tests.Scenarios
{
    public class Scenario1Test : BaseTest
    {
        private LoginPage LoginPage { get; set; }
        private InventoryPage InventoryPage { get; set; }
        private CartPage CartPage { get; set; }
        private CheckoutStepOnePage CheckoutStepOnePage { get; set; }
        private CheckoutStepTwoPage CheckoutStepTwoPage { get; set; }
        private CheckoutCompletePage CheckoutCompletePage { get; set; }

        [SetUp]
        public async Task Setup()
        {
            LoginPage = new LoginPage(Page);
            InventoryPage = new InventoryPage(Page);
            CartPage = new CartPage(Page);
            CheckoutStepOnePage = new CheckoutStepOnePage(Page);
            CheckoutStepTwoPage = new CheckoutStepTwoPage(Page);
            CheckoutCompletePage = new CheckoutCompletePage(Page);
        }

        [Category("EndToEndTest")]
        [TestCase(1920, 1080)]
        [TestCase(1366, 768)]
        [TestCase(414, 896)]
        public async Task CompleteOrderFlow(int width, int height)
        {
            ExtentTest.Test.Name = $"Complete Order Flow Test. Resolution: {width} x {height}";
            ExtentTest.Log(Status.Info, $"Test for resolution {width} x {height} has started");

            try
            {
                await LoginPage.Open();
                await LoginPage.Login("standard_user", "secret_sauce");

                var firstItemName = await InventoryPage.GetFirstItemName();
                var lastItemName = await InventoryPage.GetLastItemName();
                var previousToLastItemName = await InventoryPage.GetPreviousToLastItemName();

                // Add the first and the last item in the cart
                await InventoryPage.AddItemToCart(firstItemName);
                await InventoryPage.AddItemToCart(lastItemName);

                Assert.That(await InventoryPage.GetCartItemCount(), Is.EqualTo(2), "Cart count should be 2");
                await CartPage.Open();
                var cartItemDescriptions = await CartPage.GetCartItemsNames();
                Assert.That(cartItemDescriptions, Does.Contain(firstItemName), "First item is not in the Cart");
                Assert.That(cartItemDescriptions, Does.Contain(lastItemName), "Last item is not in the Cart");

                // Remove the first item and add previous to the last item to the cart
                await InventoryPage.RemoveItemFromCart(firstItemName);
                await InventoryPage.AddItemToCart(previousToLastItemName);

                Assert.That(await InventoryPage.GetCartItemCount(), Is.EqualTo(2), "Cart count should be 2");
                await CartPage.Open();
                cartItemDescriptions = await CartPage.GetCartItemsNames();
                Assert.That(cartItemDescriptions, Does.Contain(previousToLastItemName), "Previous to last item is not in the Cart");
                Assert.That(cartItemDescriptions, Does.Contain(lastItemName), "Last item is not in the Cart");

                // Proceed to Checkout
                await CartPage.ProceedToCheckout();
                await CheckoutStepOnePage.FillCheckoutInformation("John", "Doe", "12345");
                await CheckoutStepTwoPage.FinishCheckout();

                await CheckoutCompletePage.AssertThatOrderIsComplete();

                // Log out
                await CheckoutCompletePage.LogOut();
                await LoginPage.AssertLoginPageIsDisplayed();

                ExtentTest.Log(Status.Pass, "The test has passed!");
            }
            catch (Exception e)
            {
                ExtentTest.Log(Status.Fail, $"Test failed: {e.Message}");
                throw;
            }
        }
    }
} 