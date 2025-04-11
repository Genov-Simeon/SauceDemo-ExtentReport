using Microsoft.Playwright;
using AventStack.ExtentReports;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace SauceDemoUI.Playwright.Tests
{
    public class BaseTest
    {
        protected IPlaywright Playwright { get; set; }
        protected IBrowser Browser { get; set; }
        protected IBrowserContext Context { get; set; }
        protected IPage? Page { get; set; }
        protected static ExtentReports ExtentReports { get; set; }
        protected ExtentTest ExtentTest { get; set; }
        protected string BrowserName { get; set; }
        protected IConfiguration Configuration { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            // Initialize Configuration first
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{TestContext.Parameters["TestEnvironment"] ?? "DEV"}.json")
                .Build();

            // Initialize ExtentReports
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "index.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
            
            ExtentReports = new ExtentReports();
            var spark = new ExtentSparkReporter(reportPath);
            spark.Config.Theme = Theme.Dark;
            spark.Config.DocumentTitle = "Sauce Demo UI Tests Report";
            spark.Config.ReportName = "Playwright Test Execution Report";
            
            ExtentReports.AttachReporter(spark);
            ExtentReports.AddSystemInfo("Environment", TestContext.Parameters["TestEnvironment"] ?? "DEV");
            ExtentReports.AddSystemInfo("Browser", Configuration["Browser"] ?? "chromium");
            ExtentReports.AddSystemInfo("OS", Environment.OSVersion.ToString());
        }

        [SetUp]
        public async Task Setup()
        {
            ExtentTest = ExtentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            BrowserName = Configuration["Browser"] ?? "chromium";
            
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            
            var browserType = BrowserName.ToLower() switch
            {
                "chromium" => Playwright.Chromium,
                "firefox" => Playwright.Firefox,
                "webkit" => Playwright.Webkit,
                _ => throw new ArgumentException($"Browser '{BrowserName}' is not supported.")
            };

            var viewportSize = new ViewportSize
            {
                Width = int.Parse(TestContext.Parameters["Width"] ?? "1920"),
                Height = int.Parse(TestContext.Parameters["Height"] ?? "1080")
            };

            Browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = viewportSize
            });

            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    ExtentTest.Fail($"Test Failed: {errorMessage}");
                    ExtentTest.Fail($"Stack Trace: {stackTrace}");
                    
                    // Capture screenshot on failure
                    var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots", $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath));
                    await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
                    ExtentTest.AddScreenCaptureFromPath(screenshotPath);
                    break;
                    
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    ExtentTest.Pass("Test Passed");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    ExtentTest.Skip("Test Skipped");
                    break;
            }

            await Page?.CloseAsync();
            await Context?.CloseAsync();
            await Browser?.CloseAsync();
            Playwright?.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReports?.Flush();
        }
    }
} 