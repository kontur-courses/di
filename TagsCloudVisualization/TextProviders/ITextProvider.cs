namespace TagsCloudVisualization.Abstractions;

public interface ITextProvider
{
    IEnumerable<string> GetText(string path);
}