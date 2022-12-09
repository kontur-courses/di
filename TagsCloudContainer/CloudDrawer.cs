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

        public void DrawCloud(string path, ICustomOptions options)
        {
            var layout = new CircularCloudLayout(spiralDrawer, new InputOptions(options.PictureSize));
            var size = options.PictureSize;
            var picture = new Bitmap(size, size);
            var g = Graphics.FromImage(picture);
            var backColor = Color.FromName(options.BackgroundColor);
            var fontColor = new SolidBrush(Color.FromName(options.FontColor));
            g.Clear(backColor);
            var wordsFromFile = converter.GetWordsInFile(options);
            var wordsToDraw = calculator.CalculateSize(wordsFromFile, options);

            foreach (var pair in wordsToDraw)
            {
                var stringSize = g.MeasureString(pair.Key, pair.Value);
                if (layout.PutNextRectangle(stringSize, out var buffer))
                    g.DrawString(pair.Key, pair.Value, fontColor, buffer);
            }

            picture.Save(path, ImageFormat.Png);
        }

        public void DrawCloud(ICustomOptions configuration)
        {
            DrawCloud(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Cloud.png"), configuration);
        }
    }
}