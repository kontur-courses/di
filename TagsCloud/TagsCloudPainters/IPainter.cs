using TagsCloud.Layouters;

namespace TagsCloud.TagsCloudPainters;

public interface IPainter
{
    public void DrawCloud(ILayouter layouter);
}