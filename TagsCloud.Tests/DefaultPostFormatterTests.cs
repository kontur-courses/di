using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Formatters;

namespace TagsCloud.Tests;

[TestFixture]
public class DefaultPostFormatterTests
{
    private readonly DefaultPostFormatter defaultFormatter = new();

    [TestCase("Mercedes-Benz", "Mercedes")]
    [TestCase("Hello, world!", "Hello")]
    [TestCase("123", "")]
    [TestCase("Apple Orange Banana", "Apple")]
    [TestCase("", "")]
    [TestCase("      circle", "circle")]
    [TestCase("circle      ", "circle")]
    [TestCase("$$$", "")]
    [TestCase("A", "A")]
    [TestCase("   ___   Juice", "")]
    [TestCase("                   ", "")]
    public void Formatter_Should_CutLineToFirstNonLetterCharacter(string line, string expected)
    {
        var actual = defaultFormatter.Format(line);
        actual.ShouldBeEquivalentTo(expected);
    }
}