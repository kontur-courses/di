using System;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public class DrawingSettings : IDrawingSettings
    {
        public Brush PenBrush { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }

        public DrawingSettings(Color penColor, Color backgroundColor, int width, int height, Font font)
        {
            PenBrush = new SolidBrush(penColor);
            BackgroundColor = backgroundColor;
            Font = font;
            if (width < 0 || height < 0)
                throw new ArgumentException("Width and height should be positive but was: " +
                                            $"Width = {width} " +
                                            $"Height = {height}");
            Bitmap = new Bitmap(width, height);
            Graphics = Graphics.FromImage(Bitmap);
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
            Graphics?.Dispose();
            PenBrush?.Dispose();
            Font?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
