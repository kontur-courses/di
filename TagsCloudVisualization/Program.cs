using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Program
    {
        private const int ImageHeight = 1000;
        private const int ImageWidth = 1000;

        private const int RectanglesCount = 100;

        private const int MinimalRectangleWidth = 15;
        private const int MaximalRectangleWidth = 100;

        private const int MinimalRectangleHeight = 15;
        private const int MaximalRectangleHeight = 100;

        static void Main()
        {
            var random = new Random();
            var cloudLayouter = new CircularCloudLayouter(new Point(ImageHeight / 2, ImageWidth / 2));

            for (var i = 0; i < RectanglesCount; i++)
                cloudLayouter.PutNextRectangle(new Size(
                    random.Next(MinimalRectangleWidth, MaximalRectangleWidth),
                    random.Next(MinimalRectangleHeight, MaximalRectangleHeight)));

            Console.WriteLine("Enter image file name:");
            var fileName = Console.ReadLine();

            TagCloudVisualizatior.DrawAndSaveAtDocumentFolder(cloudLayouter.GetRectangles(), fileName, ImageHeight, ImageWidth);
        }
    }
}