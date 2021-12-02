namespace TagsCloudVisualization.Abstractions;

public interface IWordFilter
{
    bool IsValid(string word);
}