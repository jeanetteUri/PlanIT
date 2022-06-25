using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JupiterNunitTests;

public class JupiterNunits
{
    String test_url = "https://jupiter.cloud.planittesting.com/#/";
    String test_url_contact = "https://jupiter.cloud.planittesting.com/#/contact";
    IWebDriver driver;
   //Helper extentReportHelper;

// [OneTimeSetUp]
//         public void SetUpReporter()
//         {
//             extentReportHelper = new Helper();
//         }

    [SetUp]
    public void Setup()
    {
        //extentReportHelper  = new Helper();
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
      
       
    }

    [Test]
    public void TestForenameReturnMessage()
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        //extentReportHelper.CreateTest("TestForenameReturnMessage");
        driver.Url = test_url_contact;
        IWebElement submit = driver.FindElement(By.LinkText("Submit"));
        //extentReportHelper.SetStepStatusPass("Submit Found");
        submit.Click();
        //extentReportHelper.SetStepStatusPass("Clicked Submit");
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2) ;
        IWebElement errorForename = driver.FindElement(By.ClassName("error"));
       string testError = errorForename.Text;
      // extentReportHelper.SetStepStatusPass("Error Message Found");
       

       if(testError.Contains("Forename is required"))
       {
        Assert.Pass();
        //extentReportHelper.SetStepStatusPass("Passed");
       }
       else
       {
        Assert.Fail();
       }
       Close();
    }


     
    [TearDown]
    public void Close()
    {
        //extentReportHelper.Close();
        driver.Close();
        driver.Quit();
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
