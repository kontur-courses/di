using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class WordParser_Should
{
    [Test]
    public void GetInterestingWords_GetsAllLexems_WhenNoExcludedWordsAndPartsOfSpeech()
    {
        var wordParser = new MystemWordsParser(
            new MystemDullWordChecker(new HashSet<string>(),
                @"TextFiles\EmptyExcludedWords.txt"));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("яблоко", "груша", "персиковый", "яблочный", "красить", "петь", "и", "я", "он", "она");
    }

    [Test]
    public void GetInterestingWordsExcludeWords_WhenExclusionFileNotEmpty()
    {
        var wordParser = new MystemWordsParser(
            new MystemDullWordChecker(new HashSet<string>(),
                @"TextFiles\ExcludedWords.txt"));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("груша", "персиковый", "яблочный", "красить", "и", "я", "он", "она");
    }

    [Test]
    public void GetInterestingWordsExcludesPartsOfSpeech()
    {
        var wordParser = new MystemWordsParser(
            new MystemDullWordChecker(new HashSet<string> { "S", "CONJ" },
                @"TextFiles\EmptyExcludedWords.txt"));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("персиковый", "яблочный", "красить", "петь");
    }
    
    [Test]
    public void GetInterestingWordsExcludesPartsOfSpeechAndWords()
    {
        var wordParser = new MystemWordsParser(
            new MystemDullWordChecker(new HashSet<string> { "S", "CONJ" },
                @"TextFiles\ExcludedWords.txt"));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("персиковый", "яблочный", "красить");
    }
}