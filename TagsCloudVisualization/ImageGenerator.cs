using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class ImageGenerator : IImageGenerator
    {
        private Bitmap image;
        private Pen pen;
        private Font font;

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

        public void SetImageSize(Size imageSize)
        {
            if (imageSize.Width <= 0 || imageSize.Height <= 0)
                throw new ArgumentException(
                    $"imageSize was empty! width: {imageSize.Width}, height: {imageSize.Height}");

            image = new Bitmap(imageSize.Width, imageSize.Height);
        }

        public void SetFont(Font newFont)
        {
            font = newFont;
        }

        public void SetColors(Color color)
        {
            if (color == null) 
                throw new ArgumentException("Colors was null");

            pen = new Pen(color);
        }
    }
}