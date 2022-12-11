namespace TagsCloudVisualization.TextProviders;

public interface ITextProvider
{
    IEnumerable<string> GetText();
}