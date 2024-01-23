namespace TagsCloudPainter.Tags;

public interface ITagsBuilder
{
    public List<Tag> GetTags(List<string> words);
}