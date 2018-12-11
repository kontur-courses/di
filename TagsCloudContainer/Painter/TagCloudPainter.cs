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
        private readonly ImageSettings imageSettings;
        private readonly Palette palette;
        private readonly FontSettings fontSettings;

        public TagCloudPainter(IImageHolder holder,
            ImageSettings imageSettings,
            Palette palette,
            FontSettings fontSettings)
        {
            this.holder = holder;
            this.imageSettings = imageSettings;
            this.palette = palette;
            this.fontSettings = fontSettings;
        }

        public void Paint(Point center, IEnumerable<WordInfo> wordInfosEnum)
        {
            var wordInfos = wordInfosEnum.ToArray();
            var radius = (int)wordInfos.Select(wordInfo => wordInfo.Rect).Select(rect => Math.Ceiling(rect.Location.DistanceTo(center))).Max();
            var bitmapSize = GetBitmapSize(wordInfos.Select(info => info.Rect), center);
            var imageSize = holder.GetImageSize();
            if (imageSize.Width < bitmapSize || imageSize.Height < bitmapSize)
            {
                imageSettings.Height = bitmapSize;
                imageSettings.Width = bitmapSize;
                holder.RecreateImage(imageSettings);
            }

            imageSize = holder.GetImageSize();
            using (var graphics = holder.StartDrawing())
            {
                var deltaX = imageSize.Width / 2;
                var deltaY = imageSize.Height / 2;
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width, imageSize.Height);
                graphics.TranslateTransform(deltaX, deltaY);
                foreach (var wordInfo in wordInfos)
                {
                    var color = imageSettings.GetCloudPainterClass().GetRectangleColor(center, wordInfo.Rect, radius);
                    graphics.DrawString(
                        wordInfo.Word, 
                        new Font(fontSettings.Font.FontFamily, wordInfo.FontSize), 
                        new SolidBrush(color), 
                        wordInfo.Rect);
                }
            }
        }

        private int GetBitmapSize(IEnumerable<Rectangle> rectangles, Point rectanglesCenter)
        {
            var leftAndRightBorders = rectangles.Select(rectangle => (rectangle.Location.X, rectangle.Location.X + rectangle.Width)).ToArray();
            var maxX = leftAndRightBorders.Max(borders => borders.Item2);
            var minX = leftAndRightBorders.Min(borders => borders.Item1);
            var size = maxX - minX + Math.Max(rectanglesCenter.X, rectanglesCenter.Y) * 2 + 50;
            return size;
        }
    }
}