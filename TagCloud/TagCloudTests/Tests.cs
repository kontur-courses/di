using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

public class Tests
{
    [Test]
    public void FrequencyDictionary_GetWordsFrequency_ContainsAllWord()
    {
        var words = new[] { "Hello", "Yes", "Hello", "B", "No", "Yes" };
        var uniqueWords = words.Distinct();
        var actual = FrequencyDictionary.GetWordsFrequency(words).Select(x => x.Key).ToArray();
        actual.Should().OnlyHaveUniqueItems();
        actual.Should().BeEquivalentTo(uniqueWords);
    }
    
    [Test]
    public void TextWrapper_Wrap_WrappedContainsCorrectText()
    {
        var wordsFrequency = new Dictionary<string, int> { ["Yes"] = 10, ["No"] = 5 };
        var words = wordsFrequency.Select(x => x.Key).ToArray();
        var fontProperties = new FontProperties();
        var wrappedWords = new TextWrapper(fontProperties).Wrap(wordsFrequency).ToArray();
        wrappedWords.Length.Should().Be(words.Length);
        wrappedWords.Select(x => x.Text).Should().BeEquivalentTo(words);
    }
    
    [Test]
    public void WordPreprocessor_Process_MakeLowerCased()
    {
        var words = new[] { "APPLE", "Blueberries", "ApplE", "wAtermelOn", "blueberries", "lemoN", "lEMOn"};
        var wordPreprocessor = new WordPreprocessor();
        foreach (var word in wordPreprocessor.Process(words))
            word.Should().BeLowerCased();
    }
    
    [TestCase("")]
    [TestCase(null)]
    [TestCase("file.text")]
    [TestCase(@"A:\PathIsNotExists\ItIsFake.jpeg")]
    [TestCase(@"ItIsFake.jpeg")]
    public void TxtFileLoader_Load_ExceptionOnIncorrectArgument(string value)
    {
        var loader = new TxtFileLoader();
        
        var action = () => loader.Load(value);
        
        action.Should().Throw<Exception>();
    }
    
    [Test]
    public void TxtFileLoader_Load_ReadCorrect()
    {
        var loader = new TxtFileLoader();
        const string path = "Words.txt";
        var actual = loader.Load(path);
        var expected = File.ReadAllText(path);
        actual.Should().Be(expected);
    }
    
    [TestCase("apple banana watermelon", ExpectedResult = new []{ "apple banana watermelon" })]
    [TestCase("apple\nbanana\nwatermelon", ExpectedResult = new []{ "apple", "banana", "watermelon" })]
    [TestCase("apple\n\nbanana\n\n\nwatermelon", ExpectedResult = new []{ "apple", "banana", "watermelon" })]
    [TestCase("ice-cream", ExpectedResult = new []{ "ice-cream" })]
    [TestCase("can't", ExpectedResult = new []{ "can't"})]
    public IEnumerable<string> WordsParser_Parse_CorrectResult(string text)
    {
        var parser = new WordsParser();
        var actual = parser.Parse(text);
        return actual;
    }
}