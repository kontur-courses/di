using TagCloud.Common.Options;

namespace TagCloud.Common.TagsConverter;

public interface ITagsConverter
{
    IEnumerable<Tag> ConvertToTags(IEnumerable<string> words, VisualizationOptions visualizationOptions);
}