using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class CheckoutStepOnePage : BasePage
    {
        private const string FirstNameInput = "[data-test='firstName']";
        private const string LastNameInput = "[data-test='lastName']";
        private const string PostalCodeInput = "[data-test='postalCode']";
        private const string ContinueButton = "[data-test='continue']";

        public CheckoutStepOnePage(IPage page) : base(page)
        {
        }

        public async Task FillCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            await Fill(FirstNameInput, firstName);
            await Fill(LastNameInput, lastName);
            await Fill(PostalCodeInput, postalCode);
            await Click(ContinueButton);
            await WaitForPageLoad();
        }
    }
} 