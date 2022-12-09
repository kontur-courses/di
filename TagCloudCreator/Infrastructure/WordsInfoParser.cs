using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Infrastructure;

public class WordsInfoParser : IWordsInfoParser
{
    private readonly IFileReaderProvider _fileReaderProvider;
    private readonly IWordsNormalizer _wordsNormalizer;
    private readonly ICollection<IWordsFilter> _wordsFilters;

    public WordsInfoParser(
        IFileReaderProvider fileReaderProvider,
        IWordsNormalizer wordsNormalizer,
        ICollection<IWordsFilter> wordsFilters
    )
    {
        _fileReaderProvider = fileReaderProvider;
        _wordsNormalizer = wordsNormalizer;
        _wordsFilters = wordsFilters;
    }

    public IEnumerable<WordInfo> GetWordsInfo()
    {
        var text = _fileReaderProvider.GetReader().ReadFile().ToLower();
        var originalFormWords = _wordsNormalizer.GetWordsOriginalForm(text);
        var filtered = _wordsFilters.Aggregate(
            originalFormWords,
            (toFilter, filter) => filter.FilterWords(toFilter)
        );
        return filtered
            .GroupBy(w => w)
            .Select(group => new WordInfo(group.Key, group.Count()));
    }
}