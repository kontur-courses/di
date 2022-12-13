using System.Drawing;
using TagsCloudContainer.Core.Options;
using TagsCloudContainer.Core.Layouter;
using TagsCloudContainer.Core.Drawer.Interfaces;
using TagsCloudContainer.Core.Options.Interfaces;
using TagsCloudContainer.Core.Layouter.Interfaces;

namespace TagsCloudContainer.Core.Drawer
{
    public class RectangleLayout : IRectangleLayout
    {
        private readonly ILayouter _layouter;
        private readonly ILayoutDrawer _drawer;
        private readonly IImageOptions _imageOptions;

        private readonly Bitmap _bitmap;
        private readonly Graphics _graphics;
        private readonly FontOptions _fontOptions;

        public RectangleLayout(ILayouter layouter, ILayoutDrawer drawer, IImageOptions imageOptions, FontOptions fontOptions)
        {          
            _drawer = drawer;
            _layouter = layouter;
            _fontOptions = fontOptions;
            _imageOptions = imageOptions;

            _bitmap = new Bitmap(imageOptions.Width, imageOptions.Height);
            _graphics = Graphics.FromImage(_bitmap);
        }

        public void PlaceWords(Dictionary<string, int> words)
        {
            _layouter.SetCenter(new Point(_imageOptions.Width / 2, _imageOptions.Height / 2));
            foreach (var (word, count) in words)
            {
                var fontSize = CalculateFontSize(count);
                var rectangle = _layouter.PutNextRectangle(GetWordSize(word, fontSize, _fontOptions.FontFamily));

                _drawer.AddRectangle(new WordRectangle(rectangle, word, fontSize, _fontOptions.FontFamily, _fontOptions.FontColor));
            }
        }

        private Size GetWordSize(string word, int fontSize, string fontFamily)
        {
            return _graphics.MeasureString(word, new Font(fontFamily, fontSize)).ToSize();
        }

        private static int CalculateFontSize(int wordCount)
        {
            return (int)(Math.Log(Math.Max(wordCount, 6), 6) * 10);
        }

        public void DrawLayout()
        {
            _drawer.Draw(_graphics);
        }

        public void SaveLayout()
        {
            var outputDirectory = _imageOptions.ImageOutputDirectory;
            var fullPath = Path.Combine(outputDirectory, _imageOptions.ImageName + _imageOptions.ImageExtension);

            _bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}
