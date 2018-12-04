using System.Collections.Generic;
using TagsCloudContainer.Tags;

namespace TagsCloudContainer.CloudBuilder
{
    public interface ICloudBuilder
    {
        IEnumerable<Tag> BuildTagsCloud();
    }
}