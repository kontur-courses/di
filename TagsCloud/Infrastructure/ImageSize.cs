using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Infrastructure
{
    public class ImageSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageSize() {}

        public ImageSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
