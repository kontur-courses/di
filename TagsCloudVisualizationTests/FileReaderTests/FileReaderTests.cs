using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualizationTests.FileReaderTests;

[TestFixture]
public class FileReaderTests
{
    [Test]
    public void ReadText_FileNotExist_ThrowFileNotFoundException()
    {
        var reader = new FileReader();
        var action = () => reader.ReadText(@"..\..\..\FileReaderData\NotExistFile.txt");
        action.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ReadText_ExtensionNotSupported_ThrowArgumentException()
    {
        var reader = new FileReader();
        var action = () => reader.ReadText(@"..\..\..\FileReaderData\NotSupportedExtension.pdf");
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ReadText_TxtFile_ReturnTextInFile()
    {
        var reader = new FileReader();
        var result = reader.ReadText(@"..\..\..\FileReaderData\TxtFile.txt");
        result.Should().Be("txt\r\nfile\r\ndata");
    }

    [Test]
    public void ReadText_DocFile_ReturnsTextInFile()
    {
        var reader = new FileReader();
        var result = reader.ReadText(@"..\..\..\FileReaderData\DocFile.doc");
        result.Should().Be("doc file data");
    }

    [Test]
    public void ReadText_DocxFile_ReturnsTextInFile()
    {
        var reader = new FileReader();
        var result = reader.ReadText(@"..\..\..\FileReaderData\DocxFile.docx");
        result.Should().Be("docx file data");
    }

    [TestCase("Empty.docx", " ", TestName = "empty docx file")]
    [TestCase("Empty.txt", "", TestName = "empty txt file")]
    [TestCase("Empty.doc", "", TestName = "empty doc file")]
    public void ReadText_EmptyTxtFile_ReturnsEmptyStringOrSpecialSymbol(string fileName, string exceptedResult)
    {
        var reader = new FileReader();
        var result = reader.ReadText($@"..\..\..\FileReaderData\{fileName}");
        result.Should().Be(exceptedResult);
    }
}
