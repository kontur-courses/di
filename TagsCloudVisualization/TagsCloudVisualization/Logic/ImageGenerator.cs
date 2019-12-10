using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class ImageGenerator : IImageGenerator
    {
        public Bitmap CreateImage(IEnumerable<Tag> tags, float cloudScale, ImageSettings imageSettings)
        {
            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var imageCenter = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
            var graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(
                new SolidBrush(imageSettings.BackgroundColor), 
                new Rectangle(Point.Empty, imageSettings.ImageSize)
                );
            foreach (var tag in tags)
                DrawTag(graphics, tag, cloudScale, imageSettings, imageCenter);
            return bmp;
        }

        private void DrawTag(Graphics graphics, Tag tag, float cloudScale, ImageSettings imageSettings, Point imageCenter)
        {
            var scaledFontSize = tag.FontSize * cloudScale;
            var scaledFont = new Font(imageSettings.Font.FontFamily, scaledFontSize, imageSettings.Font.Style);
            var positionMinusCenter = new Point(
                tag.TagBox.Location.X - imageCenter.X,
                tag.TagBox.Location.Y - imageCenter.Y
            );
            var newPointLocation = new Point(
                imageCenter.X + (int)(positionMinusCenter.X * cloudScale),
                imageCenter.Y + (int)(positionMinusCenter.Y * cloudScale)
            );
            graphics.DrawString(tag.WordToken.Word, scaledFont, new SolidBrush(tag.Color), newPointLocation);
        }
    }
}