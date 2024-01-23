using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloud.Helpers;

namespace TagsCloud.Filters;

public class SpeechPartFilter : FilterBase, IWordFilter
{
    public SpeechPartFilter(FilterOptions options) : base(options)
    {
    }

    public List<string> GetFilteredResult(List<string> words)
    {
        var filtered = new List<string>();
        var analysis = TextAnalyzer.GetTextAnalysis(words);

        foreach (var analysisItem in analysis)
        {
            // If english word appeared in text.
            if (analysisItem.AnalysisItems.Count == 0)
                filtered.Add(analysisItem.Text);

            var wordAnalysis = analysisItem.AnalysisItems.First();
            var actualPart = wordAnalysis.Grammar
                .Split(FileHelper.Separators, StringSplitOptions.RemoveEmptyEntries)[0];

            if (!Options.ImportantTextParts.Contains(actualPart))
                continue;

            filtered.Add(Options.CastWordsToInfinitive ? wordAnalysis.Lexico : analysisItem.Text);
        }

        return filtered;
    }
}