using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Unity;

namespace SauceDemoUI.Tests
{
    [SetUpFixture]
    public class TestInitialize
    {
        public ExtentReports? ExtentReports { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var sparkReporter = new ExtentSparkReporter($@"{Environment.CurrentDirectory}\Test-Output\test-Report.html");
            sparkReporter.Config.DocumentTitle = "Automation Report";
            sparkReporter.Config.ReportName = "Test report";
            sparkReporter.Config.Encoding = "UTF-8";
            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(sparkReporter);

            App.Container.RegisterInstance(ExtentReports);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            App.Container?.Dispose();
            ExtentReports.Flush();
        }
    }
}
