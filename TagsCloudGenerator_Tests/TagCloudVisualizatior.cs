using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TagsCloudGenerator_Tests
{
    internal class TagCloudVisualizatior
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        private readonly Random randomize;
        private int RandomColorIndex => 
            randomize.Next(0, colorsPalette.Length - 1);

        private readonly SolidBrush[] colorsPalette =
        {
            new SolidBrush(Color.Green),
            new SolidBrush(Color.Blue),
            new SolidBrush(Color.Red),
            new SolidBrush(Color.Orchid), 
            new SolidBrush(Color.Orange), 
            new SolidBrush(Color.PapayaWhip), 
        };

        private readonly Pen Outliner = new Pen(Color.Wheat);

        public TagCloudVisualizatior(int height, int width)
        {
            if(height <= 0 || width <= 0)
                throw new ArgumentException($"Height or width can't be less or equal zero.");

            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);

            randomize = new Random();
        }

        public void Draw(Rectangle rectangle, Brush brush = null)
        {
            graphics.FillRectangle(brush ?? colorsPalette[RandomColorIndex], rectangle);
            graphics.DrawRectangle(Outliner, rectangle);
        }

        public void SaveAsPng(string path, string fileName)
        {
            Directory.CreateDirectory(path);
            bitmap.Save(Path.Combine(path, fileName + ".png"));
        }

        public static void DrawAndSave(IEnumerable<Rectangle> rectangles, 
                                        string path, string name,
                                        int height, int width)
        {
            var image = new TagCloudVisualizatior(height, width);
            foreach (var item in rectangles)
                image.Draw(item);
            image.SaveAsPng(path, name);
        }

        public static void DrawAndSaveAtDocumentFolder(IEnumerable<Rectangle> rectangles,
                                                        string name, int height, int width)
        {
            var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TagCloudImagesDebug");
            DrawAndSave(rectangles, documentsPath, name, height, width);
        }

    }
}