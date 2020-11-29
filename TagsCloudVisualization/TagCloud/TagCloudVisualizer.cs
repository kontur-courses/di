﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization.TagCloud
{
    public static class TagCloudVisualizer
    {
        public static void PrintTagCloud(
            List<Rectangle> rectangles,
            Point rectanglesCenter,
            int horizontalEdgeIndent,
            int verticalEdgeIndent,
            string pathToFile = @"..\..\..\Images\",
            string fileName = "TagCloud")
        {
            var imageSize = GetImageSizeToDraw(rectangles, horizontalEdgeIndent, verticalEdgeIndent);
            var image = new Bitmap(imageSize.Height, imageSize.Width);
            rectangles = CenterTheTagCloud(rectangles, image, rectanglesCenter);
            DrawRectangles(rectangles, image,
                new[]
                {
                    Color.MediumVioletRed, Color.Orange, Color.White,
                    Color.Red, Color.LawnGreen, Color.SpringGreen
                });
            image.Save($"{pathToFile}{fileName}.png", ImageFormat.Png);
        }
        
        private static Size GetImageSizeToDraw(List<Rectangle> rectangles, 
            int horizontalOffset, int verticalOffset)
        {
            var maxX = rectangles.Max(rect => rect.Right);
            var minX = rectangles.Min(rect => rect.Left);
            var maxY = rectangles.Max(rect => rect.Bottom);
            var minY = rectangles.Min(rect => rect.Top);

            return new Size(maxX - minX + 2 * horizontalOffset, maxY - minY + 2 * verticalOffset);
        }
        
        private static void DrawRectangles(IEnumerable<Rectangle> rectangles, Bitmap image, Color[] colors)
        {
            var graphics = Graphics.FromImage(image);
            graphics.FillRectangle(new SolidBrush(Color.Black),
                new Rectangle(0, 0, image.Width, image.Height));

            var iterationCount = 0;
            var pen = new Pen(Color.White);
            foreach (var rectangle in rectangles)
            {
                pen.Color = colors[iterationCount % colors.Length];
                graphics.DrawRectangle(pen, rectangle);
                iterationCount++;
            }
        }

        private static List<Rectangle> CenterTheTagCloud(
            IEnumerable<Rectangle> rectangles, Bitmap image, Point rectanglesCenter)
        {
            var shiftX = image.Width / 2 - rectanglesCenter.X;
            var shiftY = image.Height / 2 - rectanglesCenter.Y;

            return rectangles
                .Select(rectangle => new Rectangle(
                    new Point(rectangle.X + shiftX, rectangle.Y + shiftY), rectangle.Size))
                .ToList();
        }
    }
}