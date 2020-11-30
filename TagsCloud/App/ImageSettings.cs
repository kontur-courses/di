using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ImageSettings : IFontFamilyProvider, IImageColorProvider, IImageSizeProvider
    {
        private readonly List<Color> colors = new List<Color>();
        private readonly Random random = new Random();
        public FontFamily FontFamily { get; set; }

        public Color GetColor()
        {
            return colors.Count != 0
                ? colors[random.Next(colors.Count)]
                : Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        public void AddColor(Color color)
        {
            colors.Add(color);
        }

        public ImageSize ImageSize { get; }
    }
}