using SauceDemoUI.Customizations.Factories;
using SauceDemoUI.Customizations.Pages;
using AventStack.ExtentReports;
using System.Drawing;
using SauceDemoUI.Customizations.Facades;
using Unity;

namespace SauceDemoUI.Tests
{
    public class Scenario1Test : BaseTest
    {
        [SetUp]
        public void OneTimeSetUp()
        {
            CheckoutFacade = App.Container.Resolve<CheckoutFacade>();
        }

        [Category("EndToEndTest")]
        [TestCase(1920, 1080)]
        [TestCase(1366, 768)]
        [TestCase(414, 896)]
        public void CompleteOrderFlow(int width, int height)
        {
            Driver.Manage().Window.Size = new Size(width, height);

            ExtentTest.Test.Name = $"Complete Order Flow Test. Resolution: {width} x {height}";
            ExtentTest.Log(Status.Info, $"Test for resolution {width} x {height} has started");

            try
            {
                var user = UserInfoFactory.BuildUserCredentials("USER_STANDARD", "PASSWORD");
                LoginPage.Open();
                LoginPage.Login(user);

                var firstItemName = InventoryPage.GetFirstItemName();
                var lastItemName = InventoryPage.GetLastItemName();
                var previousToLastItemName = InventoryPage.GetPreviousToLastItemName();

                // Add the first and the last item in the cart
                InventoryPage.AddItemToCart(firstItemName);
                InventoryPage.AddItemToCart(lastItemName);

                Assert.That(InventoryPage.GetCartItemCount(), Is.EqualTo(2), "Cart count should be 2");
                CartPage.Open();
                var cartItemDescriptions = CartPage.GetCartItemsNames();
                Assert.IsTrue(cartItemDescriptions.Contains(firstItemName), "First item is not in the Cart");
                Assert.IsTrue(cartItemDescriptions.Contains(lastItemName), "Last item is not in the Cart");

                // Remove the first item and add previous to the last item to the cart
                InventoryPage.Open();
                InventoryPage.RemoveItemFromCart(firstItemName);
                InventoryPage.AddItemToCart(previousToLastItemName);

                Assert.That(InventoryPage.GetCartItemCount(), Is.EqualTo(2), "Cart count should be 2");
                CartPage.Open();
                cartItemDescriptions = CartPage.GetCartItemsNames();
                Assert.IsTrue(cartItemDescriptions.Contains(previousToLastItemName), "Previous to last item is not in the Cart");
                Assert.IsTrue(cartItemDescriptions.Contains(lastItemName), "Last item is not in the Cart");

                // Proceed to Checkout
                CheckoutFacade.ProceedToCheckOut();

                CheckoutCompletePage.AssertThatOrderIsComplete();

                // Log out
                CheckoutCompletePage.LogOut();
                LoginPage.AssertLoginPageIsDiplayed();                

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
