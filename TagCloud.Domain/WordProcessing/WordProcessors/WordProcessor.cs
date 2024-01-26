using TagCloud.Domain.Settings;
using TagCloud.Domain.WordEntities;
using TagCloud.Domain.WordProcessing.Interfaces;

namespace TagCloud.Domain.WordProcessing.WordProcessors;

public class WordProcessor : IWordProcessor
{
    private readonly IEnumerable<IWordExtractor> extractors;
    private readonly TagCloudSettings tagCloudSettings;

    public WordProcessor(IEnumerable<IWordExtractor> extractors, TagCloudSettings tagCloudSettings)
    {
        this.extractors = extractors;
        this.tagCloudSettings = tagCloudSettings;
    }
    
    public WordsWithCount GetClearWordsWithCount(IEnumerable<string> words)
    {
        var (clearWords, minCount, maxCount) = ExtractWords(words);

        var wordsResult = clearWords
            .Select(pair => new WordWithCount(pair.Key, pair.Value));

        if (tagCloudSettings.LayoutSettings.BigToCenter)
            wordsResult = wordsResult.OrderByDescending(w => w.Count);

        return new WordsWithCount(
            minCount,
            maxCount,
            wordsResult.ToArray());
    }

    private (Dictionary<string, int> result, int minCount, int maxCount) ExtractWords(IEnumerable<string> words)
    {
        var result = new Dictionary<string, int>();
        var minCount = int.MaxValue;
        var maxCount = int.MinValue;
        
        foreach (var word in words)
        {
            var wordLower = word.ToLower();
            var isSuitable = extractors
                .Aggregate(true, (current, extractor) => current && extractor.IsSuitable(wordLower));

            if (!isSuitable)
                continue;

            result.TryAdd(wordLower, 0);
            result[wordLower]++;

            minCount = int.Min(minCount, result[wordLower]);
            maxCount = int.Max(maxCount, result[wordLower]);
        }

        return (result, minCount, maxCount);
    }
}