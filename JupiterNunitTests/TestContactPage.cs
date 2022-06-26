using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JupiterTestHelper;
using System.Reflection;
using System.Text.Json;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace JupiterNunitTests;



[TestFixture]
public class TestContactPage:TestBase
{
    protected override string FileName
    {
        get;
        set;
    } = "ContactTest.html";
    [Test]
    public void TestCase1_ContactPage_ValidateErrorMessages()
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

        tempElement = TestHelper.LocateControl(validationHelper.ContactSubmitButton, driver, extentReportHelper);
        tempElement.Click();
        extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ContactSubmitButton.Tag}");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);


        //Validate Error Messages when field is blank
        foreach (LocatorField item in validationHelper.LocatorFields)
        {
            tempElement = TestHelper.LocateControl(item, driver, extentReportHelper);

            if (!(tempElement.Text == item.ExpectedErrorMessageWhenBlank))
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
        foreach (LocatorField item in validationHelper.TestDataInputs)
        {
            tempElement = TestHelper.LocateControl(item, driver, extentReportHelper);
            if (item.Tag.ToLower().Contains("email"))
            {
                tempElement.Clear();
                extentReportHelper.SetStepStatusPass($"Writing {item.WrongTestData} on {item.Tag}");
                tempElement.SendKeys(item.WrongTestData);
                LocatorField emailItem = validationHelper.LocatorFields.First(i => i.Tag.ToLower().Contains("email"));
                IWebElement emailElement = TestHelper.LocateControl(emailItem, driver, extentReportHelper);
                if (!(emailElement.Text == emailItem.ExpectedErrorMessageWhenFormatIsWrong))
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
                extentReportHelper.SetStepStatusPass($"No error message found on {item.Tag}");
            }
        }



        extentReportHelper.SetStepStatusPass("Passed");
        Assert.Pass();
    }

    
    [Test]
    [Repeat(5)]
    public void TestCase2_ContactPage_CheckSuccessfulSubmission_MandatoryFieldsPopulated()
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

       // for (int i = 0; i < validationHelper.NumberOfRunsForSubmission; i++)
       // {
            foreach (LocatorField item in validationHelper.TestDataInputs)
            {
                tempElement = TestHelper.LocateControl(item, driver, extentReportHelper);
                extentReportHelper.SetStepStatusPass($"Writing {item.CorrectTestData} on {item.Tag}");
                tempElement.Clear();
                tempElement.SendKeys(item.CorrectTestData);

            }
            tempElement = TestHelper.LocateControl(validationHelper.ContactSubmitButton, driver, extentReportHelper);
            tempElement.Click();
            extentReportHelper.SetStepStatusPass($"Clicked {validationHelper.ContactSubmitButton.Tag}");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Stopwatch stopwatch = Stopwatch.StartNew();
            do
            {
                
                if(stopwatch.Elapsed>TimeSpan.FromSeconds(validationHelper.TimeToWaitForSuccesfulSubmission_InSeconds))
                {
                    extentReportHelper.SetStepStatusPass($"Test FAILED - Reached Time Out : Waited for Successful Submission for more than {validationHelper.TimeToWaitForSuccesfulSubmission_InSeconds} seconds.");
                    Assert.Fail($"Reached Time Out : Waited for Successful Submission for more than {validationHelper.TimeToWaitForSuccesfulSubmission_InSeconds} seconds.");
                    break;

                }


            } while (!TestHelper.IsElementExisting(validationHelper.ModalSubmissionProgress, driver));

            if (TestHelper.IsElementExisting(validationHelper.SuccessfulSubmission, driver))
            {
                extentReportHelper.SetStepStatusPass("Passed");
                Assert.Pass();

             }
       // }


    }

    //public static void WaitForModal(IWebDriver driver, ValidationHelper validationHelper)
    //{
    //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
    //    wait.Until<IWebDriver>((d) =>
    //    {
    //        if (!TestHelper.IsElementExisting(validationHelper.ModalSubmissionProgress,driver))
    //        {
    //            return driver;
    //        }
    //        return null;
    //    });
    //}

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
