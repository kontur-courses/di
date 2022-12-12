namespace TagsCloudVisualization.ToTagConverter;

public interface IToTagConverter
{
    IEnumerable<Tag> Convert(IEnumerable<string> words);
}