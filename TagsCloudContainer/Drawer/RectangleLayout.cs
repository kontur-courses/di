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
        private readonly List<WordRectangle> rectangles;

        public RectangleLayout(ILayouter layouter, ILayoutDrawer drawer, IOptions options)
        {
            this.layouter = layouter;
            this.drawer = drawer;
            this.options = options;
            rectangles = new List<WordRectangle>();
        }

        public void PlaceWords(Dictionary<string, int> words)
        {
            layouter.SetCenter(options.Width / 2, options.Height / 2);
            foreach (var (word, count) in words)
            {
                var fontSize = CalculateFontSize(count);
                var rectangle = layouter.PutNextRectangle(GetWordSize(word, fontSize, options.FontFamily));
                rectangles.Add(new WordRectangle(rectangle, word, fontSize));
            }

            drawer.AddRectangles(rectangles);
        }

        private Size GetWordSize(string word, int fontSize, string fontFamily)
        {
            return drawer.Graphics.MeasureString(word, new Font(fontFamily, fontSize)).ToSize();
        }

        private static int CalculateFontSize(int wordCount)
        {
            return (int) (Math.Log(Math.Max(wordCount, 6), 6) * 10);
        }

        public void DrawLayout()
        {
            drawer.Draw();
        }

        public void SaveLayout()
        {
            var outputDirectory = options.OutputDirectory ?? Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(outputDirectory, options.OutputFileName + options.OutputFileExtension);
            drawer.Bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}