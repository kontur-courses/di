using System.Drawing;

namespace TagCloudTests.TestData;

public class ColorConverterTestData
{
    public static IEnumerable<TestCaseData> RightCases
    {
        get
        {
            yield return new TestCaseData("#FFFFFF").Returns(Color.FromArgb(255, 255, 255));
            yield return new TestCaseData("#FF0000").Returns(Color.FromArgb(255, 0, 0));
            yield return new TestCaseData("#00FF00").Returns(Color.FromArgb(0, 255, 0));
            yield return new TestCaseData("#0000FF").Returns(Color.FromArgb(0, 0, 255));
            yield return new TestCaseData("#808080").Returns(Color.FromArgb(128, 128, 128));
            yield return new TestCaseData("#123456").Returns(Color.FromArgb(18, 52, 86));
        }
    }
    
    public static IEnumerable<TestCaseData> WrongCases
    {
        get
        {
            yield return new TestCaseData("FFFFFF").SetName("WithoutLeadingHash");
            yield return new TestCaseData("#12345").SetName("ExcessiveLenght");
            yield return new TestCaseData("#1234567").SetName("InsufficientLenght");
            yield return new TestCaseData("#$%^").SetName("UnrecognizedCharacters");
            yield return new TestCaseData("#GHJKOP").SetName("OnlyHexDigits");
        }
    }
}