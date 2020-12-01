using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using TagsCloud.ColoringAlgorithms;

namespace TagsCloud.ImageConfig
{
    public class ImageConfig : IImageConfig
    { 
        public Size Size { get; }
        public Font Font { get; }
        public Color BackgroundColor { get; }
        public IColoringAlgorithm ColoringAlgorithm { get; }
        public ImageConfig(Size size, Font font, Color backgroundColor, IColoringAlgorithm coloringAlgorithm)
        {
            Size = size;
            Font = font;
            BackgroundColor = backgroundColor;
            ColoringAlgorithm = coloringAlgorithm;
        }
    }
}
