using System.Globalization;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;

public class DefaultWordsPreprocessor : IWordsPreprocessor
{
    private readonly CultureInfo cultureInfo;
    private HashSet<IWord> wordsSet;

    public DefaultWordsPreprocessor(CultureInfo cultureInfo)
    {
        this.cultureInfo = cultureInfo;
        wordsSet = new HashSet<IWord>();
    }

    public ISet<IWord> GetProcessedWords(List<string> words, IReadOnlyCollection<IBoringWords> boringWords)
    {
        wordsSet = CreateWordsSet(words)
            .Where(word =>
                boringWords.All(checker => !checker.IsBoring(word)))
            .ToHashSet();
        CalculateTfIndexes(wordsSet, words.Count);
        return wordsSet;
    }

    private static double GetTfIndex(int wordCount, int totalWordsCount)
    {
        return 1d * wordCount / totalWordsCount;
    }
        
    private static void CalculateTfIndexes(IEnumerable<IWord> words, int totalWordsCount)
    {
        foreach (var word in words)
        {
            word.Tf = GetTfIndex(word.Count, totalWordsCount);
        }
    }
        
    private HashSet<IWord> CreateWordsSet(IEnumerable<string> words)
    {
        var set = new HashSet<IWord>();
        foreach (var word in words.Select(wordValue => new Word(wordValue.ToLower(cultureInfo))))
        {
            if (set.TryGetValue(word, out var containedWord))
                containedWord.Count++;
            else 
                set.Add(word);
        }
        return set;
    }
}