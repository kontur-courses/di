namespace TagCloud.WordRanker;

public class WordRankerByFrequency : IWordRanker
{
    public IEnumerable<(string word, int rank)> RankWords(IEnumerable<string> words)
    {
        return words.GroupBy(word => word.Trim().ToLowerInvariant()).OrderByDescending(g => g.Count()).ToList()
            .Select(g => ValueTuple.Create(g.Key, g.Count()));
    }
}