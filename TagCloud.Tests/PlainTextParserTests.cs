﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloud.Parser;
using TagCloud.Parser.ParsingConfig;

namespace TagCloud.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class PlainTextParserTests
{
    [Test]
    public static void Should_Parse_UniqueWords()
    {
        var data = new[]
        {
            "каждый",
            "охотник",
            "желает",
            "знать",
            "где",
            "сидит",
            "фазан"
        };
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public static void Should_ParseAsLowercase_UniqueWords()
    {
        var data = new[]
        {
            "кАжДый",
            "охоТник",
            "ЖЕЛАЕТ",
            "ЗНаТЬ",
            "Где",
            "СИДИт",
            "фазан"
        };
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public static void Should_Parse_WithRepeats()
    {
        var data = new[]
        {
            "каждый",
            "каждый",
            "каждый",
            "охотник",
            "желает",
            "желает",
            "где",
            "где",
            "знать",
            "фазан",
            "фазан",
            "сидит",
            "каждый"
        };
        var filename = WriteData(data);
        var expected = GetExpected(data);

        var parser = GetParser();
        var actual = parser.Parse(filename);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public static void Should_ParseAsLowercase_WithRepeats()
    {
        var data = new[]
        {
            "кАЖДЫй",
            "каЖдЫй",
            "Каждый",
            "охОтник",
            "желАЕт",
            "жЕЛает",
            "ГДЕ",
            "где",
            "знатЬ",
            "фазАН",
            "фаЗан",
            "сидит",
            "каЖдый"
        };
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
        var testName = TestContext.CurrentContext.Test.Name;
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