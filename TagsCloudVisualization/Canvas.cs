using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Canvas
    {
        private readonly Random random;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public Canvas(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException();

            random = new Random();
            bitmap = new Bitmap(height, width);
            graphics = Graphics.FromImage(bitmap);
        }

        public void Draw(Rectangle rectangle, Brush brush = null)
        {
            graphics.FillRectangle(brush ?? RandomBrush(), rectangle);
            graphics.DrawRectangle(new Pen(Color.White), rectangle);
        }

        public void Draw(string word, Font font, RectangleF rectangleF, Brush brush = null)
        {
            graphics.DrawString(word, font, brush ?? RandomBrush(), rectangleF);
        }

        private Brush RandomBrush()
        {
            var brushes = new List<Brush>()
            {
                new SolidBrush(Color.Green),
                new SolidBrush(Color.Blue),
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Magenta),
                new SolidBrush(Color.Black),
                new SolidBrush(Color.Aqua),
                new SolidBrush(Color.Indigo),
            };

            return brushes[random.Next(0, brushes.Count)];
        }

        public void Save(string name)
        {
            bitmap.Save(name + ".png");
        }
    }
}
