namespace TagsCloudVisualization.WordsProcessors;

public interface IWordsProcessor
{
    public IEnumerable<string> Process(IEnumerable<string> words);
}