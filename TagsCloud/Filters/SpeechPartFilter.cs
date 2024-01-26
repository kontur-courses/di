using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloud.TextAnalysisTools;

namespace TagsCloud.Filters;

[FilterOrder(2)]
public class SpeechPartFilter : FilterBase
{
    public SpeechPartFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(List<WordToStatus> words)
    {
        var rawWords = words.Select(word => word.Word).ToList();
        var analyses = TextAnalyzer.GetTextAnalysis(rawWords).ToList();

        for (var i = 0; i < analyses.Count; i++)
        {
            string finalWord;
            var wordInfo = analyses[i];

            if (!wordInfo.IsRussian)
            {
                finalWord = wordInfo.InitialWord;
            }
            else
            {
                var wordAnalysis = wordInfo.Analyses.First();

                if (!options.LanguageParts.Contains(wordAnalysis.LanguagePart))
                    words[i].IsTrash = true;

                finalWord = options.ToInfinitive ? wordAnalysis.Infinitive : wordInfo.InitialWord;
            }

            words[i].Word = options.WordsCase == CaseType.Lower ? finalWord.ToLower() : finalWord.ToUpper();
        }
    }
}