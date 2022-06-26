using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JupiterTestHelper;
using OpenQA.Selenium;

namespace JupiterNunitTests;



public class TestHelper
{
    public static IWebElement LocateControl(LocatorField field, IWebDriver driver, ReportHelper extentReportHelper)
    {
        try{
            IWebElement element = driver.FindElement(field.ById != null ? By.Id(field.ById) : By.ClassName(field.ByClass));
            extentReportHelper.SetStepStatusPass($"Found {field.Tag}");
            return element;
              
        }
        catch{
            Assert.Fail($"Element {field.Tag} not found");
            throw new NoSuchElementException($"Element {field.Tag} not found");
            extentReportHelper.SetTestStatusFail($"Element {field.Tag} not found");
            
        }
    }

    public static bool IsElementExisting(LocatorField field, IWebDriver driver)
    {
        try
        {
            IWebElement element = driver.FindElement(field.ById != null ? By.Id(field.ById) : By.ClassName(field.ByClass));
            return true;
            
        }
        catch
        {
            return false;
        }
    }
}
