using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
public class WordsParserTests
{
    [TestCase("apple banana watermelon", ExpectedResult = new[] { "apple", "banana", "watermelon" })]
    [TestCase("apple\nbanana\nwatermelon", ExpectedResult = new[] { "apple", "banana", "watermelon" })]
    [TestCase("apple\n\nbanana\n\n\nwatermelon", ExpectedResult = new[] { "apple", "banana", "watermelon" })]
    [TestCase("ice-cream", ExpectedResult = new[] { "ice-cream" })]
    [TestCase("can't", ExpectedResult = new[] { "can't" })]
    public IEnumerable<string> WordsParser_Parse_CorrectResult(string text)
    {
        var parser = new WordsParser();

        var actual = parser.Parse(text);

        return actual;
    }
}