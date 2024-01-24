using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloud.Helpers;
using TagsCloud.TextAnalysisTools;

namespace TagsCloud.Filters;

[FilterOrder(2)]
public class SpeechPartFilter : FilterBase
{
    public SpeechPartFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(List<string> words)
    {
        var analysis = TextAnalyzer.GetTextAnalysis(words).ToList();

        for (int i = 0, j = 0; i < analysis.Count; i++)
        {
            string? finalWord = null;
            var analysisItem = analysis[i];

            // If english word appeared in text.
            if (analysisItem.AnalysisItems.Count == 0)
            {
                finalWord = analysisItem.Text;
            }
            else
            {
                var wordAnalysis = analysisItem.AnalysisItems.First();
                var actualPart = wordAnalysis.Grammar
                    .Split(FileHelper.Separators, StringSplitOptions.RemoveEmptyEntries)[0];

                if (options.ImportantLanguageParts.Contains(actualPart))
                    finalWord = options.CastWordsToInfinitive ? wordAnalysis.Lexico : analysisItem.Text;
            }

            if (finalWord == null)
            {
                words.RemoveAt(j);
                continue;
            }

            words[j++] = options.WordsCase == CaseType.Lower ? finalWord.ToLower() : finalWord.ToUpper();
        }
    }
}