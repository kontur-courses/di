using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.FileReader;

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
        result.Should().Be("doc\rfile\rdata\r");
    }

    [Test]
    public void ReadText_DocxFile_ReturnsTextInFile()
    {
        var reader = new FileReader();
        var result = reader.ReadText(@"..\..\..\FileReaderData\DocxFile.docx");
        result.Should().Be("docx\rfile\rdata\r");
    }
}
