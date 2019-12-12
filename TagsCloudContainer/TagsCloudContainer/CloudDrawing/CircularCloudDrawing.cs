using System;
using System.Drawing;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudContainer.CloudDrawing
{
    public class CircularCloudDrawing : ICircularCloudDrawing
    {
        private ICloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics;

        public CircularCloudDrawing(ICloudLayouter cloudLayouter)
        {
            var imageSize = new Size(2000, 2000);
            if (imageSize.Height <= 0 || imageSize.Height <= 0)
                throw new AggregateException("Size have zero width or height");
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            layouter = cloudLayouter;
            layouter.SetCenter(new Point(imageSize.Width / 2, imageSize.Height / 2));
        }

        public void SetBackground(Color background)
        {
            graphics.Clear(background);
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