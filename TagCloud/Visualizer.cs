using System;
using System.Drawing;
using TagCloud.Coloring;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private readonly ICanvas canvas;
        private readonly IPathCreater creater;
        private readonly IImageInfo imageInfo;
        private readonly IPainter painter;
        public Visualizer(ICanvas canvas, IPathCreater pathCreator, IImageInfo imageInfo, IPainter painter)
        {
            this.canvas = canvas;
            creater = pathCreator;
            this.imageInfo = imageInfo;
            this.painter = painter;
        }

        public void Visualize(string filename, string fontFamily)
        {
            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var pair in imageInfo.GetTags(filename, canvas.Height))
            {
                var rectangle = pair.Item2;
                painter.DrawAndFillRectangle(rectangle, graphics);
                painter.DrawString(rectangle, pair.Item1, fontFamily, graphics);
            }
            
            bitmap.Save(creater.GetNewPngPath());
        }
    }
}