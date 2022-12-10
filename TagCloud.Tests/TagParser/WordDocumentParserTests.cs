using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloud.Parser;
using TagCloud.Parser.ParsingConfig;

namespace TagCloud.Tests.TagParser;

[TestFixture]
public class WordDocumentParserTests
{
    [TearDown]
    public static void TearDown()
    {
        var workDir = TestContext.CurrentContext.WorkDirectory;
        var testName = TestContext.CurrentContext.WorkerId + ".docx";
        var path = Path.Combine(workDir, testName);
        File.Delete(path);
    }
    
    [TestCaseSource(typeof(TagParserDataProvider), nameof(TagParserDataProvider.GetUniqueLowercaseWords))]
    public static void Should_Parse_UniqueWords(string[] data)
    {
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }

    [TestCaseSource(typeof(TagParserDataProvider), nameof(TagParserDataProvider.GetUniqueMixedCaseWords))]
    public static void Should_ParseAsLowercase_UniqueWords(string[] data)
    {
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [TestCaseSource(typeof(TagParserDataProvider), nameof(TagParserDataProvider.GetRepeatingLowercaseWords))]
    public static void Should_Parse_WithRepeats(string[] data)
    {
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [TestCaseSource(typeof(TagParserDataProvider), nameof(TagParserDataProvider.GetRepeatingMixedCaseWords))]
    public static void Should_ParseAsLowercase_WithRepeats(string[] data)
    {
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }

    private static ITagParser GetParser()
    {
        var config = new Mock<IParsingConfig>();
        config.Setup(c => c.IsWordExcluded(It.IsAny<string>())).Returns(false);

        return new WordDocumentParser(config.Object);
    }

    private static string WriteData(string[] data)
    {
        var paragraphs = data
            .Select(s => new Paragraph(new Run(new Text(s))))
            .ToList();

        var workDir = TestContext.CurrentContext.WorkDirectory;
        var testName = TestContext.CurrentContext.WorkerId + ".docx";
        var path = Path.Combine(workDir, testName);

        using var document = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
        document.AddMainDocumentPart().Document = new Document(new Body(paragraphs));
        document.Save();

        return path;
    }

    private static TagMap GetExpected(string[] data)
    {
        var result = new TagMap();
        foreach (var line in data) result.AddWord(line.ToLower());

        return result;
    }
}