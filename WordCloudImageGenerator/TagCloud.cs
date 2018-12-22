using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Windows.Forms;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.Layouting.Layouters;
using WordCloudImageGenerator.Layouting.Layouters.Circular;
using WordCloudImageGenerator.Layouting.Layouters.Linear;
using WordCloudImageGenerator.Parsing.Word;

namespace WordCloudImageGenerator
{
    public class TagCloud
    {
        private ICloudLayouter Layouter { get; set; }
        private ITagCloudVizualizer Vizualizer { get; }
        private int MinFontSize { get; }
        private int MaxFontSize { get; }
        private LayoutTypes LayoutType { get; }
        private int maxWeight;
        private int minWight;
        private Bitmap resultImage;

        public TagCloud(WordCloudConfig config, ITagCloudVizualizer vizualizer)
        {
            LayoutType = config.LayoutType;
            Vizualizer = vizualizer;
            MaxFontSize = config.MaxFontSize;
            MinFontSize = config.MinFontSize;
        }
        public string ArrangeLayout(IEnumerable<IWord> words)
        {
            var wordList = words.ToList();
            var items = CreateItems(wordList);
            resultImage = Vizualizer.DrawItems(items);
            return SaveImage();
        }

        private string SaveImage()
        {
            const string fileName = "cloud.png";
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
            using (resultImage)
                resultImage.Save(fullPath, ImageFormat.Png);

            return fullPath;
        }
        private void SetMinMaxWeight(IEnumerable<IWord> words)
        {
            IEnumerable<IWord> wordArray = words as IWord[] ?? words.ToArray();
            if (!wordArray.Any())
                return;
            maxWeight = wordArray.Select(w => w.Entries).Max();
            minWight = wordArray.Select(w => w.Entries).Min();
        }

        private Size Measure(string text, int weight)
        {
            var font = GetFont(weight);
            return TextRenderer.MeasureText(text, font);
        }

        private Font GetFont(int weight)
        {
            var fontSize =
                (float) (weight - minWight) / (maxWeight - minWight) * (MaxFontSize - MinFontSize) + MinFontSize;
            if (double.IsNaN(fontSize))
                fontSize = MinFontSize;
            return new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
        }

        private List<CloudItem> CreateItems(List<IWord> wordList)
        {
            SetUpLayouter(new Point(0, 0));
            SetMinMaxWeight(wordList);

            var rectsToScale = new List<Rectangle>();
            foreach (var word in wordList)
            {
                var rectSize = Measure(word.Text, word.Entries);
                var rectangle = Layouter.PutNextRectangle(rectSize);
                rectsToScale.Add(rectangle);
            }

            var backgroundSize = GetBackgroundSize(rectsToScale);

            var mainFrame = new Rectangle(new Point(0, 0), backgroundSize);

            SetUpLayouter(mainFrame.GetRectangleCenter());

            var scaledRectangles = new List<Rectangle>();
            foreach (var tempRectangle in rectsToScale)
            {
                var rect = Layouter.PutNextRectangle(tempRectangle.Size);
                scaledRectangles.Add(rect);
            }

            var items = new List<CloudItem>();
            items.AddRange(
                wordList.Select((word, i) => new CloudItem(scaledRectangles[i], word, GetFont(word.Entries))));
            return items;
        }

        private void SetUpLayouter(Point center)
        {
            switch (LayoutType)
            {
                case LayoutTypes.Circular:
                    this.Layouter = new CircularCloudLayouter(center);
                    break;
                case LayoutTypes.Linear:
                    this.Layouter = new LinearLayouter();
                    break;
                default:
                    throw new ArgumentException($"Cannot resolve {LayoutType}.");
            }
        }

        private Size GetBackgroundSize(IEnumerable<Rectangle> rectangles)
        {
            var rectsList = rectangles.ToList();
            var maxX = rectsList.Select(r => r.Right).Max();
            var maxY = rectsList.Select(r => r.Top).Max();

            var minX = rectsList.Select(r => r.Left).Min();
            var minY = rectsList.Select(r => r.Bottom).Min();

            var width = Math.Sqrt(Math.Pow((maxX - minX), 2));
            var height = Math.Sqrt(Math.Pow((maxY - minY), 2));

            Size sizeBackground = new Size((int) width, (int) height);
            return sizeBackground;
        }
    }
}