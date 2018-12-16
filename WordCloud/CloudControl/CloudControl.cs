using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordCloud.LayoutGeneration.Layoter;
using WordCloud.LayoutGeneration.Layoter.Circular;
using WordCloud.LayoutGeneration.Layoter.LinearLayout;
using WordCloud.Properties;
using WordCloud.TextAnalyze.Words;
using WordCloud.WordCloudRenedering;

namespace WordCloud.CloudControl
{
    public class CloudControl : PictureBox
    {
        public ICloudLayouter layouter { get; set; }
        public Vizualizer vizualizer { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }

        public LayoutTypes LayoutType { get; set; } = LayoutTypes.Circular;
        private int maxWeight;
        private int minWight;

        public CloudControl()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void ArrangeLayout(IEnumerable<IWord> words)
        {
            var wordList = words.ToList();
            if (!wordList.Any())
            {
                MessageBox.Show(Resources.CloudControl_Draw_NoWordsError);
                return;
            }

            var items = CreateItems(wordList);
            this.Image = vizualizer.DrawItems(items);
        }

        private void SetMinMaxWeight(IEnumerable<IWord> words)
        {
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
