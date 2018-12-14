using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;

namespace TagsCloudVisualization
{
    public class TagCloudVisualization:ITagCloudVisualization
    {
        public TagCloudVisualization(
            ICloudLayouter cloudLayouter,
            Font font,
            Color color,
            Color backgroundColor,
            string imageExtension)
        {
            this.imageExtension = imageExtension;
            this.font = font;
            this.color = color;
            this.backgroundColor = backgroundColor;
            this.cloudLayouter = cloudLayouter;
            
            bitmapHeight = 1000;
            bitmapWidth = 1000;
        }
       
        public TagCloudVisualization(
            ICloudLayouter cloudLayouter)
        {
            font = defaultFont;
            color = defaultColor;
            backgroundColor = defaultBackColor;
            this.cloudLayouter = cloudLayouter;
            
            bitmapHeight = 1000;
            bitmapWidth = 1000;
        }

        
        public TagCloudVisualization(
            ICloudLayouter cloudLayouter,
            Font font,
            Color color,
            Color backgroundColor,
            Size size)
        {
            this.font = defaultFont;
            this.color = color;
            this.backgroundColor = backgroundColor;
            this.cloudLayouter = cloudLayouter;
            
            bitmapHeight = size.Height;
            bitmapWidth = size.Width;
        }
        
        private readonly Font font;
        private readonly Color color;
        private readonly Color backgroundColor;
        private readonly ICloudLayouter cloudLayouter;
        private readonly string imageExtension;
        
        private readonly int bitmapWidth;
        private readonly int bitmapHeight;
        
        private readonly Font defaultFont = new Font("Times New Roman", 40);
        private readonly Color defaultColor = Color.Black;
        private readonly Color defaultBackColor = Color.White;

        public void SaveRectanglesCloud(
            string bitmapName, 
            string directory, 
            List<Rectangle> rectangles, 
            Point center)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var g = Graphics.FromImage(bitmap);
            DrawBackgroundRectangles(g, rectangles, center);
            var path = $"{directory}\\{bitmapName}-{rectangles.Count}.{imageExtension}";

            bitmap.Save(path, ImageFormat.Png);
        }

        //ToDo Вынести определение размера шрифта в метод
        public void SaveTagCloud(
            string bitmapName,
            string directory,
            Dictionary<string, int> words)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var g = Graphics.FromImage(bitmap);
            //ToDo вынести из этого класса и убрать ICloudLayouter из конструктора
            var wordsInCloud = new WordsCloudFiller(cloudLayouter, font)
                .GetRectanglesForWordsInCloud(g, words);

            g.FillRectangle(Brushes.White, 0, 0, bitmapWidth, bitmapHeight);
            DrawBackgroundEllipses(g, wordsInCloud.Select(w => w.Value.rectangle));
            DrawWordsOfCloud(g, wordsInCloud);

            var imageFormat = ParseImageFormat(imageExtension);
            if (imageFormat == null)
            {
                Console.WriteLine("Invalid image format. Tag cloud was save in .png");
                imageFormat = ImageFormat.Png;
            }
            bitmap.Save($"{directory}\\{bitmapName}.{imageExtension}", imageFormat);
        }

        private static ImageFormat ParseImageFormat(string str)
        {
            return (ImageFormat)typeof(ImageFormat)
                .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                ?.GetValue(null);
        }

        private void DrawBackgroundEllipses(
            Graphics g,
            IEnumerable<Rectangle> rectangles)
        {
            var backgroundBrush = new SolidBrush(backgroundColor);
            foreach (var rectangle in rectangles)
                g.FillEllipse(backgroundBrush, rectangle);
        }

        private void DrawBackgroundRectangles(
            Graphics g,
            IEnumerable<Rectangle> rectangles)
        {
            var backgroundBrush = new SolidBrush(backgroundColor);
            foreach (var rectangle in rectangles)
                g.FillRectangle(backgroundBrush, rectangle);
        }

        private void DrawBackgroundRectangles(
            Graphics g,
            IEnumerable<Rectangle> rectangles,
            Point center)
        {
            var maxDist = (int)rectangles
                .Select(x => GetDistanceFromRectangleToPoint(x, center))
                .Max();

            foreach (var rectangle in rectangles)
            {
                var currentColor = GetColorOfRectangle(rectangle, center, maxDist);
                g.DrawRectangle(new Pen(currentColor), rectangle);
            }
        }

        private void DrawWordsOfCloud(
            Graphics g,
            Dictionary<string, (Rectangle rectangle, Font font)> wordsInCloud)
        {
            var num = 0;
            foreach (var pair in wordsInCloud)
            {
                var rectangle = pair.Value.rectangle;
                var word = pair.Key;
                var brush = new SolidBrush(GetColorOfWord(num, wordsInCloud.Count));

                g.DrawString(word, pair.Value.font, brush, rectangle);
                num++;
            }
        }
        

        private Color GetColorOfRectangle(Rectangle rectangle, Point center, int maxDist)
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

        private Color GetColorOfWord(int num, int count)
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