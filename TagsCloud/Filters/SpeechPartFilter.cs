using TagsCloud.Entities;
using TagsCloud.Helpers;

namespace TagsCloud.Filters;

public class SpeechPartFilter : FilterBase
{
    public SpeechPartFilter(FilterOptions options) : base(options)
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

                if (Options.ImportantTextParts.Contains(actualPart))
                    finalWord = Options.CastWordsToInfinitive ? wordAnalysis.Lexico : analysisItem.Text;
            }

            if (finalWord == null)
            {
                words.RemoveAt(j);
                continue;
            }

            words[j++] = Options.CaseType == CaseType.Lower ? finalWord.ToLower() : finalWord.ToUpper();
        }
    }
}