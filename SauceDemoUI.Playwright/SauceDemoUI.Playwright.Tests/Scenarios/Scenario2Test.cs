using AventStack.ExtentReports;
using SauceDemoUI.Playwright.Tests.Pages;

namespace SauceDemoUI.Playwright.Tests.Scenarios
{
    public class Scenario2Test : BaseTest
    {
        private LoginPage LoginPage { get; set; }
        private InventoryPage InventoryPage { get; set; }

        [SetUp]
        public async Task Setup()
        {
            LoginPage = new LoginPage(Page);
            InventoryPage = new InventoryPage(Page);
        }

        [Category("Sorting")]
        [TestCase(1920, 1080)]
        [TestCase(1366, 768)]
        [TestCase(414, 896)]
        public async Task VerifyPrice_When_SortFromHighToLow(int width, int height)
        {
            ExtentTest.Test.Name = $"Sorting test. Resolution: {width} x {height}";
            ExtentTest.Log(Status.Info, $"Test for resolution {width} x {height} has started");

            try
            {
                await LoginPage.Open();
                await LoginPage.Login("standard_user", "secret_sauce");

                await InventoryPage.SortBy("Price (high to low)");
                var sortedPrices = await InventoryPage.GetItemPricesAsDoubles();

                await InventoryPage.AssertThatItemsSortedDescending(sortedPrices);

                await InventoryPage.LogOut();
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