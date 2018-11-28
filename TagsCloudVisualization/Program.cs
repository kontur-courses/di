using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        private const int CountOfRectangles = 100;
        private const int MaxSizeOfRectangle = 100;
        private const int MinSizeOfRectangle = 10;

        public static void Main()
        {
            var center = new Point(2000, 2000);
            var spiral = new ArchimedesSpiral(center);

            var directory = Environment.CurrentDirectory;

            var cloud = new CloudLayouter(spiral);
            FillCloudWithRectangles(cloud);

            var visualization = new CircularCloudVisualization(cloud);
            visualization.SaveCloudLayouter("cloud", directory);
        }

        private static void FillCloudWithRectangles(ICloudLayouter cloud)
        {
            var rnd = new Random();
            for (var i = 0; i < CountOfRectangles; i++)
            {
                var width = rnd.Next(MinSizeOfRectangle * 10, MaxSizeOfRectangle * 10);
                var height = rnd.Next(MinSizeOfRectangle, MaxSizeOfRectangle);

                var size = new Size(width, height);

                cloud.PutNextRectangle(size);
            }
        }
    }
}
