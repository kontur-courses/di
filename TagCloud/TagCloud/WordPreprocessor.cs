namespace TagCloud;

public class WordPreprocessor : IWordPreprocessor
{
    public IEnumerable<string> Process(IEnumerable<string> words, IEnumerable<string>? excludedWords = null)
    {
        excludedWords ??= Array.Empty<string>();
        var except = excludedWords
            .Select(word => word.ToLower().Trim())
            .ToHashSet();
        return words
            .Select(word => word.ToLower().Trim())
            .Where(x => !except.Contains(x));
    }
}