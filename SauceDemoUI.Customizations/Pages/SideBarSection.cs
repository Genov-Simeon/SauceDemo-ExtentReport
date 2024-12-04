using OpenQA.Selenium;

namespace SauceDemoUI.Customizations.Pages
{
    public class SideBarSection
    {
        private readonly IWebDriver _driver;
        
        public SideBarSection(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement HamburgerIcon => _driver.FindElement(By.Id("react-burger-menu-btn"));

        public IWebElement LogoutLink => _driver.FindElement(By.Id("logout_sidebar_link"));
    }
}