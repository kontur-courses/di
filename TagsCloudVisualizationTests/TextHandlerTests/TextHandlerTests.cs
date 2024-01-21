using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextHandlers;

namespace TagsCloudVisualizationTests.TextHandlerTests;

[TestFixture]
public class TextHandlerTests
{
    [Test]
    public void GetWords_SimpleExample_ReturnsCorrectWords()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("simple boring words");
        result.Should().BeEquivalentTo(new List<string> { "simple", "boring", "words" });
    }

    [Test]
    public void GetWords_WithSpecialRussianSymbol_ReturnsCorrectWords()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("ёжик ёлка");
        result.Should().BeEquivalentTo(new List<string> { "ёжик", "ёлка" });
    }

    [Test]
    public void GetWords_EmptyString_ReturnsEmptyCollection()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("");
        result.Should().BeEmpty();
    }

    [Test]
    public void GetWords_StringWithOnlySpecialSymbols_ReturnsEmptyCollection()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("& * \r \n \t ! @ # $");
        result.Should().BeEmpty();
    }

    [Test]
    public void GetWords_WordsWithNumbers_ReturnsEmptyCollection()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("te1xt 1text text1");
        result.Should().BeEmpty();
    }

    [Test]
    public void GetWords_WordsAndSpecialSymbols()
    {
        var handler = new TextHandler("", "");
        var result = handler.GetWords("Why, who and what?");
        result.Should().BeEquivalentTo(new List<string> { "Why", "who", "and", "what" });
    }

    [Test]
    public void HandleText_OneWordManyTimes_ReturnCorrectWordCount()
    {
        var handler = new TextHandler("text text text text text", "");
        var result = handler.HandleText();
        result.Should().BeEquivalentTo(new Dictionary<string, int> { { "text", 5 } });
    }

    [Test]
    public void HandleText_ManyWords_ReturnCorrectWordCount()
    {
        var handler = new TextHandler("text little text spring", "");
        var result = handler.HandleText();
        result.Should().BeEquivalentTo(new Dictionary<string, int> { { "text", 2 }, { "little", 1 }, { "spring", 1 } });
    }

    [Test]
    public void HandleText_EmptyString_ReturnEmptyCollection()
    {
        var handler = new TextHandler("", "");
        var result = handler.HandleText();
        result.Should().BeEmpty();
    }

    [Test]
    public void HandleText_ManyElementsAndBoringWords_ReturnCorrectCountWithoutBoringWords()
    {
        var handler = new TextHandler("text text notebook table lamp lamp notebook lamp", "text");
        var result = handler.HandleText();
        result.Should().BeEquivalentTo(new Dictionary<string, int> { { "notebook", 2 }, { "table", 1 }, { "lamp", 3 } });
    }
}