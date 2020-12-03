using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ITagsVisualizer
    {
        public Bitmap GetBitmap(List<Rectangle> rectangles);
    }
}