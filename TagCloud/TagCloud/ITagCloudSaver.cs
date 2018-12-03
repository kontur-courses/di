using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    internal interface ITagCloudSaver
    {
        TagCloudImage CreateTagCloudImage(IEnumerable<Rectangle> tagCloud);
    }
}