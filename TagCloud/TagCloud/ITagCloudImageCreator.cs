using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    internal interface ITagCloudImageCreator
    {
        TagCloudImage CreateTagCloudImage(IEnumerable<(Rectangle, WordInfo)> tagCloud, TagCloudOptions options);
    }
}
