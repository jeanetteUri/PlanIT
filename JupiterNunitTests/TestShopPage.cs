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
        tempElement = TestHelper.LocateControl(validationHelper.ContactPageFromHomePage, driver, extentReportHelper);
        tempElement.Click();
        extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ContactPageFromHomePage.Tag}");

    }
}
