using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TagsCloud.CloudStructure;

namespace TagsCloud.TagsCloudVisualization
{

    public class TagsCloudVisualizer: ITagsCloudVisualizer
    {
        public readonly Size PictureSize;
        public readonly string FontName;
        public readonly Color BackgroundColor;
        public readonly Color FontColor;

        public TagsCloudVisualizer(Size pictureSize, string fontName, Color backgroundColor, Color fontColor)
        {
            PictureSize = pictureSize;
            FontName = fontName;
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
        }

        public Bitmap GetCloudVisualization(List<Tag> tags)
        {
            var pictureRectangle = GetPictureRectangle(tags);
            Brush brush = new SolidBrush(FontColor);
            var bitmap = new Bitmap(pictureRectangle.Width, pictureRectangle.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(BackgroundColor);
                graphics.TranslateTransform(-pictureRectangle.X, -pictureRectangle.Y);
                foreach (var tag in tags)
                {
                    graphics.DrawString(tag.Word, new Font(FontName, tag.FontSize), brush, tag.PosRectangle.Location);
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                }
            } 
            var resultBitmap = new Bitmap(PictureSize.Width, PictureSize.Height);
            using (var graphics = Graphics.FromImage(resultBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0, PictureSize.Width, PictureSize.Height);
            }

            return resultBitmap;
        }

        private static Rectangle GetPictureRectangle(List<Tag> tags)
        {
            var rectangles = tags.Select(t => t.PosRectangle);
            var minX = rectangles.Min(rect => rect.Left);
            var minY = rectangles.Min(rect => rect.Top);
            var maxX = rectangles.Max(rect => rect.Right);
            var maxY = rectangles.Max(rect => rect.Bottom);
            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

    }
}
