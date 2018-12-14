using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.LayoutGeneration
{
    public interface ITagsCloud
    {
        List<Rectangle> Rectangles { get; set; }
    }
}