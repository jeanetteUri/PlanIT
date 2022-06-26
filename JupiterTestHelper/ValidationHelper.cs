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
    public LocatorField ShopPageFromHomePage { get; set; }
    public int TimeToWaitForSuccesfulSubmission_InSeconds { get; set; }
    public LocatorField Product7_ValentineBear { get; set; }
    public LocatorField Product4_FluffyBunny { get; set; }
    public LocatorField Product2_StuffedFrog { get; set; }
    public LocatorField ShopPageCartCount { get; set; }

}


