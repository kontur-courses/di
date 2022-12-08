namespace TagsCloudVisualization.Parsing;

public class SingleColumnTextParser : ITextParser
{
    public IEnumerable<string> ParseWords(string text)
    {
        return text.Split(Environment.NewLine);
    }
}