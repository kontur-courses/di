using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Implementations;

public class WordsInfoProvider : IWordsInfoProvider
{
    public IReadOnlyCollection<WordInfo> WordInfos { get; }

    public WordsInfoProvider(IWordsSelector wordsSelector)
    {
        WordInfos = wordsSelector.GetWords().ToList();
    }
}