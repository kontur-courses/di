using System;
using System.Drawing;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudContainer
{
    public class CircularCloudDrawing
    {
        private ICloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics;

        public CircularCloudDrawing(Size imageSize, Color background, Func<Point, ICloudLayouter> cloudLayouterGenerator)
        {
            if (imageSize.Height <= 0 || imageSize.Height <= 0)
                throw new AggregateException("Size have zero width or height");
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(background);
            layouter = cloudLayouterGenerator(new Point(imageSize.Width / 2, imageSize.Height / 2));
        }

        public void DrawString(string str, Font font, Brush brush, StringFormat stringFormat)
        {
            var stringSize = (graphics.MeasureString(str, font) + new SizeF(1, 1)).ToSize();
            var stringRectangle = layouter.PutNextRectangle(stringSize);
            graphics.DrawString(str, font, brush, stringRectangle, stringFormat);
        }
        
        public void DrawRectangle(Rectangle rectangle, Pen pen)
        {
            graphics.DrawRectangle(pen, rectangle);
        }
        
        public void SaveImage(string filename)
        {
            bitmap.Save(filename);
        }
    }
}