namespace TagsCloudVisualization.WordsAnalyzers;

public interface ITagProvider
{
    public IEnumerable<Tag> GetTags();
}