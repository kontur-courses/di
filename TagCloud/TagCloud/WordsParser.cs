namespace TagCloud;

public class WordsParser : IWordsParser
{
    public IEnumerable<string> Parse(string text)
    {
        return text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }
}