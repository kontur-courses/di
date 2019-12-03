using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Core.Drawers
{
    public class RectangleCloudDrawer : IRectangleCloudDrawer
    {
        private static readonly Font FakeFont = new Font(FontFamily.GenericSerif, 1);
        private readonly Color backgroundColor;
        private readonly Brush tagBrush;
        private readonly Pen pen;
        private readonly StringFormat stringFormat;

        public RectangleCloudDrawer(Color backgroundColor, Brush tagBrush, Brush rectBrush)
        {
            this.backgroundColor = backgroundColor;
            this.tagBrush = tagBrush;
            pen = new Pen(rectBrush);
            stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
            };
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles, string filename)
        {
            DrawCloud(
                rectangles.Select(rect => new TagInfo("", FakeFont, rect)),
                filename, true);
        }

        public void DrawCloud(IEnumerable<TagInfo> tags, string filename, bool withRectangles=false)
        {
            var imageSize = GetSuitableImageSize(tags);
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backgroundColor);
            foreach (var tag in tags)
            {
                var movedToCenterRect = tag.Rectangle.ShiftLocation(center);
                graphics.DrawString(tag.Value, tag.Font, tagBrush, movedToCenterRect, stringFormat);
                if (withRectangles)
                {
                    graphics.DrawRectangle(pen, movedToCenterRect);
                }
            }

            bitmap.Save(filename);
        }

        private Size GetSuitableImageSize(IEnumerable<TagInfo> tags)
        {
            var origin = new Point(0, 0);
            var maxDistToOrigin = tags
                .SelectMany(tag => tag.Rectangle.GetCornersClockwiseFromTopLeft())
                .Select(corner => corner.DistanceTo(origin))
                .Max();
            var imageSide = (int) maxDistToOrigin * 2;
            return new Size(imageSide, imageSide);
        }
    }
}