using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagCloudVisualization
    {
        void SaveRectanglesCloud(
            string bitmapName,
            string directory,
            List<Rectangle> rectangles,
            Point center);

        void SaveTagCloud(
            string bitmapName,
            string directory,
            Dictionary<string, int> words);
    }
}