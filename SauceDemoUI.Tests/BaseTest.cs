using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoUI.Customizations.Pages;
using Unity;
using AventStack.ExtentReports;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SauceDemoUI.Customizations;
using SauceDemoUI.Customizations.Facades;

namespace SauceDemoUI.Tests
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        public ExtentReports ExtentReports { get; set; }

        public ExtentTest? ExtentTest { get; set; }

        public string Browser { get; set; }

        protected CartPage CartPage { get; set; }

        protected CheckoutStepOnePage CheckoutStepOnePage { get; set; }

        protected CheckoutStepTwoPage CheckoutStepTwoPage { get; set; }

        protected CheckoutCompletePage CheckoutCompletePage { get; set; }
        
        protected InventoryPage InventoryPage { get; set; }

        protected LoginPage LoginPage { get; set; }

        protected CheckoutFacade CheckoutFacade { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ExtentReports = App.Container.Resolve<ExtentReports>();
        }

        [SetUp]
        public void SetUp()
        {
            ExtentTest = ExtentReports.CreateTest("Default test");
            Browser = Configuration.Browser;
            switch (Browser)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    Driver = new ChromeDriver(chromeOptions);
                    break;

                case "Firefox":
                    var firefoxOptions = new FirefoxOptions();
                    Driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    Driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Browser '{Browser}' is not supported.");
            }

            App.Container.RegisterInstance(Driver);
            App.Container.RegisterInstance(ExtentTest);

            CartPage = App.Container.Resolve<CartPage>();
            CheckoutStepOnePage = App.Container.Resolve<CheckoutStepOnePage>();
            CheckoutStepTwoPage = App.Container.Resolve<CheckoutStepTwoPage>();
            CheckoutCompletePage = App.Container.Resolve<CheckoutCompletePage>();
            InventoryPage = App.Container.Resolve<InventoryPage>();
            LoginPage = App.Container.Resolve<LoginPage>();
        }

        [TearDown]
        public void TearDown()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
