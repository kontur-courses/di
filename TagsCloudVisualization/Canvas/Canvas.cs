using System;
using System.Drawing;

namespace TagsCloudVisualization.Canvas
{
    public abstract class Canvas
    {
        public readonly int Width;
        public readonly int Height;

        protected readonly Random Random;
        protected readonly Bitmap Bitmap;
        protected readonly Graphics Graphics;
        
        protected Canvas(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException();

            Width = width;
            Height = height;
            
            Random = new Random();
            Bitmap = new Bitmap(height, width);
            Graphics = Graphics.FromImage(Bitmap);
        }

        public abstract void Draw(Rectangle rectangle, Brush brush = null);
        public abstract void Draw(string word, Font font, RectangleF rectangleF, Brush brush = null);
        public abstract void Save(string directoryPath, string fileName);
    }
}