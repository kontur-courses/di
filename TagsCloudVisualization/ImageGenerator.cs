#region

using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class ImageGenerator : IImageGenerator
    {
        private readonly Font font;
        private readonly Bitmap image;
        private readonly Pen pen;

        public ImageGenerator(Bitmap image, Pen pen, Font font)
        {
            this.image = image;
            this.pen = pen;
            this.font = font;
        }

        public Bitmap GenerateTagCloudBitmap(IOrderedEnumerable<ITag> tags)
        {
            var tagsList = tags.ToList();
            var brush = Graphics.FromImage(image);

            foreach (var tag in tagsList)
            {
                brush.DrawRectangle(pen, tag.Rectangle);
                brush.DrawString(tag.Word, font, new SolidBrush(pen.Color), tag.Rectangle.Location);
            }

            return image;
        }
    }
}