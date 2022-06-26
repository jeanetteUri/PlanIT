using JupiterTestHelper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace JupiterNunitTests
{
    public abstract class TestBase
    {
        protected IWebDriver driver;
        protected ReportHelper extentReportHelper;
        protected ValidationHelper validationHelper;
        protected abstract string FileName{ get; set; }

        [OneTimeSetUp]
        public void SuiteSetup()
        {
            validationHelper = JsonSerializer.Deserialize<ValidationHelper>(File.ReadAllText("appsettings.json"));
            extentReportHelper = new ReportHelper(FileName);
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void SuiteClose()
        {
            extentReportHelper.Close();
        }
    }


}
