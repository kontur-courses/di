using System.Collections.Generic;
using System.Drawing;

namespace WordCloudImageGenerator.Layouting.Cloud
{
    public class RectangleCloud : IRectangleCloud
    {
        public List<Rectangle> Rectangles { get; }

        public RectangleCloud(List<Rectangle> rectangles) => Rectangles = rectangles;

        public RectangleCloud() => Rectangles = new List<Rectangle>();
    }
}