using System.Text.RegularExpressions;

namespace TagsCloudContainer.utility;

public class WordDataSet
{
    public IEnumerable<(string word, int count)> CreateFrequencyDict(string text)
    {
        var words = Regex
            .Matches(text, @"[\w\d]+")
            .Select(m => m.Value)
            .ToArray();

        var dict = new Dictionary<string, int>();

        foreach (var word in words)
            if (!dict.TryAdd(word, 1))
                dict[word]++;

        return dict.Select(kv => (kv.Key, kv.Value));
    }
}