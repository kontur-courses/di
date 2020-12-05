using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class TagsVisualizer : RectangleVisualizer
    {
        public Bitmap GetBitmap(List<Tag> tags, Color backgroundColor, string fontFamily, Brush textColor)
        {
            var imageSize = GetImageSize(tags.Select(x => x.Rectangle));
            var pen = new Pen(Color.MediumVioletRed, 4);
            var bitmap = new Bitmap(imageSize.Width + (int) pen.Width, imageSize.Height + (int) pen.Width);
            using var graphics = Graphics.FromImage(bitmap);
            {
                graphics.Clear(backgroundColor);

                foreach (var tag in tags)
                {
                    graphics.DrawString(tag.Text, new Font(fontFamily, tag.Font.Size), textColor, tag.Rectangle.X, tag.Rectangle.Y);
                }
            }

            return bitmap;
        }
    }
}