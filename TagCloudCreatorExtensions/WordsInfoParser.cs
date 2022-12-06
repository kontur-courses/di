using TagCloudCreator.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions;

public class WordsInfoParser : IWordsInfoParser
{
    private readonly IWordsFileReaderProvider _wordsFileReaderProvider;

    public WordsInfoParser(IWordsFileReaderProvider wordsFileReaderProvider)
    {
        _wordsFileReaderProvider = wordsFileReaderProvider;
    }

    public IEnumerable<WordInfo> GetWordsInfos() =>
        _wordsFileReaderProvider.GetReader().GetWords()
            .Select(w => w.ToLower())
            .GroupBy(w => w)
            .Select(group => new WordInfo(group.Key, group.Count()));
}