using System.Drawing;

namespace TagsCloudContainer;

public interface ITagsCloudGenerator
{
    public IEnumerable<Tag> Generate();
}