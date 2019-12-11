using System;
using System.Drawing;

namespace TagCloud
{
    public class ImageSettings
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public PointF CloudCenter { get; set; }

        public ImageSettings(int height, int width, PointF cloudCenter)
        {
            if (cloudCenter.X < 0 || cloudCenter.Y < 0)
                throw new ArgumentException("Invalid center");
            Height = height;
            Width = width;
            CloudCenter = cloudCenter;
        }

        public static ImageSettings GetDefaultSettings() =>
            new ImageSettings(1000, 1000, new PointF(500, 500));
    }
}
