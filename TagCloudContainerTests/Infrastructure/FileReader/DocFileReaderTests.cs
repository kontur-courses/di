using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.FileReader;

namespace TagCloudTests.Infrastructure.FileReader;

internal class DocFileReaderTests
{
    private const string FileName = $"{nameof(DocFileReader)}Test.txt";
    private readonly DocFileReader sut = new();

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var paragraphs = new[] { "firstline", "secondline", "thirdline", "" };
        using var document = WordprocessingDocument.Create(FileName, WordprocessingDocumentType.Document, true);
        var mainPart = document.AddMainDocumentPart();
        mainPart.Document = new Document();
        var body = new Body();

        foreach (var paragraph in paragraphs)
        {
            var run = new Run();
            run.Append(new Text(paragraph));
            body.Append(new Paragraph(run));
        }

        mainPart.Document.Append(body);
    }

    [Test]
    public void GetLines_ShouldReturnCorrectLinesEnumerable()
    {
        var expected = new[] { "firstline", "secondline", "thirdline", "" };

        var actual = sut.GetLines(FileName);

        actual.Should().BeEquivalentTo(expected);
    }
}