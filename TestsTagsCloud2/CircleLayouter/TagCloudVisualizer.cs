using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizer
    {
        public void MakePicture(List<Rectangle> rectangles, string fileName)
        {
            var minX = Int32.MaxValue;
            var maxX = Int32.MinValue;
            var minY = Int32.MaxValue;
            var maxY = Int32.MinValue;
            foreach (var rectangle in rectangles)
            {
                minX = Math.Min(rectangle.Left, minX);
                maxX = Math.Max(rectangle.Right, maxX);
                minY = Math.Min(rectangle.Bottom, minY);
                maxY = Math.Max(rectangle.Top, maxY);
            }
            var width = maxX - minX;
            var height = maxY - minY;
            var additive = 100;
            var squareLength = Math.Max(width, height)+additive;
            if (squareLength % 2 == 1)
                squareLength += 1;
            var bitmap = new Bitmap(squareLength, squareLength);
            DrawBackground(bitmap, squareLength);
            var blackColor = Color.FromArgb(0, 0, 0);
            DrawRectangles(rectangles, bitmap, blackColor, squareLength/2);
            bitmap.Save(fileName);
        }

        private static void DrawBackground(Bitmap bitmap, int squareLength)
        {
            for (var x = 0; x < squareLength; x++)
            {
                for (var y = 0; y < squareLength; y++)
                {
                    var whiteColor = Color.FromArgb(255, 255, 255);
                    bitmap.SetPixel(x, y, whiteColor);
                }
            }
        }

        private static void DrawRectangles(List<Rectangle> rectangleList, Bitmap bitmap, 
            Color blackColor, int halfSideLength)
        {
            foreach (var rectangle in rectangleList)
            {
                for (var x = rectangle.Left; x <= rectangle.Right; x++)
                {
                    bitmap.SetPixel(x + halfSideLength, rectangle.Top + halfSideLength, blackColor);
                    bitmap.SetPixel(x + halfSideLength, rectangle.Bottom + halfSideLength, blackColor);
                }

                for (var y = rectangle.Top; y <= rectangle.Bottom; y++)
                {
                    bitmap.SetPixel(rectangle.Left + halfSideLength, y + halfSideLength, blackColor);
                    bitmap.SetPixel(rectangle.Right + halfSideLength, y + halfSideLength, blackColor);
                }
            }
        }
    }
}