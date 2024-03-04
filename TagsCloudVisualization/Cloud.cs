using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Cloud
    {
        public Cloud(Point center)
        {
            Center = center;
        }

        public readonly Point Center;
        public readonly List<Rectangle> Rectangles = new();
    }
}