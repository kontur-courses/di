using System.Text.RegularExpressions;

namespace TagsCloudVisualization.TextHandlers;

public partial class TextHandler : ITextHandler
{
    private readonly IEnumerable<string> words;
    private readonly HashSet<string> boringWords;

    public TextHandler(string text, string boringWords)
    {
        words = GetWords(text);
        this.boringWords = GetWords(boringWords.ToLower()).ToHashSet();
    }

    public Dictionary<string, int> HandleText()
    {
        var result = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (result.ContainsKey(word))
                result[word]++;
            else if (!boringWords.Contains(word))
                result[word] = 1;
        }

        return result;
    }

    public IEnumerable<string> GetWords(string boringWords)
    {
        var pattern = new Regex(@"\b[\p{L}]+\b", RegexOptions.Compiled);

        foreach (var word in pattern.Matches(boringWords).ToHashSet())
            yield return word.Value;
    }
}
