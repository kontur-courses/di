using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using TagsCloud.ColoringAlgorithms;

namespace TagsCloud.ImageConfig
{
    interface IImageConfig
    {
        public Size Size { get; }
        public Font Font { get; }
        public Color BackgroundColor { get; }
        public IColoringAlgorithm ColoringAlgorithm { get; }
    }
}
