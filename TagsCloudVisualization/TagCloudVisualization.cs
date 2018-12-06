using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudVisualization
    {
        public TagCloudVisualization()
        {
            bitmapHeight = 1000;
            bitmapWidth = 1000;
        }

        private readonly int bitmapWidth;
        private readonly int bitmapHeight;
        private Color defaultColor = Color.Black;
        private Color defaultBackColor = Color.White;

        public void SaveRectanglesCloud(string bitmapName, string directory, List<Rectangle> rectangles, Point center)
        {
            SaveRectanglesCloud(bitmapName, directory, rectangles, center, defaultColor);
        }

        public void SaveRectanglesCloud(
            string bitmapName, 
            string directory, 
            List<Rectangle> rectangles, 
            Point center, 
            Color color)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var g = Graphics.FromImage(bitmap);
            DrawBackgroundRectangles(g, rectangles, color, center);
            var path = $"{directory}\\{bitmapName}-{rectangles.Count}.png";

            bitmap.Save(path, ImageFormat.Png);
        }

        //ToDo Вынести определение размера шрифта в метод
        public void SaveTagCloud(
            string bitmapName,
            string directory,
            Font font,
            Color color,
            Color backgroundColor,
            ICloudLayouter cloudLayouter,
            List<string> words)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var g = Graphics.FromImage(bitmap);
            var wordsInCloud = new WordsCloudFiller(cloudLayouter, font).GetRectanglesForWordsInCloud(g, words);

            g.FillRectangle(Brushes.White, 0, 0, bitmapWidth, bitmapHeight);
            DrawBackgroundEllipses(g, wordsInCloud.Select(w => w.rectangle), backgroundColor);
            DrawWordsOfCloud(g, color, wordsInCloud);

            bitmap.Save($"{directory}\\{bitmapName}.png", ImageFormat.Png);
        }

        private void DrawBackgroundEllipses(
            Graphics g,
            IEnumerable<Rectangle> rectangles,
            Color backgroundColor)
        {
            var backgroundBrush = new SolidBrush(backgroundColor);
            foreach (var rectangle in rectangles)
                g.FillEllipse(backgroundBrush, rectangle);
        }

        private void DrawBackgroundRectangles(
            Graphics g,
            IEnumerable<Rectangle> rectangles,
            Color backgroundColor)
        {
            var backgroundBrush = new SolidBrush(backgroundColor);
            foreach (var rectangle in rectangles)
                g.FillRectangle(backgroundBrush, rectangle);
        }

        private void DrawBackgroundRectangles(
            Graphics g,
            IEnumerable<Rectangle> rectangles,
            Color backgroundColor,
            Point center)
        {
            var maxDist = (int)rectangles
                .Select(x => GetDistanceFromRectangleToPoint(x, center))
                .Max();

            foreach (var rectangle in rectangles)
            {
                var currentColor = GetColorOfRectangle(rectangle, center, maxDist, backgroundColor);
                g.DrawRectangle(new Pen(currentColor), rectangle);
            }
        }

        private void DrawWordsOfCloud(
            Graphics g,
            Color color,
            List<(string word, Rectangle rectangle, Font font)> wordsInCloud)
        {
            var num = 0;
            foreach (var pair in wordsInCloud)
            {
                var rectangle = pair.rectangle;
                var word = pair.word;
                var brush = new SolidBrush(GetColorOfWord(num, wordsInCloud.Count(), color));

                g.DrawString(word, pair.font, brush, rectangle);
                num++;
            }
        }
        

        private Color GetColorOfRectangle(Rectangle rectangle, Point center, int maxDist, Color color)
        {
            var dist = GetDistanceFromRectangleToPoint(rectangle, center);
            var r = (int)(dist / maxDist * color.R);
            var g = (int)(dist / maxDist * color.G);
            var b = (int)(dist / maxDist * color.B);

            return Color.FromArgb(r, g, b);
        }

        private double GetSmooth(double coefficient)
        {
            return Math.Pow(coefficient, 0.4);
        }

        private Color GetColorOfWord(int num, int count, Color color)
        {
            var r = (int)((GetSmooth((double)num / count)) * color.R);
            var g = (int)((GetSmooth((double)num / count)) * color.G);
            var b = (int)((GetSmooth((double)num / count)) * color.B);

            return Color.FromArgb(r, g, b);
        }

        private double GetDistanceFromRectangleToPoint(Rectangle rectangle, Point center)
        {
            return Math.Sqrt((GetCenterOfRectangle(rectangle).X - center.X) *
                             (GetCenterOfRectangle(rectangle).X - center.X) +
                             (GetCenterOfRectangle(rectangle).Y - center.Y) *
                             (GetCenterOfRectangle(rectangle).Y - center.Y));
        }

        private Point GetCenterOfRectangle(Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}