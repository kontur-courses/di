using FluentAssertions;
using TagsCloud.WordHandler.Implementation;

namespace TagsCloud.Tests;

[TestFixture]
public class WordHandlerShould
{
    [TestCase("UP Down He kfSf soSme SoMe")]
    [TestCase("UP fafIfj fkiwI")]
    public void LowerCaseHandler_WordsInUpperCaseBySpace_AllCharsLower(string words)
    {
        var wordsArray = words.Split(" ");
        var wordsInLower = wordsArray.Select(word => word.ToLower()).ToArray();
        var handler = new LowerCaseHandler();
        handler.ProcessWords(wordsArray).Should().BeEquivalentTo(wordsInLower);
    }

    [TestCase("в дом за дом", "дом дом")]
    [TestCase("над горой за рекой к оврагу", "горой рекой оврагу")]
    public void BoringRusWordsHandler_PrepositionsWithWordsBySpace_WithoutPrepositions(string words, string result)
    {
        var wordsArray = words.Split(" ");
        var handler = new BoringRusWordsHandler();
        string.Join(" ", handler.ProcessWords(wordsArray)).Should().Be(result);
    }

    [TestCase("я гулял он пришел", "гулял пришел")]
    [TestCase("я гулял что-то нашел", "гулял нашел")]
    public void BoringRusWordsHandler_PronounsWithWordsBySpace_WithoutPronouns(string words, string result)
    {
        var wordsArray = words.Split(" ");
        var handler = new BoringRusWordsHandler();
        string.Join(" ", handler.ProcessWords(wordsArray)).Should().Be(result);
    }

    [TestCase("я гулял в доме за рекой", "гулял доме рекой")]
    [TestCase("он пришел кое-что взял у меня", "пришел взял меня")]
    public void BoringRusWordsHandler_PronounsAndPrepositionsWithWords_WithoutPronounsAndPrepositions(string words,
        string result)
    {
        var wordsArray = words.Split(" ");
        var handler = new BoringRusWordsHandler();
        string.Join(" ", handler.ProcessWords(wordsArray)).Should().Be(result);
    }

    [TestCase("гулял гулял снова пришел", 3)]
    [TestCase("гулял гулял гулял", 1)]
    public void RecurringWordsHandler_SeveralRecurringWords_Count(string words, int result)
    {
        var wordsArray = words.Split(" ");
        var handler = new RecurringWordsHandler();
        handler.ProcessWords(wordsArray);
        handler.WordCount.Count.Should().Be(result);
    }

    [TestCase("гулял гулял гулял", "гулял")]
    [TestCase("гулял гулял снова пришел", "гулял снова пришел")]
    public void RecurringWordsHandler_SeveralRecurringWords_WordsWithoutRecurring(string words, string result)
    {
        var wordsArray = words.Split(" ");
        var handler = new RecurringWordsHandler();
        string.Join(" ", handler.ProcessWords(wordsArray)).Should().Be(result);
    }

    [Test]
    public void RecurringWordsHandler_SeveralRecurringWords_CurrentWordCount()
    {
        var wordsArray = "гулял гулял гулял пришел".Split(" ");
        var handler = new RecurringWordsHandler();
        handler.ProcessWords(wordsArray);
        handler.WordCount["гулял"].Should().Be(3);
    }
}