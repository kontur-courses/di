using System.Drawing;
using TagsCloud.Core.TagContainersCreators;

namespace TagsCloud.Core.Painters;

public interface ITagPainter
{
    public Bitmap Draw(IEnumerable<TagContainer> tagContainers);
}