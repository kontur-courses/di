using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class LayoutVisualization
    {
        private readonly ILayouter layouter;
        private readonly Font baseFont;
        private readonly Pen pen;
        private readonly Dictionary<string, float> fontSizes;
        
        public LayoutVisualization(ILayouter layouter, Font baseFont, Pen pen)
        {
            this.layouter = layouter;
            this.baseFont = baseFont;
            this.pen = pen;
            fontSizes = new Dictionary<string, float>();
        }
        
        private const int scale = 10;

        public Bitmap Visualize(IEnumerable<WordData> words)
        {
            var items = ToTagCloudItems(words.ToList());
            var bitmap = new Bitmap(2 * scale * layouter.LayoutWidth, 2 * scale * layouter.LayoutWidth);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var tagCloudItem in items)
            {
                var newLocation = new Point(tagCloudItem.Rectangle.X + bitmap.Width / 2,
                                            tagCloudItem.Rectangle.Y + bitmap.Height / 2);
                var newRect = new Rectangle(newLocation, tagCloudItem.Rectangle.Size);
                DrawMarking(graphics, bitmap.Width, bitmap.Height);
                graphics.DrawRectangle(pen, newRect);
                DrawWord(graphics, tagCloudItem.Word, newRect);
            }

            return bitmap;
        }

        private IEnumerable<TagCloudItem> ToTagCloudItems(IEnumerable<WordData> words)
        {
            var result = new List<TagCloudItem>();
            var wordDatas = words.ToList();
            var minCount = wordDatas.Select(w => w.Count).Min();
            var maxCount = wordDatas.Select(w => w.Count).Max();
            foreach (var word in wordDatas)
            {
                var size = ResolveSize(word, minCount, maxCount);
                result.Add(layouter.PlaceNextWord(word, size));
            }

            return result;
        }

        private Size ResolveSize(WordData word, int minCount, int maxCount)
        {
            var coefficient = word.Count - minCount == 0 ? 1 : (maxCount - minCount) / (word.Count - minCount);
            var fontSize = baseFont.Size * coefficient;
            fontSizes[word.Word] = fontSize;
            return new Size(Convert.ToInt32(fontSize) * word.Word.Length * scale,
                baseFont.Height * coefficient * scale);
        }

        private void DrawWord(Graphics graphics, string word, Rectangle rectangle)
        {
            using (var font = new Font(baseFont.Name, fontSizes[word], baseFont.Style, baseFont.Unit))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                graphics.DrawString(word, font, Brushes.Black, rectangle, stringFormat);
            }
        }

        private static void DrawMarking(Graphics graphics, int width, int height)
        {
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            var pen = new Pen(Color.Gray);
            graphics.DrawRectangle(pen, new Rectangle(new Point(0, 0),
                new Size(width - 1, height - 1)));
            graphics.DrawLine(pen, new Point(0, height / 2),
                new Point(width, height / 2));
            graphics.DrawLine(pen, new Point(width / 2, 0),
                new Point(width / 2, height));
        }
    }
}