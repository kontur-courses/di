using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.FileReaders;

namespace TagsCloudVisualizationTests.FileReaderTests;

[TestFixture]
public class FileReaderTests
{
    private IFileReader[] readers = new IFileReader[] {new DocReader(), new DocxReader(), new TxtReader()};

    [Test]
    public void ReadText_FileNotExist_ThrowFileNotFoundException()
    {
        var reader = GetReader(@"..\..\..\FileReaderData\NotExistFile.txt");
        var action = () => reader.ReadText(@"..\..\..\FileReaderData\NotExistFile.txt");
        action.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ReadText_ExtensionNotSupported_ThrowArgumentException()
    {
        var action = () => GetReader(@"..\..\..\FileReaderData\NotSupportedExtension.pdf");
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ReadText_TxtFile_ReturnTextInFile()
    {
        var reader = GetReader(@"..\..\..\FileReaderData\TxtFile.txt");
        var result = reader.ReadText(@"..\..\..\FileReaderData\TxtFile.txt");
        result.Should().Be("txt\r\nfile\r\ndata");
    }

    [Test]
    public void ReadText_DocFile_ReturnsTextInFile()
    {
        var reader = GetReader(@"..\..\..\FileReaderData\DocFile.doc");
        var result = reader.ReadText(@"..\..\..\FileReaderData\DocFile.doc");
        result.Should().Be("doc file data");
    }

    [Test]
    public void ReadText_DocxFile_ReturnsTextInFile()
    {
        var reader = GetReader(@"..\..\..\FileReaderData\DocxFile.docx");
        var result = reader.ReadText(@"..\..\..\FileReaderData\DocxFile.docx");
        result.Should().Be("docx file data");
    }

    [TestCase("Empty.docx", " ", TestName = "empty docx file")]
    [TestCase("Empty.txt", "", TestName = "empty txt file")]
    [TestCase("Empty.doc", "", TestName = "empty doc file")]
    public void ReadText_EmptyTxtFile_ReturnsEmptyStringOrSpecialSymbol(string fileName, string exceptedResult)
    {
        var reader = GetReader($@"..\..\..\FileReaderData\{fileName}");
        var result = reader.ReadText($@"..\..\..\FileReaderData\{fileName}");
        result.Should().Be(exceptedResult);
    }

    private IFileReader GetReader(string path)
    {
        var reader = readers.Where(r => r.CanRead(path)).FirstOrDefault();
        return reader is null
            ? throw new ArgumentException($"Can't read file with path {path}")
            : reader;
    }
}