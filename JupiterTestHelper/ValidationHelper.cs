using System.Net.Mail;

namespace JupiterTestHelper;

public class ValidationHelper
{
    public bool IsValidEmail(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
 
    public LocatorField[]? LocatorFields{get; set;}
    public string? URLUnderTest { get; set; }
    public LocatorField? ContactSubmitButton { get; set; }
    public LocatorField? ContactPageFromHomePage { get; set; }
    public LocatorField[]? TestDataInputs { get; set; }
    public int NumberOfRunsForSubmission { get; set; }
    public LocatorField SuccessfulSubmission { get; set; }
    public LocatorField ModalSubmissionProgress { get; set; }
    public int TimeToWaitForSuccesfulSubmission_InSeconds { get; set; }
}


