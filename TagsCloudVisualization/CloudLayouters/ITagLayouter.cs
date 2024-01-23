using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.CloudLayouters;

public interface ITagLayouter
{
    IEnumerable<Tag> GetTags();
}
