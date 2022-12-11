namespace TagsCloudVisualization.ToTagConverter;

public interface IToTagConverter
{
    IReadOnlyCollection<Tag> Convert(IEnumerable<string> words);
}