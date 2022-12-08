using System.Drawing;
using System.Drawing.Imaging;
using CloudLayout;

namespace TagsCloudContainer
{
    public class CloudDrawer
    {
        private readonly int size;
        private readonly ILayout layout;
        private readonly IConverter converter;
        private readonly IWordSizeCalculator calculator;
        private readonly IMyConfiguration configuration;

        public CloudDrawer(IMyConfiguration configuration, ILayout layout, IConverter converter,
            IWordSizeCalculator calculator)
        {
            size = configuration.PictureSize;
            this.layout = layout;
            this.converter = converter;
            this.calculator = calculator;
            this.configuration = configuration;
        }

        public void DrawCloud(string path)
        {
            var picture = new Bitmap(size, size);
            var g = Graphics.FromImage(picture);
            var backColor = Color.FromName(configuration.BackgroundColor);
            var fontColor = new SolidBrush(Color.FromName(configuration.FontColor));
            g.Clear(backColor);
            var wordsFromFile = converter.GetWordsInFile();
            var wordsToDraw = calculator.CalculateSize(wordsFromFile);

            foreach (var pair in wordsToDraw)
            {
                var stringSize = g.MeasureString(pair.Key, pair.Value);
                if (layout.PutNextRectangle(stringSize, out var buffer))
                    g.DrawString(pair.Key, pair.Value, fontColor, buffer);
            }

            picture.Save(path, ImageFormat.Png);
        }

        public void DrawCloud()
        {
            DrawCloud(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Cloud.png"));
        }
    }
}