using MyStemWrapper;
using TagsCloudContainer.TextAnalysers.WordsFilters;

namespace TagsCloudContainer.TextAnalysers;

public class TextPreprocessor: ITextPreprocessor
{
    private readonly MyStem myStem;
    private readonly IMyStemParser myStemParser;
    private readonly IWordsFilter wordsFilter;
    private readonly IFrequencyCalculator frequencyCalculator;

    public TextPreprocessor(MyStem myStem, IMyStemParser myStemParser, IWordsFilter wordsFilter, IFrequencyCalculator frequencyCalculator)
    {
        this.myStem = myStem;
        this.myStemParser = myStemParser;
        this.wordsFilter = wordsFilter;
        this.frequencyCalculator = frequencyCalculator;
    }

    public WordDetails[] Preprocess(string text)
    {
        var analyzed = myStem.Analysis(text);
        var wordInfos = analyzed.Split('\n');
        var wordsDetails = new List<WordDetails>(wordInfos.Length);
        foreach (var wordInfo in wordInfos)
        {
            if (myStemParser.CanParse(wordInfo))
                wordsDetails.Add(myStemParser.Parse(wordInfo));
        }

        return wordsFilter.Filter(frequencyCalculator.CalculateFrequency(wordsDetails))
            .ToArray();
    }
}