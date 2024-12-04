using OpenQA.Selenium;
using SauceDemoUI.Customizations.Pages;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using Unity;

namespace SauceDemoUI.Customizations;
public abstract class BasePage
{
    protected IWebDriver Driver { get; private set; }

    protected IUnityContainer Container { get; private set; }

    public ExtentTest ExtentTest { get; set; }

    protected string BaseUrl { get; private set; }

    protected SideBarSection SideBarSection { get; private set; }

    protected WebDriverWait Wait { get; private set; }

    protected BasePage(IWebDriver driver, IUnityContainer container)
    {
        Container = container;

        Driver = driver;

        SideBarSection = new SideBarSection(driver);

        BaseUrl = Configuration.BaseUrl;

        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        ExtentTest = Container.Resolve<ExtentTest>();
    }

    public virtual void Open(string relativePath = "")
    {
        Driver.Navigate().GoToUrl(BaseUrl + relativePath);
        ExtentTest.Log(Status.Info, $"Navigated to {GetType().Name}");
    }

    // Abstracted the method so that it can be called from any page
    public void LogOut()
    {
        SideBarSection.HamburgerIcon.Click();

        WaitUntilElementIsVisible(SideBarSection.LogoutLink);

        SideBarSection.LogoutLink.Click();
        ExtentTest.Log(Status.Info, $"Logging out from the system");
    }

    protected void WaitUntilElementIsVisible(IWebElement element)
    {
        Wait.Until(driver => element.Displayed);
    }
}