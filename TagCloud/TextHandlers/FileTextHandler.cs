using MyStemWrapper;

namespace TagCloud.TextHandlers;

public class FileTextHandler : ITextHandler
{
    private static readonly string[] ForbidenSpeechParts = new []
    {
        "PR", // предлог
        "PART", // частица
        "CONJ", // союз
        "INTJ" // междометие
    };

    private readonly Stream stream;

    public FileTextHandler(Stream stream)
    {
        this.stream = stream;
    }
    
    public IEnumerable<(string word, int count)> Handle()
    {
        Dictionary<string, int> counts = new();
        using var sr = new StreamReader(stream);
        
        while(!sr.EndOfStream){
            var t = sr.ReadLine()?.ToLower();
            if (t == null)
                continue;
            counts.TryAdd(t, 0);
            counts[t]++;
        }
        
        counts = ExcludeWords(counts);
        
        return counts.Select(pair => (pair.Key, pair.Value));
    }

    private Dictionary<string, int> ExcludeWords(Dictionary<string, int> counts)
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