using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Implementations;

public class WordsSelector : IWordsSelector
{
    private readonly IWordsFileReaderProvider _wordsFileReaderProvider;

    public WordsSelector(IWordsFileReaderProvider wordsFileReaderProvider)
    {
        _wordsFileReaderProvider = wordsFileReaderProvider;
    }

    public IEnumerable<WordInfo> GetWordsInfos() =>
        _wordsFileReaderProvider.GetReader().GetWords()
            .Select(w => w.ToLower())
            .GroupBy(w => w)
            .Select(group => new WordInfo(group.Key, group.Count()));
}