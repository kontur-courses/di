namespace TagsCloudVisualization.Abstractions;

public interface ITextReader
{
    IEnumerable<string> ReadLines();
    bool IsValid();
}