using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        static Random random = new Random();

        static int imageWidth = 1000;
        static int imageHeight = 1000;

        static string imageName = "TestForMe";

        static int countRectangles = 100;

        static int maxRectangleWidth = 100;
        static int minRectangleWidth = 50;
        static int maxRectangleHeight = 50;
        static int minRectangleHeight = 20;

        public static void Main()
        {
            var canvas = new Canvas(imageWidth, imageHeight);
            var cloudLayouter = new CircularCloudLayouter(new Point(imageWidth / 2, imageHeight / 2));

            for (int i = 0; i < countRectangles; i++)
            {
                var rectangleRandomSize = new Size(
                    random.Next(minRectangleWidth, maxRectangleWidth),
                    random.Next(minRectangleHeight, maxRectangleHeight));
                canvas.Draw(cloudLayouter.PutNextRectangle(rectangleRandomSize));
            }

            canvas.Save(imageName);
        }
    }
}
