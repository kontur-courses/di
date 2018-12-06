using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Painter
{
    public class TagCloudPainter
    {
        private readonly IImageHolder holder;
        private readonly Palette palette;
        private readonly FontSettings fontSettings;
        private readonly ICloudColorPainter painter;

        public TagCloudPainter(IImageHolder holder,
            Palette palette,
            FontSettings fontSettings,
            ICloudColorPainter painter)
        {
            this.holder = holder;
            this.palette = palette;
            this.fontSettings = fontSettings;
            this.painter = painter;
        }

        public void Paint(Point center, IEnumerable<WordInfo> wordInfosEnum)
        {
            var wordInfos = wordInfosEnum.ToArray();
            var radius = (int)wordInfos.Select(wordInfo => wordInfo.Rect).Select(rect => Math.Ceiling(rect.Location.DistanceTo(center))).Max();
            var bitmapSize = GetBitmapSize(wordInfos.Select(info => info.Rect), center);
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
                foreach (var wordInfo in wordInfos)
                {
                    var color = painter.GetRectangleColor(center, wordInfo.Rect, radius);
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