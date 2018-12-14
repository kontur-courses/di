using System.Collections.Generic;
using System.Drawing;

namespace WordCloud.LayoutGeneration.Cloud
{
    public class RectangleCloud : IRectangleCloud
    {
        public List<Rectangle> Rectangles { get; set; }

        public RectangleCloud(List<Rectangle> rectangles)
        {
            Rectangles = rectangles;
        }

        public RectangleCloud()
        {
            this.Rectangles = new List<Rectangle>();
        }
    }
}