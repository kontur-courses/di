using System;
using System.Diagnostics;
using System.Drawing;

namespace TagCloudVisualisation
{ 
    public class Program
    {
        private static readonly Random random = new();
        public static void Main()
        {
            var folder = "RenderedPictures";
            System.IO.Directory.CreateDirectory(folder);
            var size = new Size(1000, 1000);
            GeneratePicture(folder, 20, "1", 20, 150, size);
            GeneratePicture(folder, 400, "2", 4, 40, size);
            GeneratePicture(folder, 5000, "3", 2, 5, size);
            var folderPath = Environment.CurrentDirectory + "\\" + folder;
            Process.Start("explorer.exe", folderPath);
        }

        private static void GeneratePicture(string folder, int amount, string name, int minRectSize, int maxRectSize, Size bitmapSize)
        {
            var CCL = new CircularCloudLayouter(new ArchimedeanSpiral(new Point(500, 500)));
            for (int i = 0; i < amount; i++)
            {
                CCL.PutNewRectangle(new Size(random.Next(minRectSize, maxRectSize), random.Next(minRectSize, maxRectSize)));
            }

            BitmapSaver.SaveRectangleRainbowBitmap(CCL.GetRectangles(), folder + @"\" + name + ".bmp", bitmapSize);
        }
    }
}
