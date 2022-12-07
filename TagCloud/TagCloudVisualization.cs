using System;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud
{
    public class TagCloudVisualization
    {
        private static readonly Random random = new Random();

        public static void SaveAsBitmap(TagCloud tagCloud, string file)
        {
            var rectangleOutline = 1;

            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline, 
                tagCloud.GetHeight() + rectangleOutline);

            Size frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());

            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var rectangle in tagCloud.Rectangles)
                {
                    var rectangleInFrame = MoveRectangleToImageFrame(rectangle, frameShift);
                    graphics.DrawRectangle(new Pen(GetRandomBrush()), rectangleInFrame);
                }
            }
            bitmap.Save(file);
        }

        private static Rectangle MoveRectangleToImageFrame(Rectangle rectangle, Size imageCenter) =>
            new Rectangle(rectangle.Location.ShiftTo(imageCenter), rectangle.Size);
        

        private static Brush GetRandomBrush() =>
            new SolidBrush(GetRandomColor());
        

        private static Color GetRandomColor()
        {
            var knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = knownColors[random.Next(knownColors.Length)];
            return Color.FromKnownColor(randomColorName);
        }
    }
}