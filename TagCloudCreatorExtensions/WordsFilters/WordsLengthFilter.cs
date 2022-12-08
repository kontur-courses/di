using TagCloudCreator.Interfaces;
using TagCloudCreatorExtensions.WordsFilters.Settings;

namespace TagCloudCreatorExtensions.WordsFilters;

public class WordsLengthFilter : IWordsFilter
{
    private readonly IWordsLengthFilterSettings _settings;

    public WordsLengthFilter(IWordsLengthFilterSettings settings)
    {
        _settings = settings;
    }

    public IEnumerable<string> FilterWords(IEnumerable<string> sourceWords) =>
        sourceWords
            .Where(word => word.Length >= _settings.MinWordLength && word.Length <= _settings.MaxWordLength);
}