using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualizationTests.FileReaderTests;

[TestFixture]
public class FileReaderTests
{
    private IFileReaderFactory factory;

    [SetUp]
    public void SetUp()
    {
        factory = new FileReaderFactory();
    }

    [Test]
    public void ReadText_FileNotExist_ThrowFileNotFoundException()
    {
        var action = () => factory.Create(@"..\..\..\FileReaderData\NotExistFile.txt");
        action.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ReadText_ExtensionNotSupported_ThrowArgumentException()
    {
        var action = () => factory.Create(@"..\..\..\FileReaderData\NotSupportedExtension.pdf");
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ReadText_TxtFile_ReturnTextInFile()
    {
        var reader = factory.Create(@"..\..\..\FileReaderData\TxtFile.txt");
        var result = reader.ReadText();
        result.Should().Be("txt\r\nfile\r\ndata");
    }

    [Test]
    public void ReadText_DocFile_ReturnsTextInFile()
    {
        var reader = factory.Create(@"..\..\..\FileReaderData\DocFile.doc");
        var result = reader.ReadText();
        result.Should().Be("doc file data");
    }

    [Test]
    public void ReadText_DocxFile_ReturnsTextInFile()
    {
        var reader = factory.Create(@"..\..\..\FileReaderData\DocxFile.docx");
        var result = reader.ReadText();
        result.Should().Be("docx file data");
    }

    [TestCase("Empty.docx", " ", TestName = "empty docx file")]
    [TestCase("Empty.txt", "", TestName = "empty txt file")]
    [TestCase("Empty.doc", "", TestName = "empty doc file")]
    public void ReadText_EmptyTxtFile_ReturnsEmptyStringOrSpecialSymbol(string fileName, string exceptedResult)
    {
        var reader = factory.Create($@"..\..\..\FileReaderData\{fileName}");
        var result = reader.ReadText();
        result.Should().Be(exceptedResult);
    }
}