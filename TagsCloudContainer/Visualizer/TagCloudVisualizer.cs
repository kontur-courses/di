using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Visualizer
{
    class TagCloudVisualizer : IVisualizer, IDisposable
    {
        private readonly IVisualizerSettings settings;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public TagCloudVisualizer(IVisualizerSettings settings)
        {
            this.settings = settings;
            bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
        }

        private void DrawWords(IList<WordRectangle> wordRectangles)
        {
            foreach (var wordRectangle in wordRectangles)
            {
                var size = wordRectangle.Rectangle.Height;
                var font = new Font(settings.FontFamily, size, settings.FontStyle, GraphicsUnit.Pixel);
                var brush = new SolidBrush(settings.TextColor);
                //graphics.DrawRectangle(new Pen(Color.Green), wordRectangle.Rectangle);
                graphics.DrawString(wordRectangle.Word, font, brush, wordRectangle.Rectangle);
            }
        }

        //private void DrawCenter(Point center)
        //{
        //    var brush = Brushes.Red;
        //    var location = center - new Size(1, 1);
        //    graphics.FillRectangle(brush, new Rectangle(location, new Size(2, 2)));
        //}

        //private void DrawRectangles()
        //{
        //    var brush = Brushes.Green;
        //    graphics.FillRectangles(brush, rectangles.ToArray());
        //    var pen = new Pen(Color.Black);
        //    graphics.DrawRectangles(pen, rectangles.ToArray());
        //}

        //private void Save(string directoryName, string filename)
        //{
        //    var directoryInfo = Directory.GetParent(Environment.CurrentDirectory);
        //    while (directoryInfo != null && directoryInfo.Name != "TagsCloudVisualization")
        //        directoryInfo = directoryInfo.Parent;
        //    if (directoryInfo == null)
        //        throw new DirectoryNotFoundException("Directory with the name TagsCloudVisualization not found");
        //    Directory.CreateDirectory(directoryInfo.FullName + @"\" + directoryName);
        //    bitmap.Save(directoryInfo.FullName + $@"\{directoryName}\{filename}");
        //}

        //public static void SaveNewRectanglesLayout(List<Rectangle> rectangles, string directoryName, string filename)
        //{
        //    var imageSize = VisualizerСalculations.GetOptimalSizeForImage(rectangles, 5);
        //    var center = VisualizerСalculations.GetCenter(imageSize);
        //    var offset = new Size(center);
        //    rectangles = VisualizerСalculations.GetRectanglesWithOptimalLocation(rectangles, offset);
        //    using var visualizer = new TagCloudVisualizer(imageSize, rectangles);
        //    visualizer.DrawRectangles();
        //    visualizer.DrawCenter(center);
        //    visualizer.Save(directoryName, filename);
        //}

        public Bitmap GetImage(IList<WordRectangle> wordRectangles)
        {
            DrawWords(wordRectangles);
            return bitmap;
        }

        public void Dispose()
        {
            bitmap?.Dispose();
            graphics?.Dispose();
        }
    }
}
