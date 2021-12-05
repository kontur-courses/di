using System.Collections.Generic;
using TagCloud.Creators;

namespace TagCloud.Layouters
{
    public interface ICloudLayouter
    {
        IEnumerable<Tag> PutTags(IEnumerable<Tag> tags);
    }
}
