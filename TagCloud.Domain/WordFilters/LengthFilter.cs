public class LengthFilter : IWordsFilter
{
    private int length { get; }

    public LengthFilter(WordExtractionOptions options)
    {
        length = options.MinWordLength;
    }

    public string[] Filter(string[] words)
    {
        return words.Where(x => x.Length >= length).ToArray();
    }
}