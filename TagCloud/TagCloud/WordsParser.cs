namespace TagCloud;

public class WordsParser : IWordsParser
{
    public List<string> Parse(string text)
    {
        return text.Split(new[] { "\n", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}