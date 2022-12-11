namespace TagCloud;

public class WordPreprocessor : IWordPreprocessor
{
    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        return words.Select(word => word.ToLower());
    }
}