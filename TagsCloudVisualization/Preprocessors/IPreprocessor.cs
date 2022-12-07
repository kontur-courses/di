namespace TagsCloudVisualization.Preprocessors;

public interface IPreprocessor
{
    IEnumerable<string> Process(IEnumerable<string> text);
}