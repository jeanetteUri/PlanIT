namespace JupiterTestHelper;
public class LocatorField
{
    public string ById { get; set; }
    public string ExpectedErrorMessageWhenBlank { get; set; }
    public string ExpectedErrorMessageWhenFormatIsWrong { get; set; }
    public string ByClass { get; set; }
    public string Tag { get; set; }

    public string CorrectTestData { get; set; }
    public string WrongTestData { get; set; }

}
