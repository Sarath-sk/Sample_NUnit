using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Sample_NUnit.Pages;
using Sample_NUnit.Utilities;

namespace Sample_NUnit
{
    public class Admin
    {
        // Driver declaration
        IWebDriver driver;
        EdgeOptions options;

        // Object declaration for pages 
        LoginPage lp;

        // Reports declaration
        ExtentReports reports;
        ExtentHtmlReporter htmlreport;
        

        // Setup for project to start project from here
        // To run before every test case
        [SetUp]
        public void setup()
        {
            // EdgeOptions creation and declaration
            options = new EdgeOptions();

            // Using EdgeOptions set customized browser
            options.AddArguments("--start-maximize","--headless");

            // Service object to store logs of a project
            EdgeDriverService service = EdgeDriverService.CreateDefaultService();

            // Given path to store logs
            service.LogPath = constant.LogPath;

            // Driver intialization with EdgeOptions and service
            driver = new EdgeDriver(service, options);

            // Navigation to given URL
            driver.Navigate().GoToUrl(constant.URL);

            // Page Objects intialization
            lp = new LoginPage(driver);
            

        }
        // To run once before all tests
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Reports object intialization
            reports = new ExtentReports();
            // html report object intialization and given path to store reports
            htmlreport = new ExtentHtmlReporter(constant.ReportstPath);
            // Attach html report to extent reports
            reports.AttachReporter(htmlreport);

        }
        // Method for testcase Pass scenarios
        public void TestCaseIfPass(string title, string description)
        {
            // Creating a testcase in the reports with title
            var testcase = reports.CreateTest(title);
            
            // Passing the created testcase with success message
            testcase.Pass(description);
        }
        // Method for testcase Fail scenarios
        public void TestCaseIfFail(string title, string description)
        {
            // Creating a testcase in the reports with title
            var testcase = reports.CreateTest(title);

            // Failing the created testcase with description/exception message
            testcase.Fail(description);

            // Adding screenshot as evidence
            // Creating screenshot object to get screenshot
            ITakesScreenshot screenshot = (ITakesScreenshot)driver;

            // get the screenshot by using GetScreenshot() method
            var scrshot = screenshot.GetScreenshot();

            // Attaching the screenshot as Base64String
            testcase.AddScreenCaptureFromBase64String(scrshot.AsBase64EncodedString);
        }

        // To run after all tests
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            reports.Flush();
        }

        // To run after every testcase
        
        [TearDown]
        public void teardown()
        {
            driver.Quit();
        }

        [Test]
        public void VerifyLogin()
        {
            try
            {
                // Calling method with object
                lp.Login();
                // Calling testcase pass method to add the result in the report
                TestCaseIfPass("VerifyLogin", "Logged in successfully!!");
            }
            catch (Exception ex)
            {
                // Print error/ exception which was caught
                Console.WriteLine(ex.Message);
                // Calling testcase fail method to add the result in the report
                TestCaseIfFail("VerifyLogin", ex.Message);
                // throw and caught exception
                throw ex;

            }
            
        }


    }
}