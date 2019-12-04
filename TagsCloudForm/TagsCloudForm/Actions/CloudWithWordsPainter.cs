using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudForm.Actions
{
    public class CloudWithWordsPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly CircularCloudLayouterWithWordsSettings settings;
        private readonly Palette palette;
        private Size imageSize;
        private ICircularCloudLayouter layouter;
        private IWordsFrequencyParser parser;
        private SpellCheckerFilter filter;

        public delegate CloudWithWordsPainter Factory(IImageHolder imageHolder,
            CircularCloudLayouterWithWordsSettings settings, Palette palette, ICircularCloudLayouter layouter,
            IWordsFrequencyParser parser, SpellCheckerFilter filter);

        public CloudWithWordsPainter(IImageHolder imageHolder,
            CircularCloudLayouterWithWordsSettings settings, Palette palette, ICircularCloudLayouter layouter,
            IWordsFrequencyParser parser, SpellCheckerFilter filter)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.layouter = layouter;
            this.parser = parser;
            this.filter = filter;
            imageSize = imageHolder.GetImageSize();
        }

        public void Paint()
        {
            var wordsWithFrequency = new Dictionary<string, int>();
            try
            {
                var lines = File.ReadLines(settings.WordsSource);
                wordsWithFrequency = parser.GetWordsFrequency(lines.ToArray(), filter, settings.Language);
            }
            catch (Exception e)
            {
                wordsWithFrequency = new Dictionary<string, int> { { "no_file", 10 } };
                MessageBox.Show(e.Message, "Не удалось загрузить файл");
            }
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width,
                    imageSize.Height);
                var backgroundBrush = new SolidBrush(palette.SecondaryColor);
                var rectBrush = new Pen(palette.PrimaryColor);
                var rng = new Random();
                IOrderedEnumerable<KeyValuePair<string, int>> shuffledWords;
                if (settings.Ordered)
                    shuffledWords = wordsWithFrequency.OrderByDescending(x => x.Value);
                else
                    shuffledWords = wordsWithFrequency.OrderBy(a => rng.Next());
                foreach (var word in shuffledWords)
                {
                    var font = new Font("Arial", Math.Min(72, word.Value * settings.Scale));
                    var size = new Size(TextRenderer.MeasureText(word.Key, font).Width, TextRenderer.MeasureText(word.Key, font).Height);
                    var rect = layouter.PutNextRectangle(size);
                    graphics.FillRectangle(backgroundBrush, rect);
                    graphics.DrawRectangle(rectBrush, rect);
                    graphics.DrawString(word.Key, font, new SolidBrush(palette.PrimaryColor), new PointF(rect.X, rect.Y));
                }
            }

            imageHolder.UpdateUi();
        }
    }
}
