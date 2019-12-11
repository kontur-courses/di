using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    public interface ICloudBuilder<out T>
    {
        T CreateTagCloudFromTags(IEnumerable<CloudTag> tags, Size imageSize, CloudFormat format);
    }
}