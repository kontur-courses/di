using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Visualizing
{
    public class TagsCloudVisualizer
    {
        private readonly ImageSettings settings;
        private readonly ITagPainter tagPainter;

        public TagsCloudVisualizer(ITagPainter tagPainter, ImageSettings settings)
        {
            this.settings = settings;
            this.tagPainter = tagPainter;
        }

        public Bitmap GetPictureOfRectangles(IEnumerable<Tag> tags)
        {
            var tagsList = tags.ToList();
            var pictureRectangle = CalculatePictureRectangle(tagsList.Select(tag => tag.Location).ToList());
            var picture = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using (var graphics = Graphics.FromImage(picture))
            {
                var scaleCoefficient = CalculateScaleCoefficient(pictureRectangle);
                graphics.ScaleTransform(scaleCoefficient, scaleCoefficient);
                graphics.TranslateTransform(-pictureRectangle.X, -pictureRectangle.Y);
                graphics.Clear(settings.BackgroundColor);
                foreach (var tag in tagsList)
                    DrawTag(graphics, tag);
            }

            return picture;
        }

        private void DrawTag(Graphics graphics, Tag tag)
        {
            graphics.DrawString(tag.Word, tag.Font, tagPainter.ChooseBrushForTag(tag), tag.Location);
        }

        private Rectangle CalculatePictureRectangle(IReadOnlyList<Rectangle> rectangles)
        {
            var minTop = rectangles.Min(r => r.Top);
            var maxBottom = rectangles.Max(r => r.Bottom);
            var minLeft = rectangles.Min(r => r.Left);
            var maxRight = rectangles.Max(r => r.Right);

            return new Rectangle(
                new Point(minLeft, minTop), 
                new Size(maxRight - minLeft, maxBottom - minTop));
        }

        private float CalculateScaleCoefficient(Rectangle pictureRectangle)
        {
            return Math.Min((float)settings.ImageSize.Height / pictureRectangle.Height,
                (float)settings.ImageSize.Width / pictureRectangle.Width);
        }
    }
}
