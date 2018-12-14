using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordCloud.LayoutGeneration.Layoter;
using WordCloud.Properties;
using WordCloud.TextAnalyze.Words;
using WordCloud.WordCloudRenedering;

namespace WordCloud
{
    public class CloudControl : PictureBox
    {
        private IEnumerable<Brush> palette;
        private ICloudLayouter layouter;
        private int maxWeight;
        private int minWight;

        private int minFontSize = 8;
        private int maxFontSize = 28;

       
        public CloudControl(IEnumerable<Brush> palette, ICloudLayouter layouter)
        {
            this.palette = palette;
            this.layouter = layouter;            
        }
       
        public void Draw(IEnumerable<IWord> words)
        {
            var wordList = words.ToList();
            if (!wordList.Any())
            {
                MessageBox.Show(Resources.CloudControl_Draw_NoWordsError);
                return;
            }

            layouter.Reset();
            SetMinMaxWeight(wordList);
            var items = new List<CloudControlItem>();

            var tempRectangles = new List<Rectangle>();

            foreach (var word in wordList)
            {
                var rectSize = Measure(word.Text, word.Entries);
                var rectangle = layouter.PutNextRectangle(rectSize);
                tempRectangles.Add(rectangle);
            }
            var backgroundSize = GetBackgroundSize(tempRectangles);
            var mainFrame = new Rectangle(new Point(0, 0), backgroundSize);
            tempRectangles = ScaleRectangles(tempRectangles, mainFrame);

            items.AddRange(wordList.Select((word, i) => new CloudControlItem(tempRectangles[i], word)));

            var image = DrawImage(items);
            this.Image = image;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public void SetMinFontSize(int fontSize)
        {
            this.minFontSize = fontSize;
        }
        public void SetMaxFontSize(int fontSize)
        {
            this.maxFontSize = fontSize;
        }

        private Bitmap DrawImage(List<CloudControlItem> items)
        {
            var backgroundSize = GetBackgroundSize(items.Select(item => item.Rectangle).ToList());
            var mainFrame = new Rectangle(new Point(0, 0), backgroundSize);
            var image = new Bitmap(backgroundSize.Width, backgroundSize.Height);
            var graphics = Graphics.FromImage(image);
            var pen = new Pen(Color.Black, 1);
            graphics.FillRectangle(Brushes.Wheat, mainFrame);

            var random = new Random();
            var colorList = palette.ToList();

            foreach (var item in items)
            {
                var numberColor = random.Next(0, palette.Count());

                var brush = colorList[numberColor];
                var rect = item.Rectangle;
                var text = item.Word.Text;
                graphics.FillRectangle(brush, rect);
                graphics.DrawRectangle(pen, rect);

                Font font = GetFont(item.Word.Entries);
                PointF point = new PointF(rect.X, rect.Y);
                graphics.DrawString(text, font, Brushes.Black, point);
            }

            return image;
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
                (float)(weight - minWight) / (maxWeight - minWight) * (maxFontSize- minFontSize) + minFontSize;
            if (double.IsNaN(fontSize))
                fontSize = minFontSize;
            return new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
        }

        private List<Rectangle> ScaleRectangles(IEnumerable<Rectangle> rectangles, Rectangle mainFrame)
        {
            var rects = new List<Rectangle>();

            var mainFrameCenter = mainFrame.GetRectangleCenter();
            var enumerable = rectangles.ToList();
            var first = enumerable.First();
            var firstRectangleCenter = first.GetRectangleCenter();
            var deltaY = mainFrameCenter.Y - firstRectangleCenter.Y;
            var deltaX = mainFrameCenter.X - firstRectangleCenter.X;


            foreach (var rectangle in enumerable)
            {
                var offsetY = deltaY - first.Height / 2;
                var offsetX = deltaX - first.Width/ 2;
                rectangle.Offset(offsetX, offsetY);
                rects.Add(rectangle);
            }

            return rects;
        }

        private Size GetBackgroundSize(IEnumerable<Rectangle> rectangles)
        {
            var qwe = rectangles.ToList();
            var max_X = qwe.Select(r => r.Right).Max();
            //оч странно
            var max_Y = qwe.Select(r => r.Bottom).Max();

            var min_X = qwe.Select(r => r.Left).Min();
            var min_Y = qwe.Select(r => r.Top).Min();


           
            var width = Math.Sqrt(Math.Pow((max_X - min_X), 2));
            var height = Math.Sqrt(Math.Pow((max_Y - min_Y), 2));

            Size sizeBackground = new Size((int)width, (int)height);
            return sizeBackground;
        }
    }
}
