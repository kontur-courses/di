using System.Collections.Generic;
using TagsCloud.WordProcessing;

namespace TagsCloud.Interfaces
{
    public interface ITagCloudGenerator
    {
        IEnumerable<(Tag tag, System.Drawing.Rectangle position)> GenerateTagCloud(IEnumerable<Tag> allTags);
    }
}
