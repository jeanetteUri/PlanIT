using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace JupiterNunitTests;

[TestFixture]
public class TestContactPage
{
    String test_url = "https://jupiter.cloud.planittesting.com/#/";
    IWebDriver driver;
    ReportHelper extentReportHelper;

    [Test]
    public void TestCase1_001_ContactPage_SubmitContact_NoFieldsFilled()
    {
        IWebElement contact;
        IWebElement submit ;
        driver.Manage().Window.Maximize();
        extentReportHelper.CreateTest("TestCase1_001_ContactPage_SubmitContact_NoFieldsFilled");
        extentReportHelper.SetStepStatusPass("Chrome Browser opened"); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        driver.Url = test_url;
        try{
            contact = driver.FindElement(By.Id("nav-contact"));
        }
        catch{
            throw new NoSuchElementException("Contact Element Not found");
            extentReportHelper.SetTestStatusFail("Contact Element Not found");
            Assert.Fail("Contact Element Not found");
        }
       
        contact.Click();
        extentReportHelper.SetStepStatusPass("Contact Page clicked");
        submit = driver.FindElement(By.ClassName("btn-primary"));
        //submit = driver.FindElement(By.LinkText("Submit"));
        extentReportHelper.SetStepStatusPass("Submit button found");
        submit.Click();
        extentReportHelper.SetStepStatusPass("Clicked Submit"); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        IWebElement errorForename = driver.FindElement(By.ClassName("error"));
        string testError = errorForename.Text;
        extentReportHelper.SetStepStatusPass("Error Message Found : " + testError);
       if(testError.Contains("Forename is required"))
       {
        extentReportHelper.SetStepStatusPass("Passed");
        Assert.Pass();
       }
       else
       {
        Assert.Fail();
       }
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
