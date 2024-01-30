using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class WordParser_Should
{
    [Test]
    public void GetInterestingWords_GetsAllLexems_WhenNoExcludedWordsAndPartsOfSpeech()
    {
        var tagLayoutSettings =
            new TagLayoutSettings(Algorithm.Spiral, new HashSet<string>(), @"TextFiles\EmptyExcludedWords.txt");

        var wordParser = new MystemWordsParser(new MystemDullWordChecker(tagLayoutSettings));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("яблоко", "груша", "персиковый", "яблочный", "красить", "петь", "и", "я", "он", "она");
    }

    [Test]
    public void GetInterestingWordsExcludeWords_WhenExclusionFileNotEmpty()
    {
        var tagLayoutSettings =
            new TagLayoutSettings(Algorithm.Spiral, new HashSet<string>(), @"TextFiles\ExcludedWords.txt");
        var wordParser = new MystemWordsParser(new MystemDullWordChecker(tagLayoutSettings));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("груша", "персиковый", "яблочный", "красить", "и", "я", "он", "она");
    }

    [Test]
    public void GetInterestingWordsExcludesPartsOfSpeech()
    {
        var tagLayoutSettings =
            new TagLayoutSettings(Algorithm.Spiral, new HashSet<string> { "S", "CONJ" }, @"TextFiles\EmptyExcludedWords.txt");
        var wordParser = new MystemWordsParser(new MystemDullWordChecker(tagLayoutSettings));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("персиковый", "яблочный", "красить", "петь");
    }

    [Test]
    public void GetInterestingWordsExcludesPartsOfSpeechAndWords()
    {
        var tagLayoutSettings =
            new TagLayoutSettings(Algorithm.Spiral, new HashSet<string> { "S", "CONJ" }, @"TextFiles\ExcludedWords.txt");
        var wordParser = new MystemWordsParser(new MystemDullWordChecker(tagLayoutSettings));
        var parsedWords = wordParser.GetInterestingWords(@"TextFiles\RuWords.txt").ToArray();

        parsedWords.Should()
            .BeEquivalentTo("персиковый", "яблочный", "красить");
    }
}