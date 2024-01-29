using MyStemWrapper;
using TagCloud.Excluders;

namespace TagCloud.WordFilters;

public class MyStemWordFilter : IWordFilter
{
    private static readonly string[] ForbidenSpeechParts = new[]
    {
        "PR", // предлог
        "PART", // частица
        "CONJ", // союз
        "INTJ" // междометие
    };
    
    public Dictionary<string, int> ExcludeWords(Dictionary<string, int> counts)
    {
        var stem = new MyStem();
        stem.Parameters = "-lig";
        var newCounts = new Dictionary<string, int>();
        foreach (var (word, count) in counts)
        {
            var analysis = stem.Analysis(word);
            if (string.IsNullOrEmpty(analysis))
                continue;

            analysis = analysis.Substring(1, analysis.Length - 2);
            var analysisResults = analysis.Split(",");
            var partsOfSpeech = analysisResults[0]
                .Split("=|")
                .Select(part => part.Split("=")[1]);

            if (partsOfSpeech.Any(ForbidenSpeechParts.Contains))
                continue;
            newCounts.Add(word, count);
        }

        return newCounts;
    }
}