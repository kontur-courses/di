using System.Text;

public class WordExtractor : IWordExtractor
{
    private readonly IWordsFilter filter;

    public WordExtractor(IWordsFilter filter)
    {
        this.filter = filter;
    }

    public string[] Extract(string text)
    {
        var rawWords = SplitOnWords(text);

        var normalizedWords = rawWords.Select(x => x.ToLower()).ToArray();

        return filter.Filter(normalizedWords);
    }

    private string[] SplitOnWords(string text)
    {
        var buffer = new StringBuilder();
        var wordsList = new List<string>();

        foreach (var symbol in text)
        {
            if (char.IsLetterOrDigit(symbol))
                buffer.Append(symbol);
            else
            {
                wordsList.Add(buffer.ToString());
                buffer.Clear();
            }
        }

        return wordsList.Where(x => !string.IsNullOrEmpty(x))
                        .ToArray();
    }
}
