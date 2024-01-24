using FluentAssertions;
using Spire.Doc;
using TagsCloud;

namespace TagsCloudTests;

public class FileReaderTests
{
    private Document documentWriter;
    private FileReader sut;
    public string FileName;
    
    [SetUp]
    public void SetUp()
    {
        documentWriter = new Document();
        var parsers = new List<IParser>() { new DocParser(), new TxtParser(), new DocxParser() };
        sut = new FileReader(parsers);
    }
    
    [TearDown]
    public void TearDown()
    {
        documentWriter.Close();
        if (File.Exists(FileName))
            File.Delete(FileName);
    }

    [TestCaseSource(typeof(FileReaderTestData), nameof(FileReaderTestData.ConstructSomeFileTypes))]
    public void FileReader_GetWordList(string fileName, FileFormat format, string text, string[] expected)
    {
        FileName = fileName;
        File.Create(FileName).Close();
        var paragraph1 = documentWriter.AddSection().AddParagraph();
        paragraph1.Text = text;
        documentWriter.SaveToFile(FileName, FileFormat.Txt);
        documentWriter.Close();
        var wordList = sut.GetWords(FileName).ToArray()[11..];
        wordList.Should().BeEquivalentTo(expected);

    }
    
    [Test]
    public void FileReader_WhenReadEmptyFile_ShouldBeNotTrow()
    {
        FileName = "Empty.txt";
        var paragraph1 = documentWriter.AddSection();
        File.Create(FileName).Close();
        documentWriter.SaveToFile(FileName, FileFormat.Txt);
        documentWriter.Close();
        var action = () =>
        {
            var wordList = sut.GetWords(FileName);
        };
        action.Should().NotThrow();
    }
    [Test]
    public void FileReader_WhenFileTypeParserDoesNotExist_ShouldBeTrow()
    {
        FileName = "NotFound.pdf";
        File.Create(FileName).Close();
        documentWriter.SaveToFile(FileName, FileFormat.PDF);
        var action = () =>
        {
            var wordList = sut.GetWords(FileName);
        };
        action.Should().Throw<ArgumentException>($"not found parser for {nameof(FileFormat.PDF)}");
    }
}