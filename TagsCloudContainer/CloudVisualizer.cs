using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private readonly IEnumerable<KeyValuePair<string,int>> wordsWithFontSize;
        private readonly CircularCloudLayouter layouter = new CircularCloudLayouter(new Point(1920 / 2, 1080 / 2));
        private readonly CloudOptions options;

        public CloudVisualizer(ICloudConfigurator configurator, CloudOptions options)
        {
            this.options = options;
            wordsWithFontSize = configurator.ConfigureWords();
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
            var size = new Size(int.Parse(options.Width), int.Parse(options.Height));
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);
            foreach (var word in wordsWithFontSize)
            {
                DrawWord(word.Key, word.Value, graphics);
            }

            return bitmap;
        }

        private void DrawWord(string word, int fontSize, Graphics graphics)
        {
            var font = new Font(options.FontFamily, fontSize);
            var size = graphics.MeasureString(word, font);
            var intSize = new Size((int)size.Width + 1, (int)size.Height + 1);
            var rectangle = layouter.PutNextRectangle(intSize);
            graphics.DrawString(word, font, new SolidBrush(Color.FromName(options.Color)), rectangle);
        }
    }
}
