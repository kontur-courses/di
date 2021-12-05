using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Interfaces;

public interface ITagPainter
{
    IEnumerable<PaintedTag> Paint(IEnumerable<Tag> tags);
}
