namespace TagsCloudVisualization.Abstractions;

public interface ITag
{
    string Value { get; }
    double RelativeSize { get; }
}