using System;
using System.Drawing;

namespace TagsCloudLayout
{
    public static class Program
    {
        public static Random rnd = new Random();

        public static void Main()
        {
            var center = new Point(0, 0);

            var bitmap = RectangleVisualizator.GetBitmapFromRectangles(center, 
                CloudLayouterUtilities.GenerateRandomLayout(center, rnd.Next(80, 150),25, 75, 25, 75));
            bitmap.Save($"1_RandomLayout.bmp");
            bitmap = RectangleVisualizator.GetBitmapFromRectangles(center, 
                CloudLayouterUtilities.GenerateRandomLayout(center, rnd.Next(80, 150), 25, 25, 20, 50));
            bitmap.Save($"2_Strecthed_rectangles.bmp");
            bitmap = RectangleVisualizator.GetBitmapFromRectangles(center, 
                CloudLayouterUtilities.GenerateRandomLayout(center, rnd.Next(80, 150), 50, 50, 50, 50));
            bitmap.Save($"3_Squares.bmp");
        }
    }
}
