using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private readonly Dictionary<string, int> words;
        private readonly CircularCloudLayouter layouter = new CircularCloudLayouter(new Point(990, 510));

        public CloudVisualizer(IWordsPreprocessor preprocessor)
        {
            words = preprocessor.Prepare();
        }

        public void VisualizeCloud()
        {
            var bitmap = RenderToBitmap();
            var imageFormat = ImageFormat.Png;
            var filename = $"wordsCloud.{imageFormat.ToString().ToLower()}";

            bitmap.Save(filename, imageFormat);
        }

        private Bitmap RenderToBitmap()
        {
            var size = new Size(1980, 1020);
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);
            if (words.Any())
            {
                foreach (var word in words)
                {
                    DrawWord(word.Key, word.Value, graphics);
                }
            }
                
            return bitmap;
        }

        private void DrawWord(string word, int frequency, Graphics graphics)
        {
            var font = new Font("Arial", 16 + 2 * frequency);
            var size = graphics.MeasureString(word, font);
            var intSize = new Size((int)size.Width + 1, (int)size.Height + 1);
            var rectangle = layouter.PutNextRectangle(intSize);
            graphics.DrawString(word, font, Brushes.DarkRed, rectangle);
        }
    }
}
