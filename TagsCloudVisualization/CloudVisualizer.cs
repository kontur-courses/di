using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CloudVisualizer
    {
        private readonly Point center;
        private readonly string path;
        private readonly string imageName;
        private readonly Size imageSize;
        private readonly Bitmap image;
        private readonly List<Rectangle> centeredRectangles;
        private readonly Dictionary<string, int> wordsFrequency;
        private readonly List<Rectangle> rectangles;
        private readonly Graphics gr;

        public CloudVisualizer(
            Point center,
            CircularCloudLayouter layouter,
            string path,
            string imageName,
            Dictionary<string, int> wordsFrequency)
        {
            this.center = center;
            this.path = path;
            this.imageName = imageName;
            this.wordsFrequency = wordsFrequency;
            image = new Bitmap(700, 700);
            gr = Graphics.FromImage(image);
            rectangles = GetRectangles(layouter);
            centeredRectangles = CenterRectangles();
        }

        public void CreateImage()
        {
            DrawWords();
            image.Save($"{path}{imageName}.png", ImageFormat.Png);
        }

        private void DrawWords()
        {
            double minFrequency = wordsFrequency.Min(x => x.Value);
            double maxFrequency = wordsFrequency.Max(x => x.Value);
            var random = new Random();
            var c = -1;
            foreach (var wordFreqPair in wordsFrequency.OrderByDescending(x => x.Value))
            {
                c++;
                var rectangle = rectangles[c];
                var font = GetFont(12, 50, minFrequency, maxFrequency, wordFreqPair.Value);
                var stringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };

                var brush = new SolidBrush(GetRandomColor(random));
                gr.DrawString(wordFreqPair.Key, font, brush, rectangle, stringFormat);
            }   
        }

        private Font GetFont(int minSize, int maxSize, double minFrequency, double maxFrequency, double wordFrequency)
        {
            var fontSize = (int)(minSize + (maxSize - minSize) * (wordFrequency - minFrequency) / (maxFrequency - minFrequency));
            return new Font(FontFamily.GenericSansSerif, fontSize);
        }

        private Point GetCenteredRectangleCoordinates(Rectangle rectangle)
        {
            return new Point(imageSize.Width / 2 + rectangle.X - center.X,
                imageSize.Height / 2 + rectangle.Y - center.Y);
        }

        private List<Rectangle> CenterRectangles()
        {
            return rectangles.Select(rectangle => new Rectangle(
                GetCenteredRectangleCoordinates(rectangle), rectangle.Size)).ToList();
        }

        private static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255),
                (byte)random.Next(0, 255));
        }

        private Size GetWordSize(string word, Font font)
        {
            var wordSize = gr.MeasureString(word, font);
            var width = (int)Math.Ceiling(wordSize.Width);
            var height = (int)Math.Ceiling(wordSize.Height);
            
            return new Size(width, height);
        }

        private List<Rectangle> GetRectangles(CircularCloudLayouter layouter)
        {
            double minFrequency = wordsFrequency.Min(x => x.Value);
            double maxFrequency = wordsFrequency.Max(x => x.Value);
            var rectangleSizes = new List<Size>();

            foreach (var el in wordsFrequency)
            {
                var font = GetFont(12, 50, minFrequency, maxFrequency, el.Value);
                rectangleSizes.Add(GetWordSize(el.Key, font));
            }
            return layouter.GetRectangles(rectangleSizes.OrderByDescending(size => size.Width*size.Height).ToList());
        }
    }
}