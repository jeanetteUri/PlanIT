using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JupiterTestHelper;
using System.Reflection;
using System.Text.Json;

using System.Diagnostics;

namespace JupiterNunitTests;

public class TestShopPage
{
    IWebDriver driver;
    ReportHelper extentReportHelper;
    ValidationHelper validationHelper;

    [Test]
    public void TestCase3_ShopPage_AddtoCart_VerifyCart()
    {
        IWebElement tempElement;
        driver.Manage().Window.Maximize();
        extentReportHelper.CreateTest(MethodBase.GetCurrentMethod().Name);
        extentReportHelper.SetStepStatusPass("Chrome Browser opened");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        driver.Url = validationHelper.URLUnderTest;
        tempElement = TestHelper.LocateControl(validationHelper.ShopPageFromHomePage, driver, extentReportHelper);
        tempElement.Click();
        extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ShopPageFromHomePage.Tag}");

        for(int i = 0; i < 2; i++)
        {
            tempElement = TestHelper.LocateControl(validationHelper.Product2_StuffedFrog, driver, extentReportHelper);
            tempElement = tempElement.FindElement(By.ClassName("btn-success"));
            tempElement.Click();
            extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.Product2_StuffedFrog.Tag}");
        }
        for (int i = 0; i < 5; i++)
        {
            tempElement = TestHelper.LocateControl(validationHelper.Product4_FluffyBunny, driver, extentReportHelper);
            tempElement = tempElement.FindElement(By.ClassName("btn-success"));
            tempElement.Click();
            extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.Product4_FluffyBunny.Tag}");
        }
        for (int i = 0; i < 3; i++)
        {
            tempElement = TestHelper.LocateControl(validationHelper.Product7_ValentineBear, driver, extentReportHelper);
            tempElement = tempElement.FindElement(By.ClassName("btn-success"));
            tempElement.Click();
            extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.Product7_ValentineBear.Tag}");
        }

        tempElement = TestHelper.LocateControl(validationHelper.ShopPageCartCount, driver, extentReportHelper);
        if(tempElement.Text != "10")
        {
            extentReportHelper.SetStepStatusPass($"Cart count is not complete. Expected:10  Actual:{tempElement.Text}");
            Assert.Fail($"Cart count is not complete. Expected:10  Actual:{tempElement.Text}");
        }

        extentReportHelper.SetStepStatusPass($"Cart count. Expected:10  Actual:{tempElement.Text}");
        extentReportHelper.SetStepStatusPass("Passed");
        Assert.Pass();

    }

    [SetUp]
    public void Setup()
    {
        validationHelper = JsonSerializer.Deserialize<ValidationHelper>(File.ReadAllText("appsettings.json"));
        extentReportHelper = new ReportHelper();
        driver = new ChromeDriver();
    }

    [TearDown]
    public void Close()
    {
        driver.Close();
        driver.Quit();
        extentReportHelper.Close();
    }
}
