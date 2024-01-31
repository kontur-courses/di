using TagsCloudCore.BuildingOptions;
using TagsCloudCore.WordProcessing.WordFiltering;
using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudCore.WordProcessing.WordGrouping;

public class DefaultWordProcessor : IProcessedWordProvider
{
    private readonly IEnumerable<IWordFilter> _filters;
    private readonly WordProviderInfo _wordProviderInfo;
    private readonly IEnumerable<IWordProvider> _wordProviders;

    public DefaultWordProcessor(ICommonOptionsProvider commonOptionsProvider, IEnumerable<IWordFilter> filters,
        IEnumerable<IWordProvider> wordProviders)
    {
        _wordProviderInfo = commonOptionsProvider.CommonOptions.WordProviderInfo;
        _wordProviders = wordProviders;
        _filters = filters;
    }

    public Dictionary<string, int> ProcessedWords => ProcessWords();

    private Dictionary<string, int> ProcessWords()
    {
        var provider = _wordProviders.SingleOrDefault(p => p.Match(_wordProviderInfo.Type));
        var words = provider!.GetWords(_wordProviderInfo.ResourceLocation).Select(w => w.ToLower()).ToArray();
        var filtered = _filters.Aggregate(words, (current, filter) => filter.FilterWords(current));
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