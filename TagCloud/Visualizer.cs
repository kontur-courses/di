using System;
using System.Drawing;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private readonly ICanvas canvas;
        private readonly IPathCreater creater;
        private readonly IImageInfo imageInfo;
        private readonly Brush textBrush = new SolidBrush(Color.Black);
        public Visualizer(ICanvas canvas, IPathCreater pathCreator, IImageInfo imageInfo)
        {
            //TODO: add coloring algoritm
            this.canvas = canvas;
            creater = pathCreator;
            this.imageInfo = imageInfo;
        }

        public void Visualize(string filename, string fontFamily)
        {
            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var pair in imageInfo.GetTags(filename, canvas.Height))
            {
                var rectangle = pair.Item2;
                DrawAndFillRectangle(graphics, rectangle);
                graphics.DrawString(pair.Item1, new Font(fontFamily,rectangle.Height/2), textBrush, rectangle);
            }
            
            bitmap.Save(creater.GetNewPngPath());
        }

        private static void DrawAndFillRectangle(Graphics graphics, Rectangle rectangle)
        {
            var brushColor = Color.FromArgb(Math.Abs(rectangle.X) % 255,
                Math.Abs(rectangle.Y) % 255, 100);
            var brush = new SolidBrush(brushColor);
            graphics.FillRectangle(brush, rectangle);
        }
    }
}