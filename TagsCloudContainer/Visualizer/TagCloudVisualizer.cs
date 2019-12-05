using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Visualizer
{
    class TagCloudVisualizer : IVisualizer, IDisposable
    {
        private readonly IVisualizerSettings settings;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public TagCloudVisualizer(IVisualizerSettings settings)
        {
            this.settings = settings;
            bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
        }

        private void DrawWords(IList<WordRectangle> wordRectangles)
        {
            foreach (var wordRectangle in wordRectangles)
            {
                var size = wordRectangle.Rectangle.Height;
                var font = new Font(settings.FontFamily, size, settings.FontStyle, GraphicsUnit.Pixel);
                var brush = new SolidBrush(settings.TextColor);
                graphics.DrawString(wordRectangle.Word, font, brush, wordRectangle.Rectangle);
            }
        }

        public Bitmap GetImage(IList<WordRectangle> wordRectangles)
        {
            DrawWords(wordRectangles);
            return bitmap;
        }

        public void Dispose()
        {
            bitmap?.Dispose();
            graphics?.Dispose();
        }
    }
}
