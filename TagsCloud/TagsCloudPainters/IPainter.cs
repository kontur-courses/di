using System.Drawing;
using TagsCloud.Entities;

namespace TagsCloud.TagsCloudPainters;

public interface IPainter
{
    public void DrawCloud(IEnumerable<Tag> tags, Size imageSize);
}