using System.Collections.Generic;
using TagsCloud.TagGenerators;

namespace TagsCloud.Interfaces
{
    public interface ITagCloudGenerator
    {
        IEnumerable<(Tag tag, System.Drawing.Rectangle position)> GenerateTagCloud(IEnumerable<Tag> allTags);
    }
}
