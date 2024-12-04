using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsDisplayed(this IWebElement element)
        {
            try
            {
                return element != null && element.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (ElementNotVisibleException)
            {
                return false;
            }
        }
    }
} 