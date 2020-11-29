using System;
using System.Drawing;
using System.Linq;
using TagCloud.Layout;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private ICanvas Canvas;
        private IPathCreater Creater;
        private IImageInfo ImageInfo;
        public Visualizer(ICanvas canvas, IPathCreater pathCreator, IImageInfo imageInfo)
        {
            //TODO: add fontFamily and coloring algoritm
            Canvas = canvas;
            Creater = pathCreator;
            ImageInfo = imageInfo;
        }

        public void Visualize(string filename)
        {
            var bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var pair in ImageInfo.GetTags(filename, Canvas.Height))
            {
                var rectangle = pair.Item2;
                DrawAndFillRectangle(graphics, rectangle);
                graphics.DrawString(pair.Item1, new Font("Arial",rectangle.Height/2),
                    new SolidBrush(Color.Black), rectangle);
            }
            
            bitmap.Save(Creater.GetNewPngPath());
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