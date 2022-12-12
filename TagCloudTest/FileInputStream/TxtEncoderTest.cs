using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.WordPreprocessorDriver.InputStream;

namespace TagCloudTest.FileInputStream;

public class TxtEncoderTest
{
    private static string? _filePath;

    [OneTimeSetUp]
    public void StartTests()
    {
        _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "test.txt";
    }
        
    [Test]
    public void GetText_ShouldReturnTextFromTxtFile_WhenOnlyOneLineInFile()
    {
        var text = "word1 word2";
        GetText_Helper(text, CreateFileWithText(text));
    }
        
    [Test]
    public void GetText_ShouldReturnTextFromTxtFile_WhenManyLinesInFile()
    {
        var text = "text1" + Environment.NewLine + "text2";
        GetText_Helper(text, CreateFileWithText(text));
    }

    [TearDown]
    public void OnTestStop()
    {
        try
        {
            File.Delete(_filePath!);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static string CreateFileWithText(string text)
    {
        File.WriteAllText(_filePath!, text);
        return _filePath!;
    }

    private static void GetText_Helper(string expectedText, string filePath)
    {
        var sut = new TxtEncoder();
        sut.GetText(filePath).Should().Be(expectedText);
    }
}