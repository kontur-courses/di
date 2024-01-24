namespace TagsCloudVisualization.WordsProcessors;

public class SimpleWordsProcessor : IWordsProcessor
{
    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        return words.Select(x => x.ToLower()).Where(y => y.Length > 3);
    }
}