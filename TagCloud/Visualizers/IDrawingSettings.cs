using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface IDrawingSettings
    {
        public Brush PenBrush { get; }
        public Brush BackgroundBrush { get; }
        //public int Width { get; }
        //public int Height { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }
    }
}
