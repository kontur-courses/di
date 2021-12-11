using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public class DrawingSettings : IDrawingSettings
    {
        public IEnumerable<Color> PenColors { get; }
        public Color BackgroundColor { get; }
        public Font Font { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }

        public DrawingSettings(IEnumerable<Color> penColor, Color backgroundColor, int width, int height, Font font)
        {
            PenColors = penColor;
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
            Font?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
