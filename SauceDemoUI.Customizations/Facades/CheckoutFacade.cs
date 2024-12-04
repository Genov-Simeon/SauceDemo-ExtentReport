using AventStack.ExtentReports;
using SauceDemoUI.Customizations.Pages;

namespace SauceDemoUI.Customizations.Facades
{
    public class CheckoutFacade
    {
        private readonly CartPage _cartPage;
        private readonly CheckoutStepOnePage _checkoutStepOnePage;
        private readonly CheckoutStepTwoPage _checkoutStepTwoPage;

        public CheckoutFacade(CartPage cartPage, CheckoutStepOnePage checkoutStepOnePage, CheckoutStepTwoPage checkoutStepTwoPage)
        {
            _cartPage = cartPage;
            _checkoutStepOnePage = checkoutStepOnePage;
            _checkoutStepTwoPage = checkoutStepTwoPage;            
        }

        public void ProceedToCheckOut()
        {
            _cartPage.ExtentTest.Log(Status.Info, "Proceed with checkout");
            _cartPage.CheckoutButton.Click();
            _checkoutStepOnePage.FillAllFields();
            _checkoutStepOnePage.ContinueButton.Click();
            _checkoutStepTwoPage.FinishButton.Click();
        }
    }
}
