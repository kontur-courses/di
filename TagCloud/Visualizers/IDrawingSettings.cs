using System;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface IDrawingSettings : IDisposable
    {
        public Brush PenBrush { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }
    }
}
