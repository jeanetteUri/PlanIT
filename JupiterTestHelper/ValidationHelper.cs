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

    public ErrorMessage[] ErrorMessages{get; set;}
}


