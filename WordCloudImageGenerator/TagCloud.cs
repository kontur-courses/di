using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Windows.Forms;
using WordCloud.CloudControl;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.LayoutCraetion.Layouters.Circular;
using WordCloudImageGenerator.LayoutCraetion.Layouters.Linear;
using WordCloudImageGenerator.Parsing.Word;

namespace WordCloudImageGenerator
{
    public class TagCloud
    {
        private ICloudLayouter layouter { get; set; }
        public ITagCloudVizualizer vizualizer { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        private Bitmap resultImage;
        
        public LayoutTypes LayoutType { get; set; }
        private int maxWeight;
        private int minWight;

        public TagCloud(WordCloudConfig config, ITagCloudVizualizer vizualizer)
        {
            this.LayoutType = config.LayoutType;
            this.vizualizer = vizualizer;
            this.MaxFontSize = config.MaxFontSize;
            this.MinFontSize = config.MinFontSize;
        }
        public string ArrangeLayout(IEnumerable<IWord> words)
        {
            var wordList = words.ToList();
            var items = CreateItems(wordList);
            this.resultImage = vizualizer.DrawItems(items);
            return SaveImage();
        }

        private string SaveImage()
        {
            var fileName = "cloud.png";
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
            using (resultImage)
                resultImage.Save(fullPath, ImageFormat.Png);

            return fullPath;
        }
        private void SetMinMaxWeight(IEnumerable<IWord> words)
        {
            if (!words.Any())
                return;
            maxWeight = words.Select(w => w.Entries).Max();
            minWight = words.Select(w => w.Entries).Min();
        }

        private Size Measure(string text, int weight)
        {
            Font font = GetFont(weight);
            return TextRenderer.MeasureText(text, font);
        }

        private Font GetFont(int weight)
        {
            float fontSize =
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
                var rectangle = layouter.PutNextRectangle(rectSize);
                rectsToScale.Add(rectangle);
            }

            var backgroundSize = GetBackgroundSize(rectsToScale);

            var mainFrame = new Rectangle(new Point(0, 0), backgroundSize);

            SetUpLayouter(mainFrame.GetRectangleCenter());

            var scaledRectangles = new List<Rectangle>();
            foreach (var tempRectangle in rectsToScale)
            {
                var rect = layouter.PutNextRectangle(tempRectangle.Size);
                scaledRectangles.Add(rect);
            }

            var items = new List<CloudItem>();
            items.AddRange(
                wordList.Select((word, i) => new CloudItem(scaledRectangles[i], word, GetFont(word.Entries))));
            return items;
        }

        private void SetUpLayouter(Point center)
        {
            switch (this.LayoutType)
            {
                case LayoutTypes.Circular:
                    this.layouter = new CircularCloudLayouter(center);
                    break;
                case LayoutTypes.Orthogonal:
                    this.layouter = new LinearLayouter();
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