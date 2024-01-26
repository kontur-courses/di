using System.Text.RegularExpressions;

namespace TagsCloudContainer.utility;

public class WordDataSet(ITextHandler source)
{
    public IEnumerable<(string word, int count)> CreateFrequencyDict()
    {
        var words = Regex
            .Matches(source.ReadText(), @"[\w\d]+")
            .Select(m => m.Value)
            .ToArray();

        var dict = new Dictionary<string, int>();

        foreach (var word in words)
            if (!dict.TryAdd(word, 1))
                dict[word]++;

        return dict.Select(kv => (kv.Key, kv.Value));
    }
}