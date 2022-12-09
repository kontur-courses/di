using System.Drawing;
using System.Drawing.Imaging;
using CloudLayout;

namespace TagsCloudContainer
{
    public class CloudDrawer
    {
        private readonly ISpiralDrawer spiralDrawer;
        private readonly IConverter converter;
        private readonly IWordSizeCalculator calculator;

        public CloudDrawer(ISpiralDrawer spiralDrawer, IConverter converter,
            IWordSizeCalculator calculator)
        {
            this.spiralDrawer = spiralDrawer;
            this.converter = converter;
            this.calculator = calculator;
        }

        public void DrawCloud(string path, IMyConfiguration configuration)
        {
            var layout = new CircularCloudLayout(spiralDrawer, new InputOptions(configuration.PictureSize));
            var size = configuration.PictureSize;
            var picture = new Bitmap(size, size);
            var g = Graphics.FromImage(picture);
            var backColor = Color.FromName(configuration.BackgroundColor);
            var fontColor = new SolidBrush(Color.FromName(configuration.FontColor));
            g.Clear(backColor);
            var wordsFromFile = converter.GetWordsInFile(configuration);
            var wordsToDraw = calculator.CalculateSize(wordsFromFile, configuration);

            foreach (var pair in wordsToDraw)
            {
                var stringSize = g.MeasureString(pair.Key, pair.Value);
                if (layout.PutNextRectangle(stringSize, out var buffer))
                    g.DrawString(pair.Key, pair.Value, fontColor, buffer);
            }

            picture.Save(path, ImageFormat.Png);
        }

        public void DrawCloud(IMyConfiguration configuration)
        {
            DrawCloud(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Cloud.png"), configuration);
        }
    }
}