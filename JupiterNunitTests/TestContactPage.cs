using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JupiterTestHelper;
namespace JupiterNunitTests;
using  System.Text.Json;

[TestFixture]
public class TestContactPage
{
    String test_url = "https://jupiter.cloud.planittesting.com/#/";
    IWebDriver driver;
    ReportHelper extentReportHelper;
    ValidationHelper validationHelper;


    [Test]
    public void TestCase1_001_ContactPage_SubmitContact_NoFieldsFilled()
    {

        IWebElement tempElement;
        driver.Manage().Window.Maximize();
        extentReportHelper.CreateTest("TestCase1_001_ContactPage_SubmitContact_NoFieldsFilled");
        extentReportHelper.SetStepStatusPass("Chrome Browser opened"); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        driver.Url = test_url;
        try{
            tempElement = driver.FindElement(By.Id("nav-contact"));
        }
        catch{
            throw new NoSuchElementException("Contact Element Not found");
            extentReportHelper.SetTestStatusFail("Contact Element Not found");
            Assert.Fail("Contact Element Not found");
        }
       
        tempElement.Click();
        extentReportHelper.SetStepStatusPass("Contact Page clicked");
        tempElement = driver.FindElement(By.ClassName("btn-primary"));
        //submit = driver.FindElement(By.LinkText("Submit"));
        extentReportHelper.SetStepStatusPass("Submit button found");
        tempElement.Click();
        extentReportHelper.SetStepStatusPass("Clicked Submit"); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        IWebElement errorForename = driver.FindElement(By.Id("forename-err"));
        string testError = errorForename.Text;
        extentReportHelper.SetStepStatusPass("Error Message Found : " + testError);
       if(!testError.Contains("Forename is required"))
       {
        extentReportHelper.SetTestStatusFail("Wrong Error Message on Missing Forename");
        Assert.Fail();
       }
       errorForename = driver.FindElement(By.Id("email-err"));
        testError = errorForename.Text;
        extentReportHelper.SetStepStatusPass("Error Message Found : " + testError);
         if(!testError.Contains("Please enter a valid email"))
       {
        extentReportHelper.SetTestStatusFail("Wrong Error Message on Missing Email");
        Assert.Fail();
       }

         errorForename = driver.FindElement(By.Id("message-err"));
        testError = errorForename.Text;
        extentReportHelper.SetStepStatusPass("Error Message Found : " + testError);
         if(!testError.Contains("Message is required"))
       {
        extentReportHelper.SetTestStatusFail("Wrong Error Message on Missing Email");
        Assert.Fail();
       }

       tempElement = driver.FindElement(By.Id("forename"));
       tempElement.SendKeys("Test");
       tempElement = driver.FindElement(By.Id("email"));
       tempElement.SendKeys("Test");
       tempElement = driver.FindElement(By.Id("message"));
       tempElement.SendKeys("Test");

        tempElement = driver.FindElement(By.ClassName("btn-primary"));
        //submit = driver.FindElement(By.LinkText("Submit"));
        extentReportHelper.SetStepStatusPass("Submit button found");
        tempElement.Click();

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
