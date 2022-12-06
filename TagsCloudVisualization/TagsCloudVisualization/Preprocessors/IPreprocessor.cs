namespace TagsCloudVisualization;

public interface IPreprocessor
{
    Dictionary<string, int> Preprocessing(string text);
}