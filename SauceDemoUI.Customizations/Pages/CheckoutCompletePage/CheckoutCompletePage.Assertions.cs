using AventStack.ExtentReports;
using NUnit.Framework;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class CheckoutCompletePage
    {
        public void AssertThatOrderIsComplete()
        {
            Assert.That(IsOrderComplete(), Is.True, "Order not completed");
            ExtentTest.Log(Status.Info, "Checkout was successful!");
        }
    }
}
