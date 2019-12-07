using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Visualizer
{
    class TagCloudVisualizer : IVisualizer
    {
        private readonly IVisualizerSettings settings;

        public TagCloudVisualizer(IVisualizerSettings settings)
        {
            this.settings = settings;
        }

        private void DrawWords(IList<WordRectangle> wordRectangles, Graphics graphics)
        {
            foreach (var wordRectangle in wordRectangles)
            {
                var font = settings.GetFont(wordRectangle);
                var brush = settings.GetBrush(wordRectangle);
                graphics.DrawString(wordRectangle.Word, font, brush, wordRectangle.Rectangle);
            }
        }

        public Image DrawImage(IList<WordRectangle> wordRectangles)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.FillRectangle(settings.BackgroundBrush, 0, 0,
                settings.ImageSize.Width, settings.ImageSize.Height);
            DrawWords(wordRectangles, graphics);
            return image;
        }
    }
}
