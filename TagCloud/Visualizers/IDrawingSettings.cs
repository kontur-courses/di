using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface IDrawingSettings : IDisposable
    {
        public IEnumerable<Color> PenColors { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }
    }
}
