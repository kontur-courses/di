using TagCloudCreator.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class WordsInfosProvider : IWordsInfosProvider
{
    public IEnumerable<WordInfo> WordsInfos => _wordsInfoParser.GetWordsInfos().ToList();
    private readonly IWordsInfoParser _wordsInfoParser;

    public WordsInfosProvider(IWordsInfoParser wordsInfoParser)
    {
        _wordsInfoParser = wordsInfoParser;
    }
}