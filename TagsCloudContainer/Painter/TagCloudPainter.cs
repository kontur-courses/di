using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagCloudPainter
    {
        private readonly IImageHolder holder;
        private readonly WordsContainer container;
        private readonly Palette palette;
        private readonly FontSettings fontSettings;
        private readonly WordDrawInfoGetter drawInfoGetter;
        private readonly ICloudColorPainter painter;

        public TagCloudPainter(IImageHolder holder,
            WordsContainer container,
            Palette palette,
            FontSettings fontSettings,
            WordDrawInfoGetter drawInfoGetter,
            ICloudColorPainter painter)
        {
            this.holder = holder;
            this.container = container;
            this.palette = palette;
            this.fontSettings = fontSettings;
            this.drawInfoGetter = drawInfoGetter;
            this.painter = painter;
        }

        public void Paint()
        {
            drawInfoGetter.GetWordsAndRectangles();
            var wordDrawInfos = container.WordsToDraw;
            var radius = (int)wordDrawInfos.Select(wordInfo => wordInfo.Rect).Select(rect => Math.Ceiling(rect.Location.DistanceTo(container.WordsCenter))).Max();
            var bitmapSize = GetBitmapSize(wordDrawInfos.Select(info => info.Rect), container.WordsCenter);
            holder.RecreateImage(new ImageSettings
            {
                Height = bitmapSize,
                Width = bitmapSize
            });
            using (var graphics = holder.StartDrawing())
            {
                var delta = bitmapSize / 2;
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, bitmapSize, bitmapSize);
                graphics.TranslateTransform(delta, delta);
                foreach (var wordInfo in wordDrawInfos)
                {
                    var color = painter.GetRectangleColor(container.WordsCenter, wordInfo.Rect, radius);
                    graphics.DrawString(
                        wordInfo.Word, 
                        new Font(fontSettings.FontFamily, wordInfo.FontSize), 
                        new SolidBrush(color), 
                        wordInfo.Rect);
                }
            }
        }

        private int GetBitmapSize(IEnumerable<Rectangle> rectangles, Point rectanglesCenter)
        {
            var xCoordinates = rectangles.Select(rectangle => rectangle.Location.X).ToArray();
            var maxX = xCoordinates.Max();
            var minX = xCoordinates.Min();
            var size = maxX - minX + Math.Max(rectanglesCenter.X, rectanglesCenter.Y) * 2 + 50;
            return size;
        }
    }
}