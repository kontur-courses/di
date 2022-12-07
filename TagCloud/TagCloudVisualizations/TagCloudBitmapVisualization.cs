using System;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        private readonly TagCloud tagCloud;

        public TagCloudBitmapVisualization(TagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
        }

        public void Visualize()
        {
            throw new NotImplementedException();
        }

        public void Save(string file)
        {
            var rectangleOutline = 1;

            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline, 
                tagCloud.GetHeight() + rectangleOutline);

            var frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());

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

        private Rectangle MoveRectangleToImageFrame(Rectangle rectangle, Size imageCenter) =>
            new Rectangle(rectangle.Location.ShiftTo(imageCenter), rectangle.Size);
        

        private Brush GetRandomBrush() =>
            new SolidBrush(GetRandomColor());
        

        private Color GetRandomColor()
        {
            var knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = knownColors[random.Next(knownColors.Length)];
            return Color.FromKnownColor(randomColorName);
        }
    }
}