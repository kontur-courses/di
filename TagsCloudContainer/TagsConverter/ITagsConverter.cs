using TagsCloudContainer.Options;

namespace TagsCloudContainer.TagsConverter;

public interface ITagsConverter
{
    IEnumerable<Tag> ConvertToTags(IEnumerable<string> words, VisualizationOptions visualizationOptions);
}