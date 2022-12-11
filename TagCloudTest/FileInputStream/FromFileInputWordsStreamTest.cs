using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;
using TagCloudTest.FileInputStream.Infrastructure;

namespace TagCloudTest.FileInputStream;

public class FromFileInputWordsStreamTest
{
    private string path = "test.txt";
    private readonly string textWithNewLines = "word1" + Environment.NewLine + "word2" + Environment.NewLine + "word3";

    [OneTimeSetUp]
    public void StartTests()
    {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "test.txt";
        File.Create(path);
    }
        
    [Test]
    public void Next_ShouldReturnFalse_WhenNoNextWord()
    {
        var sut = CreateFileInputStream("");
        sut.MoveNext().Should().BeFalse();
    }

    [Test]
    public void Creation_ShouldThrowFileNotFoundException_WhenIncorrectFilename()
    {
        Action creatingStreamWithIncorrectFile = () =>
        {
            var _ = new FromFileInputWordsStream()
                .OpenFile("incorrect/filename", new FileEncoderСheater("", false, ""))
                .UseSplitter(new NewLineTextSplitter());
        };
        creatingStreamWithIncorrectFile.Should().Throw<FileNotFoundException>();
    }
        
    [Test]
    public void Creation_ShouldThrowIncorrectFileTypeException_WhenIncorrectFileType()
    {
        Action creatingStreamWithIncorrectFileType = () =>
            CreateFileInputStream("", fileType:".docx");
        creatingStreamWithIncorrectFileType.Should().Throw<Exception>();
    }
        
    [Test]
    public void GetWord_ShouldThrowIncorrectCallException_WhenNoFirstCallMoveNext()
    {
        Action gettingWordWithoutCallMoveNext = () =>    
            CreateFileInputStream("word").GetWord();
        gettingWordWithoutCallMoveNext.Should().Throw<Exception>();
    }

    [Test]
    public void GetWord_ShouldThrowEndOfStreamException_WhenNoMoreWords()
    {
        var sut = CreateFileInputStream("word");
        sut.MoveNext();
        sut.MoveNext();
        Action gettingWordAfterStreamEnd = () => sut.GetWord();
        gettingWordAfterStreamEnd.Should().Throw<EndOfStreamException>();
    }

    [Test]
    public void Next_ShouldReturnTrue_WhenHasMoreWords()
    {
        var sut = CreateFileInputStream(textWithNewLines);
        sut.MoveNext().Should().BeTrue();
    }

    [Test]
    public void Next_ShouldMoveIterator()
    {
        var sut = CreateFileInputStream(textWithNewLines);
        sut.MoveNext().Should().BeTrue();
        sut.MoveNext().Should().BeTrue();
        sut.MoveNext().Should().BeTrue();
        sut.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetWord_ShouldReturnSameWords_WhenNoCollMoveNextBetweenThem()
    {
        var sut = CreateFileInputStream(textWithNewLines);
        sut.MoveNext();
        var w1 = sut.GetWord();
        sut.GetWord().Should().Be(w1);
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
        
    private IInputWordsStream CreateFileInputStream(string text, bool existsFile = true, string fileType = "txt")
    {
        return new FromFileInputWordsStream()
            .OpenFile(path, new FileEncoderСheater(text, existsFile, fileType))
            .UseSplitter(new NewLineTextSplitter());
    }
}