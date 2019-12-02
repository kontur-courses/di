using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.TagsCloudVisualization
{
    public class DebugVisualization : ITagsCloudVisualization<Rectangle>
    {
        public Bitmap Draw(IEnumerable<Rectangle> figuresToDraw, int imageWidth, int imageHeight)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            using (var drawPlace = Graphics.FromImage(image))
            {
                var blackPen = new Pen(new SolidBrush(Color.Black), 3);
                var redPen = new Pen(new SolidBrush(Color.Red), 3);
                var rectangles = figuresToDraw.ToList();
                foreach (var rectangle in rectangles)
                {
                    var rectangleIntersectsAnother = rectangles.Any(rec => rec != rectangle
                                                                           && rectangle.IntersectsWith(rec));
                    drawPlace.DrawRectangle(rectangleIntersectsAnother ? redPen : blackPen, rectangle);
                }
            }
            return image;
        }
    }
}
