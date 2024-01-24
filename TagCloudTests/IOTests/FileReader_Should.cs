using Microsoft.VisualStudio.TestPlatform.Utilities;
using TagCloud.FileReader;

namespace TagCloudTests;

[TestFixture]
public class FileReader_Should
{
    private IFileReader sut = new TxtReader();
    private const string inputPath = "test.txt";
    private string text = $"one{Environment.NewLine}two{Environment.NewLine}three{Environment.NewLine}";

    [SetUp]
    public void SetUp()
    {
        using var fileStream = File.Open(inputPath, FileMode.Create);
        using var writer = new StreamWriter(fileStream);

        writer.Write(text);
    }

    [Test]
    public void ReadWordsFromTxt()
    {
        var expected = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

        var result = sut.ReadLines(inputPath);

        result.Should().BeEquivalentTo(expected);

        File.Delete(inputPath);
    }
}