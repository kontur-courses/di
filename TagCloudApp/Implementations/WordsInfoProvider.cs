using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Implementations;

public class WordsInfoProvider : IWordsInfoProvider
{
    public IEnumerable<WordInfo> WordInfos => _wordsSelector.GetWordsInfos().ToList();
    private readonly IWordsSelector _wordsSelector;

    public WordsInfoProvider(IWordsSelector wordsSelector)
    {
        _wordsSelector = wordsSelector;
    }
}