namespace TagsCloudVisualization.Parsing;

public interface ITextParser
{
    IEnumerable<string> ParseWords(string text);
}