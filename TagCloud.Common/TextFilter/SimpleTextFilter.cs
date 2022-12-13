using System.Text.RegularExpressions;

namespace TagCloud.Common.TextFilter;

public class SimpleTextFilter : ITextFilter
{
    public IEnumerable<string> FilterAllWords(IEnumerable<string> lines, int boringWordsLength)
    {
        if (lines == null)
        {
            throw new ArgumentNullException(nameof(lines));
        }
        var words = new List<string>();
        foreach (var line in lines)
        {
            words.AddRange(GetWords(line).Where(word => word.Length > boringWordsLength));
        }

        return words;
    }

    private IEnumerable<string> GetWords(string input)
    {
        var matches = Regex.Matches(input, @"\b[\w']*\b");

        var words = from m in matches
            where !string.IsNullOrEmpty(m.Value)
            select TrimSuffix(m.Value).ToLower();

        return words.ToArray();
    }

    private string TrimSuffix(string word)
    {
        var apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1)
        {
            word = word[..apostropheLocation];
        }

        return word;
    }
}