using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextAnalysers.WordsFilters;

public class WordsFilter: IWordsFilter
{
    private readonly IAnalyseSettings analyseSettings;

    public WordsFilter(IAnalyseSettings analyseSettings)
    {
        this.analyseSettings = analyseSettings;
    }

    public IEnumerable<WordDetails> Filter(IEnumerable<WordDetails> wordDetails)
    {
        return wordDetails
            .Where(word => analyseSettings.ValidSpeechParts.Contains(word.SpeechPart));
    }
}