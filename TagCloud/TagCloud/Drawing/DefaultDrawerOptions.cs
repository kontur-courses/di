using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Drawing
{
    public class DefaultDrawerOptions : IDrawerOptions
    {
        public Point Center => Point.Empty;
        public IEnumerable<Color> WordColors { get; }
        public Color BackgroundColor => Color.White;
        public FontFamily FontFamily => FontFamily.GenericSerif;
        public float BaseFontSize => 14;
        public Size Size { get; }
        public ImageFormat? Format => ImageFormat.Png;
        public string Path => AppDomain.CurrentDomain.BaseDirectory;
        public string FileName { get; }
    }
}