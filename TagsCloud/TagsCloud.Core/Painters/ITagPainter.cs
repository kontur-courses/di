using System.Drawing;
using TagsCloud.Core.TagContainersProviders;

namespace TagsCloud.Core.Painters;

public interface ITagPainter
{
    public Bitmap Draw(IEnumerable<TagContainer> tagContainers);
}