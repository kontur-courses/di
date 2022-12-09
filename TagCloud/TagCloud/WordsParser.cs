namespace TagCloud;

public class WordsParser : IWordsParser
{
    public IEnumerable<string> Parse(string text) => text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
}