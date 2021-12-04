using System.Collections.Generic;

namespace TagCloud.Layouters
{
    public interface ICloudLayouter
    {
        IEnumerable<Tag> PutTags(IEnumerable<Tag> tags);
    }
}
