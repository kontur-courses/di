using TagsCloudVisualization.Drawer;

namespace TagsCloudVisualization.TagFactory;

public interface ITagFactory
{
    TagImage Create(Tag tag);
}