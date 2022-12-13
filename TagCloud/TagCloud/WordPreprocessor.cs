namespace TagCloud;

public class WordPreprocessor : IWordPreprocessor
{
    public List<string> Process(List<string> words, IReadOnlyList<string>? excludedWords = null)
    {
        excludedWords ??= new List<string>();
        var except = excludedWords
            .Select(word => word.ToLower().Trim())
            .ToHashSet();
        return words
            .Select(word => word.ToLower().Trim())
            .Where(x => !except.Contains(x))
            .ToList();
    }
}