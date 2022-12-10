using FluentAssertions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Parser;
using TagCloud.Parser.ParsingConfig;

namespace TagCloud.Tests.TagParser;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class PlainTextParserTests
{
    [TearDown]
    public static void TearDown()
    {
        var workDir = TestContext.CurrentContext.WorkDirectory;
        var testName = TestContext.CurrentContext.WorkerId;
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

        return new PlainTextParser(config.Object);
    }

    private static string WriteData(string[] data)
    {
        var workDir = TestContext.CurrentContext.WorkDirectory;
        var testName = TestContext.CurrentContext.WorkerId;
        var path = Path.Combine(workDir, testName);

        File.WriteAllLines(path, data);

        return path;
    }

    private static TagMap GetExpected(string[] data)
    {
        var result = new TagMap();
        foreach (var line in data) result.AddWord(line.ToLower());

        return result;
    }
}