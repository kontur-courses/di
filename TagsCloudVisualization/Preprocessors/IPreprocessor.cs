namespace TagsCloudVisualization.Abstractions;

public interface IPreprocessor
{
    IEnumerable<string> Process(IEnumerable<string> text);
}