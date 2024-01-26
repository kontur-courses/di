using System.Drawing;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter.CloudLayouter;

public interface ICloudLayouter : IResetable
{
    Rectangle PutNextTag(Tag tag);
    TagsCloud GetCloud();
    void PutTags(List<Tag> tags);
}