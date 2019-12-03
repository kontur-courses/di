﻿using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudDrawing
    {
        private CircularCloudLayouter layouter;
        private Bitmap bitmap;
        private Graphics graphics;
        private StringFormat stringFormat;
        private Pen pen;
        private Brush brush;
        
        public CircularCloudDrawing(Size imageSize)
        {
            if (imageSize.Height <= 0 || imageSize.Height <= 0)
                throw new AggregateException("Size have zero width or height");
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Linen);
            brush = Brushes.Black;
            layouter = new CircularCloudLayouter(new Point(imageSize.Width / 2, imageSize.Height / 2));
            pen = new Pen(Brushes.Brown);
            stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center
            };
        }

        public void DrawString(string str, Font font)
        {
            var stringSize = (graphics.MeasureString(str, font) + new SizeF(1, 1)).ToSize();
            var stringRectangle = layouter.PutNextRectangle(stringSize);
            graphics.DrawString(str, font, brush, stringRectangle, stringFormat);
        }
        
        public void DrawRectangle(Rectangle rectangle)
        {
            graphics.DrawRectangle(pen, rectangle);
        }
        
        public void SaveImage(string filename)
        {
            bitmap.Save(filename);
        }
    }
}