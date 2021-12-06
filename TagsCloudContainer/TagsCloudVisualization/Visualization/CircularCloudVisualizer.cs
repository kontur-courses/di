using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization.Visualization
{
    #pragma warning disable CA1416
    public class CircularCloudVisualizer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly string path;
        private readonly Pen pen;

        public CircularCloudVisualizer(
            int imageWidth,
            int imageHeight,
            Color penColor,
            string imageSavingPath = "../../../TagsCloudVisualizations/")
        {
            path = imageSavingPath;
            Directory.CreateDirectory(imageSavingPath);
            
            bitmap = new Bitmap(imageWidth, imageHeight);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(penColor);
        }

        public void PutRectangle(RectangleF rectangle)
        {
            graphics.DrawRectangles(pen, new[] {rectangle});
        }

        public void PutRectangles(IEnumerable<RectangleF> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                PutRectangle(rectangle);
            }
        }

        public void PutWordInRectangle(string word, RectangleF rectangle)
        {
            var wordSize = (int) rectangle.Width / word.Length;
            graphics.DrawString(word, new Font(FontFamily.GenericSansSerif, wordSize), pen.Brush, rectangle);
        }

        public void PutWordsInRectangles(IEnumerable<string> words, IEnumerable<RectangleF> rectangles)
        {
            var wordsArray = words as string[] ?? words.ToArray();
            var rectanglesArray = rectangles as RectangleF[] ?? rectangles.ToArray();
            
            if (wordsArray.Length != rectanglesArray.Length)
            {
                throw new ArgumentException("Texts and Rectangles counts should be the same");
            }

            foreach (var tuple in wordsArray.Zip(rectanglesArray))
            {
                PutWordInRectangle(tuple.First, tuple.Second);
            }
        }

        public string SaveImage(string imageName="TagsCloudVisualization.png")
        {
            var savingPath = path + imageName;
            bitmap.Save(savingPath);
            return savingPath;
        }
    }
}