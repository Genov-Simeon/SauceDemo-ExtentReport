using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private const string UsernameInput = "#user-name";
        private const string PasswordInput = "#password";
        private const string LoginButton = "#login-button";
        private const string ErrorMessage = "[data-test='error']";

        public LoginPage(IPage page) : base(page)
        {
        }

        public async Task Open()
        {
            await Page.GotoAsync("https://www.saucedemo.com/");
            await WaitForPageLoad();
        }

        public async Task Login(string username, string password)
        {
            await Fill(UsernameInput, username);
            await Fill(PasswordInput, password);
            await Click(LoginButton);
        }

        public async Task AssertLoginPageIsDisplayed()
        {
            Assert.That(await IsVisible(UsernameInput), Is.True, "Username input should be visible");
            Assert.That(await IsVisible(PasswordInput), Is.True, "Password input should be visible");
            Assert.That(await IsVisible(LoginButton), Is.True, "Login button should be visible");
        }

        public async Task AssertErrorMessage(string expectedMessage)
        {
            var errorText = await GetText(ErrorMessage);
            Assert.That(errorText, Is.EqualTo(expectedMessage), "Error message should match");
        }
    }
} 