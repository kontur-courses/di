using MyStemWrapper;
using MyStemWrapper.Domain;
using TagCloudCreator.Interfaces;
using TagCloudCreatorExtensions.WordsFilters.Settings;

namespace TagCloudCreatorExtensions.WordsFilters;

public class MyStemSpeechPartWordsFilter : IWordsFilter
{
    private readonly ISpeechPartWordsFilterSettings _settings;
    private readonly MyStemWordsGrammarInfoParser _grammarInfoParser;

    public MyStemSpeechPartWordsFilter(
        ISpeechPartWordsFilterSettings settings,
        MyStemWordsGrammarInfoParser grammarInfoParser
    )
    {
        _settings = settings;
        _grammarInfoParser = grammarInfoParser;
    }

    public IEnumerable<string> FilterWords(IEnumerable<string> sourceWords) =>
        _grammarInfoParser.Parse(sourceWords)
            .Where(info => !_settings.ExcludedSpeechParts.Contains(info.SpeechPart))
            .Where(info => !_settings.ExcludeUndefined || info.SpeechPart is not SpeechPart.Undefined)
            .Select(word => word.OriginalForm);
}