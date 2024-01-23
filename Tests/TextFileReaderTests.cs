using TagsCloudPainter.FileReader;

namespace TagsCloudPainterTests;

[TestFixture]
public class TextFileReaderTests
{
    [SetUp]
    public void Setup()
    {
        fileReader = new TextFileReader();
    }

    private TextFileReader fileReader;

    private static IEnumerable<TestCaseData> ReadTextFiles => new[]
    {
        new TestCaseData(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.txt")
            .Returns("Товарищи! постоянное информационно-пропагандистское обеспечение нашей " +
                     $"деятельности играет важную роль в формировании форм развития.{Environment.NewLine}{Environment.NewLine}" +
                     "Значимость этих проблем настолько очевидна, что укрепление и развитие.")
            .SetName("WhenPassedTxtFile"),
        new TestCaseData(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.docx")
            .Returns("Товарищи! постоянное информационно-пропагандистское обеспечение нашей " +
                     $"деятельности играет важную роль в формировании форм развития.{Environment.NewLine}{Environment.NewLine}" +
                     "Значимость этих проблем настолько очевидна, что укрепление и развитие.")
            .SetName("WhenPassedDocxFile")
    };

    [TestCaseSource(nameof(ReadTextFiles))]
    public string ReadFile_ShouldReturnFileText(string path)
    {
        return fileReader.ReadFile(path);
    }

    [Test]
    public void ReadFile_ThrowsFileNotFoundExceptio_WhenPassedNonexistentPath()
    {
        Assert.Throws<FileNotFoundException>(() => fileReader.ReadFile(""));
    }
}