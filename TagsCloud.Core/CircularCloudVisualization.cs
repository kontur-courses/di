using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Core
{
    public static class CircularCloudVisualization
    {
        public static Bitmap CreateImage(List<Rectangle> rectangles, int imageWidth, int imageHeight)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            foreach (var rect in rectangles)
                Graphics.FromImage(image).DrawRectangle(new Pen(Color.Black), rect);
            return image;
        }
    }
}