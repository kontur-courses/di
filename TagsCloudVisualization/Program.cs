using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloud;

namespace TagsCloudVisualization
{
    static class Program
    {
        private static int rectCount = 100;
        private static int maxRectWidth = 80;
        private static int maxRectHeight = 100;
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var pointGenerator = new ArchimedesSpiral(new Point(50, 50));
            var cloudLayouter = new CircularCloudLayouter(pointGenerator);
            var rectanglesToAdd = GetSortedRectanglesToAdd(rectCount, maxRectWidth, maxRectHeight);
            var cloudRectangles = rectanglesToAdd
                .Select(rectangleSize => cloudLayouter.PutNextRectangle(rectangleSize)).ToList();
            
            TagCloudVisualizer.PrintTagCloud(cloudRectangles, cloudLayouter.Center, 
                maxRectWidth, maxRectHeight, 
                fileName: $"TagCloudWith{rectCount}Rects");
        }
        
        private static IEnumerable<Size> GetSortedRectanglesToAdd(int count, int maxWidth, int maxHeight)
        {
            var rand = new Random();
            var rectangleSizes = new List<Size>();

            for (var i = 0; i < count; i++)
            {
                var width = rand.Next(1, maxWidth);
                var height = rand.Next(1, maxHeight);
                rectangleSizes.Add(new Size(width, height));
            }

            return rectangleSizes.OrderByDescending(rect => rect.Width * rect.Height);
        }
    }
}