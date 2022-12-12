using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using CloudLayout;
using CloudLayout.Interfaces;
using TagsCloudContainer.Interfaces;

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

            picture.Save(path, GetImageFormat(options.ImageFormat));
        }

        public void DrawCloud(ICustomOptions options)
        {
            DrawCloud(Path.Combine(options.WorkingDir, string.Concat("Cloud.", options.ImageFormat.ToLower())), options);
        }
        private static ImageFormat GetImageFormat(string format)
        {
            return format.ToLower() switch
            {
                "png" => ImageFormat.Png,
                "heif" => ImageFormat.Heif,
                "bmp" => ImageFormat.Bmp,
                "emf" => ImageFormat.Emf,
                "exif" => ImageFormat.Exif,
                "gif" => ImageFormat.Gif,
                "icon" => ImageFormat.Icon,
                "jpeg" => ImageFormat.Jpeg,
                "memorybmp" => ImageFormat.MemoryBmp,
                "tiff" => ImageFormat.Tiff,
                "webp" => ImageFormat.Webp,
                "wmf" => ImageFormat.Wmf,
                _ => throw new ArgumentException("Unexpected image format")
            };
        }
    }
}