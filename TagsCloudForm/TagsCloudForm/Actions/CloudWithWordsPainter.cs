using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Dictionary<string, int> words;
        private IWordsFrequencyParser parser;

        public CloudWithWordsPainter(IImageHolder imageHolder,
            CircularCloudLayouterWithWordsSettings settings, Palette palette, ICircularCloudLayouter layouter,
            Dictionary<string, int> words, IWordsFrequencyParser parser)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.layouter = layouter;
            this.words = words;
            this.parser = parser;
            imageSize = imageHolder.GetImageSize();
        }

        public void Paint()
        {
            var wordsWithFrequency = new Dictionary<string, int>();
            try
            {
                var lines = File.ReadLines(settings.WordsSource);
                wordsWithFrequency = parser.GetWordsFrequency(lines.ToArray());

            }
            catch (Exception e)
            {
                wordsWithFrequency = words;
                MessageBox.Show(e.Message, "Не удалось загрузить файл");
            }
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width,
                    imageSize.Height);
                var backgroundBrush = new SolidBrush(palette.SecondaryColor);
                var rectBrush = new Pen(palette.PrimaryColor);
                foreach (var word in wordsWithFrequency)
                {
                    var font = new Font("Arial", Math.Min(72, word.Value * 5));
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
