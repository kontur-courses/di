using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;
using TagCloudTest.FileInputStream.Infrastructure;

namespace TagCloudTest.FileInputStream;

public class FromFileInputWordsStreamTest
{
    private readonly string textWithNewLines = "word1" + Environment.NewLine + "word2" + Environment.NewLine + "word3";
    private string path = "test.txt";
    private FromFileInputWordsStream sut = new(new NewLineTextSplitter());

    [OneTimeSetUp]
    public void StartTests()
    {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "test.txt";
        File.Create(path);
    }
        
    [Test]
    public void GetAllWordsFromStream_ShouldReturnEmptyList_WhenNoWords()
    {
        sut.GetAllWordsFromStream(GetContext("")).Should().BeEmpty();
    }

    [Test]
    public void GetAllWordsFromStream_ShouldThrowFileNotFoundException_WhenIncorrectFilename()
    {
        Action creatingStreamWithIncorrectFile = () =>
            sut.GetAllWordsFromStream(GetContext("", "incorrectPath"));
        creatingStreamWithIncorrectFile.Should().Throw<FileNotFoundException>();
    }
        
    [Test]
    public void GetAllWordsFromStream_ShouldThrowException_WhenIncorrectFileType()
    {
        Action creatingStreamWithIncorrectFileType = () =>
            sut.GetAllWordsFromStream(GetContext("", filetype: "docx"));
        creatingStreamWithIncorrectFileType.Should().Throw<Exception>();
    }

    [Test]
    public void GetAllWordsFromStream_ShouldReturnAllWordsFromFile()
    {
        var words = sut.GetAllWordsFromStream(GetContext(textWithNewLines));
        words.Count.Should().Be(3);
    }
    
        
    [OneTimeTearDown]
    public void StopTests()
    {
        try
        {
            File.Delete(path);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private FromFileStreamContext GetContext(string textInFile, string? filepath = null, string filetype = "txt")
    {
        filepath ??= path;
        return new FromFileStreamContext(filepath, new FileEncoderСheater(textInFile, true, filetype));
    }
}