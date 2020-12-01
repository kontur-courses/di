using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public class RectangleLayout : IRectangleLayout
    {
        private readonly IOptions options;
        private readonly ILayouter layouter;
        private readonly ILayoutDrawer drawer;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public RectangleLayout(ILayouter layouter, ILayoutDrawer drawer, IOptions options)
        {
            this.layouter = layouter;
            this.drawer = drawer;
            this.options = options;
            bitmap = new Bitmap(options.Width, options.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void PlaceWords(Dictionary<string, int> words)
        {
            layouter.SetCenter(new Point(options.Width / 2, options.Height / 2));
            foreach (var (word, count) in words)
            {
                var fontSize = CalculateFontSize(count);
                var rectangle = layouter.PutNextRectangle(GetWordSize(word, fontSize, options.FontFamily));
                drawer.AddRectangle(new WordRectangle(rectangle, word, fontSize));
            }
        }

        private Size GetWordSize(string word, int fontSize, string fontFamily)
        {
            return graphics.MeasureString(word, new Font(fontFamily, fontSize)).ToSize();
        }

        private static int CalculateFontSize(int wordCount)
        {
            return (int) (Math.Log(Math.Max(wordCount, 6), 6) * 10);
        }

        public void DrawLayout()
        {
            drawer.Draw(graphics);
        }

        public void SaveLayout()
        {
            var outputDirectory = options.OutputDirectory ?? Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(outputDirectory, options.OutputFileName + options.OutputFileExtension);
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}