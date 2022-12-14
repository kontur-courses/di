using System.Drawing;
using TagsCloudVisualization.Parsing;
using TagsCloudVisualization.Reading;

namespace TagsCloudVisualization.Words;

public class CustomWordsLoader : IWordsLoader
{
    private readonly ITextReader _textReader;
    private readonly ITextParser _textParser;
    private readonly IWordsFilter _wordsFilter;
    private readonly IWordsSizeCalculator _wordsSizeCalculator;

    public CustomWordsLoader(ITextReader textReader, ITextParser textParser, IWordsSizeCalculator wordsSizeCalculator, IWordsFilter wordsFilter)
    {
        _textReader = textReader;
        _textParser = textParser;
        _wordsSizeCalculator = wordsSizeCalculator;
        _wordsFilter = wordsFilter;
    }

    public IEnumerable<Word> LoadWords(VisualizationOptions options)
    {
        var text = _textReader.ReadText();
        var rawWords = _textParser.ParseWords(text);

        var wordsAndCount = rawWords.GroupBy(r => r.ToLowerInvariant())
            .ToDictionary(r => r.Key, r => r.Count());

        var filteredWords = _wordsFilter.FilterWords(wordsAndCount, options);
        var takeWords = filteredWords;

        if (options.TakeMostPopularWords > 0)
            takeWords = filteredWords.OrderByDescending(r => r.Value)
                .Take(options.TakeMostPopularWords)
                .ToDictionary(r => r.Key, r => r.Value);

        var sizesForWords = _wordsSizeCalculator.CalcSizeForWords(takeWords, options.MinFontSize, options.MaxFontSize);

        var words = new List<Word>();
        foreach (var group in takeWords)
        {
            if (!sizesForWords.TryGetValue(group.Key, out var wordSize))
                wordSize = options.MinFontSize;

            var word = new Word(group.Key, group.Value, wordSize);
            words.Add(word);
        }

        return words.OrderByDescending(r => r.Size).ToList();
    }
}