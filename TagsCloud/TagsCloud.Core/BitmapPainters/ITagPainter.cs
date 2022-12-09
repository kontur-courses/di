using System.Drawing;
using TagsCloud.Core.TagContainersCreators;

namespace TagsCloud.Core.BitmapPainters;

public interface ITagPainter
{
    public Bitmap Draw(IEnumerable<TagContainer> tagContainers, Size imageSize);
}