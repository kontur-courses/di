namespace TagsCloudContainer.TagsConverter;

public interface ITagsConverter
{
    IEnumerable<Tag> ConvertToTags(IEnumerable<string> words, int minFontSize);
}