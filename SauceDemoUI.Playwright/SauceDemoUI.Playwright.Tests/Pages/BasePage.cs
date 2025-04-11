using Microsoft.Playwright;

namespace SauceDemoUI.Playwright.Tests.Pages
{
    public class BasePage
    {
        protected IPage Page { get; }
        protected Func<string, ILocator> Locator { get; }

        public BasePage(IPage page)
        {
            Page = page;
            Locator = selector => page.Locator(selector);
        }

        protected ILocator GetLocator(string selector) => Page.Locator(selector);


        protected async Task WaitForPageLoad()
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        protected async Task Click(string selector)
        {
            await Page.Locator(selector).ClickAsync();
            await WaitForPageLoad();
        }

        protected async Task Fill(string selector, string value)
        {
            await Page.Locator(selector).FillAsync(value);
        }

        protected async Task<string> GetText(string selector)
        {
            return await Page.Locator(selector).TextContentAsync();
        }

        protected async Task<bool> IsVisible(string selector)
        {
            return await Page.Locator(selector).IsVisibleAsync();
        }

        protected async Task<string> GetAttribute(string selector, string attribute)
        {
            return await Page.Locator(selector).GetAttributeAsync(attribute);
        }
    }
} 