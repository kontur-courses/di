using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.CloudLayouters.CircularCloudLayouter
{
    public class CircularCloudLayouterVisualizer
    {
        public void SaveImage(IEnumerable<Rectangle> rectangles, string path)
        {
            var bitmap = new Bitmap(700, 700);
            var enumerable = rectangles.ToList();
            var minX = enumerable.OrderBy(rect => rect.X).First().X;
            var minY = enumerable.OrderBy(rect => rect.Y).First().Y;
            var maxX = enumerable.OrderBy(rect => rect.Right).Last().Right;
            var maxY = enumerable.OrderBy(rect => rect.Bottom).Last().Bottom;
            var xDifference = Math.Abs(maxX - minX);
            var yDifference = Math.Abs(maxY - minY);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.ScaleTransform(700f / xDifference, 700f / yDifference);
                g.TranslateTransform(-minX, -minY);
                foreach (var rectangle in enumerable)
                    g.DrawRectangle(Pens.Red, rectangle);
            }
            
            bitmap.Save(path);
        }
    }
}