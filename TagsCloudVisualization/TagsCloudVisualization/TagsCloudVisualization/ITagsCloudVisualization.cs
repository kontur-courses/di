using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.TagsCloudVisualization
{
    internal interface ITagsCloudVisualization<in T>
    {   
        Bitmap Draw(IEnumerable<T> figuresToDraw, int imageWidth, int imageHeight);
    }
}
