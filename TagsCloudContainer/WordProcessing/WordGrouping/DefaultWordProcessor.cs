using TagsCloudContainer.Utils;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainer.WordProcessing.WordGrouping;

public class DefaultWordProcessor : IProcessedWordProvider
{
    private readonly string[] _words;
    private readonly IEnumerable<IWordFilter> _filters;
    
    public DefaultWordProcessor(IWordProvider words, IEnumerable<IWordFilter> filters)
    {
        _words = words.Words.Select(w => w.ToLower()).ToArray();
        _filters = filters;
    }

    public Dictionary<string, int> ProcessedWords => ProcessWords();

    private Dictionary<string, int> ProcessWords()
    {
        var filtered = _filters.Aggregate(_words, (current, filter) => filter.FilterWords(current));
        return GroupWords(filtered);
    }
    
    private static Dictionary<string, int> GroupWords(IEnumerable<string> filtered)
    {
        var frequency = new Dictionary<string, int>();
        foreach (var word in filtered)
        {
            frequency.TryAdd(word, 0);
            frequency[word]++;
        }

        return frequency;
    }
}