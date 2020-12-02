using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ITagsVisualizator
    {
        public Bitmap GetBitmap(List<Rectangle> rectangles);
    }
}