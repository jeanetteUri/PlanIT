using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JupiterTestHelper;
namespace JupiterNunitTests;

using System.Reflection;
using  System.Text.Json;

[TestFixture]
public class TestContactPage
{
    IWebDriver driver;
    ReportHelper extentReportHelper;
    ValidationHelper validationHelper;

    [Test] 
    public void TestCase1_ContactPage_ValidateErrorMessages()
    {
        IWebElement tempElement;
        driver.Manage().Window.Maximize();
        extentReportHelper.CreateTest(MethodBase.GetCurrentMethod().Name);
        extentReportHelper.SetStepStatusPass("Chrome Browser opened"); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        driver.Url = validationHelper.URLUnderTest;

        tempElement = TestHelper.LocateControl(validationHelper.ContactPageFromHomePage,driver,extentReportHelper);
        tempElement.Click();
        extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ContactPageFromHomePage.Tag}");

        tempElement = TestHelper.LocateControl(validationHelper.ContactSubmitButton, driver, extentReportHelper);
        tempElement.Click();
        extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ContactSubmitButton.Tag}");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;

        
        //Validate Error Messages when field is blank
        foreach(LocatorField item in validationHelper.LocatorFields)
        {
            tempElement = TestHelper.LocateControl(item, driver, extentReportHelper);
           
            if (!(tempElement.Text==item.ExpectedErrorMessageWhenBlank))
            {
                extentReportHelper.SetTestStatusFail($"Error message not as expected." +
                                $"Expected : {item.ExpectedErrorMessageWhenBlank}." +
                                $"Actual : {tempElement.Text}");
                Assert.Fail();
            }
            else
            {
                extentReportHelper.SetStepStatusPass("Error Message Found : " + tempElement.Text);
            }

        }

        //Input Data
        foreach(LocatorField item in validationHelper.TestDataInputs)
        {
            tempElement = TestHelper.LocateControl(item, driver, extentReportHelper);
            if (item.Tag.ToLower().Contains("email"))
            {
                tempElement.Clear();
                extentReportHelper.SetStepStatusPass($"Writing {item.WrongTestData} on {item.Tag}");
                tempElement.SendKeys(item.WrongTestData);                
                LocatorField emailItem = validationHelper.LocatorFields.First(i => i.Tag.ToLower().Contains("email"));
                IWebElement emailElement = TestHelper.LocateControl(emailItem, driver, extentReportHelper);
                if (!(emailElement.Text==emailItem.ExpectedErrorMessageWhenFormatIsWrong))
                {
                    extentReportHelper.SetTestStatusFail($"Error message not as expected." +
                                    $"Expected : {emailItem.ExpectedErrorMessageWhenFormatIsWrong}." +
                                    $"Actual : {emailElement.Text}");
                    Assert.Fail();
                }
                else
                {
                    extentReportHelper.SetStepStatusPass($"Error Message Found : {emailElement.Text}");
                }
            }
            extentReportHelper.SetStepStatusPass($"Writing {item.CorrectTestData} on {item.Tag}");
            tempElement.Clear();
            tempElement.SendKeys(item.CorrectTestData);
        }


        foreach (LocatorField item in validationHelper.LocatorFields)
        {
            if (TestHelper.IsElementExisting(item, driver))
            {
                extentReportHelper.SetStepStatusPass($"Failed Test: {item.Tag} should not have any error message");
                Assert.Fail();
            }
            else 
            {
                extentReportHelper.SetStepStatusPass("No data found on error section");
            }
        }


      
        extentReportHelper.SetStepStatusPass("Passed");
        Assert.Pass();
    }

   
    [Test]
    public void TestCase2_SubmitContact_AllFieldsFilled()
    {}

    [Test]
    public void TestCase1_002_SubmitContact_ForenameMissing()
    {} 

    [Test]
    public void TestCase1_003_SubmitContact_EmailMissing()
    {}
    [Test]
    public void TestCase1_004_SubmitContact_MessageMissing()
    {}

    [SetUp]
    public void Setup()
    {
        validationHelper = JsonSerializer.Deserialize<ValidationHelper>(File.ReadAllText("appsettings.json"));
        extentReportHelper  = new ReportHelper();
        driver = new ChromeDriver();
    }

    [TearDown]
    public void Close()
    {
        driver.Close();
        driver.Quit();
        extentReportHelper.Close();
    }
    // [OneTimeTearDown]
    //     public void CloseAll()
    //     {
    //         try
    //         {
    //             extentReportHelper.Close();
    //         }
    //         catch (Exception e)
    //         {
    //             throw (e);
    //         }
    //     }
}
