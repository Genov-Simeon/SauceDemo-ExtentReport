using AventStack.ExtentReports;
using SauceDemoUI.Customizations.Factories;
using SauceDemoUI.Customizations.Pages;
using System.Drawing;

namespace SauceDemoUI.Tests
{
    public class Scenario2Test : BaseTest
    {
        [Category("Sorting")]
        [TestCase(1920, 1080)]
        [TestCase(1366, 768)]
        [TestCase(414, 896)]
        public void VerifyPrice_When_SortFromHighToLow(int width, int height)
        {
            Driver.Manage().Window.Size = new Size(width, height);

            ExtentTest.Test.Name = $"Sorting test. Resolution: {width} x {height}";
            ExtentTest.Log(Status.Info, $"Test for resolution {width} x {height} has started");

            try
            {
                var user = UserInfoFactory.BuildUserCredentials("USER_STANDARD", "PASSWORD");
                LoginPage.Open();
                LoginPage.Login(user);

                InventoryPage.SortBy(SortOptions.PriceHighToLow);
                var sortedPrices = InventoryPage.GetItemPricesAsDoubles();

                InventoryPage.AssertThatItemsSortedDescending(sortedPrices);                

                InventoryPage.LogOut();
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
