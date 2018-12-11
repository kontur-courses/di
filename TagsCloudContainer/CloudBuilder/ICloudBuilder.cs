using System.Collections.Generic;
using TagsCloudContainer.Tags;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.CloudBuilder
{
    public interface ICloudBuilder
    {
        IEnumerable<Tag> BuildTagsCloud(List<WordFrequency> miniTags);
    }
}