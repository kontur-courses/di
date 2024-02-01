using TagsCloudPainter.FileReader;

namespace TagsCloudPainterTests;

[TestFixture]
public class TextFileReaderTests
{
    [SetUp]
    public void Setup()
    {
        var fileReaders = new List<IFileReader> { new TxtFileReader(), new DocFileReader() };
        textFileReader = new TextFileReader(fileReaders);
    }

    private TextFileReader textFileReader;

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
            .SetName("WhenPassedDocxFile"),
        new TestCaseData(@$"{Environment.CurrentDirectory}..\..\..\..\TextFiles\testFile.doc")
            .Returns("Товарищи! постоянное информационно-пропагандистское обеспечение нашей " +
                     $"деятельности играет важную роль в формировании форм развития.{Environment.NewLine}{Environment.NewLine}" +
                     "Значимость этих проблем настолько очевидна, что укрепление и развитие.")
            .SetName("WhenPassedDocFile")
    };

    [TestCaseSource(nameof(ReadTextFiles))]
    public string ReadFile_ShouldReturnFileText(string path)
    {
        return textFileReader.ReadFile(path);
    }

    [Test]
    public void ReadFile_ThrowsFileNotFoundExceptio_WhenPassedNonexistentPath()
    {
        Assert.Throws<FileNotFoundException>(() => textFileReader.ReadFile(""));
    }
}