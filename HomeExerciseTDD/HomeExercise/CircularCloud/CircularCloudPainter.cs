using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;


namespace HomeExerciseTDD
{
    public class CircularCloudPainter: IPainter<Rectangle>
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private Pen pen;
        private Color color;
        private readonly Random randomazer;
        private readonly List<Rectangle> rectangles;
        private readonly string fileName;
        private readonly ImageFormat format;
        private readonly int offsetX;
        private readonly int offsetY;
        
      
        public CircularCloudPainter(int width, int height, List<Rectangle> rectangles, string fileName, ImageFormat format)
        //public CircularCloudPainter(PainterSettings settings)
        {
            offsetX = width/2;
            offsetY = height/2;
            this.format = format;
            this.fileName = fileName;
            this.rectangles = rectangles;
            randomazer= new Random();
            color = new Color();
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void DrawFigures()
        {
            foreach (var rectangle in rectangles)
            {
                var center = rectangles.First();
                var newX= rectangle.X + offsetX - center.X;
                var newY = rectangle.Y + offsetY - center.Y;
                var point = new Point(newX, newY);
                var newRectangle = new Rectangle(point, rectangle.Size);

                color = GetColor();
                pen = new Pen(color);
                graphics.DrawRectangle(pen, newRectangle);
                //ПЕРЕГРУЗКИ
                //MeasureString(String, Font)
                var font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
                graphics.DrawString("sss", font1, Brushes.Blue, newRectangle);
            }
            
            bitmap.Save(fileName, format);
        }

        private Color GetColor()
        {
            var R = randomazer.Next(0, 255);
            var G = randomazer.Next(0, 255);
            var B = randomazer.Next(0, 255);
            
            return Color.FromArgb(R, G, B);
        }
    }
}