using System;
using System.Drawing;
using CloudLayouter;

namespace CloudDrawing
{
    public class CircularCloudDrawing : ICircularCloudDrawing
    {
        private ICloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics;

        public CircularCloudDrawing(ICloudLayouter cloudLayouter)
        {
            
            layouter = cloudLayouter;
           
        }

        public void SetOptions(Color background, Size imageSize)
        {
            if (imageSize.Height <= 0 || imageSize.Height <= 0)
                throw new AggregateException("Size have zero width or height");
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(background);
            layouter.SetCenter(new Point(imageSize.Width / 2, imageSize.Height / 2));
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
        
        public Bitmap GetBitmap() => bitmap;
        
        public void SaveImage(string filename)
        {
            bitmap.Save(filename);
        }
    }
}