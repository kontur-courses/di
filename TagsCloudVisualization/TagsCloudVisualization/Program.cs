using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal class Program
    {
        internal static void Main()
        {
            DrawRectangles(100, 100, 1);
            DrawRectangles(50, 50, 2);
            DrawRectangles(30, 30, 3);
        }

        private static void DrawRectangles(int widthMax, int heightMax, int numberOfPicture)
        {
            var center = new Point(0, 0);
            var cloudLayouter = new CircularCloudLayouter(center);

            for (var width = 5; width < widthMax; width += 5)
            {
                for (var height = 5; height < heightMax; height += 5)
                {
                    cloudLayouter.PutNextRectangle(new Size(width, height));
                }
            }

            var workingDirectory = Environment.CurrentDirectory;
            var fullPath = workingDirectory + "\\cloud" + numberOfPicture + ".bmp";
            TagDrawer.Draw(fullPath, cloudLayouter);
            Console.WriteLine("rectangle saved to: " + fullPath);
        }
    }
}