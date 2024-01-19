namespace TagsCloudVisualization.TextReaders;

public interface IWordProvider
{
    public IEnumerable<string> GetWords();
}