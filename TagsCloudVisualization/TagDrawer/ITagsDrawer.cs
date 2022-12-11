namespace TagsCloudVisualization.TagDrawer;

public interface ITagsDrawer
{
    void Draw(IReadOnlyCollection<TagImage> tagImages,string filepath);
}