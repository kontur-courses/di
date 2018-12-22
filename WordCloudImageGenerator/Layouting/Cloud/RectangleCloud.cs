using System.Collections.Generic;
using System.Drawing;

namespace WordCloudImageGenerator.LayoutCraetion.Cloud
{
    public class RectangleCloud : IRectangleCloud
    {
        public List<Rectangle> Rectangles { get; private set; }

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