using System.Drawing;
using TagsCloud.Core.TagContainersProviders;

namespace TagsCloud.Core.Painters;

public interface ITagsCloudPainter
{
    public Bitmap Draw(IEnumerable<TagContainer> tagContainers);
}