using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rectanglesSizes = RectangleSizeGenerator.GetConstantSizes(1000, new Size(25, 10));
            GenerateAndSaveCloudImage(rectanglesSizes, "Equivalent_rectangles_cloud.png");

            rectanglesSizes = RectangleSizeGenerator.GetRandomOrderedSizes(300, new Size(20, 10), new Size(80, 40));
            GenerateAndSaveCloudImage(rectanglesSizes, "Horizontal_rectangles_cloud.png");

            rectanglesSizes = RectangleSizeGenerator.GetRandomOrderedSizes(300, new Size(10, 20), new Size(40, 80));
            GenerateAndSaveCloudImage(rectanglesSizes, "Vertical_rectangles_cloud.png");
        }


        private static void GenerateAndSaveCloudImage(IEnumerable<Size> rectanglesSizes, string fileName)
        {
            var layouter = new CircularCloudLayouter(new Point(0, 0));

            var imageGenerator = new CloudImageGenerator(layouter, Color.Black);

            var bitmap = imageGenerator.GenerateBitmap(rectanglesSizes);

            ImageSaver.SaveBitmapInSolutionSubDirectory(bitmap, "TagCloudImages", fileName);
        }
    }
}
