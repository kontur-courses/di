using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.BackgroundPainter;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private readonly ICanvas canvas;
        private readonly IPathCreater creater;
        private readonly ITagsCreater imageInfo;
        private readonly IBackgroundPainter backgroundPainter;
        public Visualizer(ICanvas canvas, IPathCreater pathCreator, ITagsCreater tagsCreater, IBackgroundPainter backgroundPainter)
        {
            this.canvas = canvas;
            creater = pathCreator;
            this.imageInfo = tagsCreater;
            this.backgroundPainter = backgroundPainter;
        }

        public void Visualize(string filename, FontFamily fontFamily, Color stringColor)
        {
            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            var graphics = Graphics.FromImage(bitmap);
            var tags = imageInfo.GetTags(filename, canvas.Height);
            
            backgroundPainter.Draw(tags, canvas, graphics);
            DrawAllStrings(tags, fontFamily, stringColor, graphics);
            
            bitmap.Save(creater.GetNewPngPath());
        }
        
        private void DrawAllStrings(List<Tuple<string, Rectangle>> tags, FontFamily fontFamily, Color color, Graphics graphics)
        {
            var textBrush = new SolidBrush(color);
            foreach (var (str, rectangle) in tags)
            {
                DrawString(str, rectangle, fontFamily, textBrush, graphics);
            }
        }

        private void DrawString(string str, Rectangle rectangle, FontFamily fontFamily, Brush textBrush, Graphics graphics)
        {
            var x = rectangle.X - (rectangle.Height / 4);
            var y = rectangle.Y - (rectangle.Height / 2);
            if (rectangle.Height < 2)
                return;
            graphics.DrawString(str, new Font(fontFamily, rectangle.Height), textBrush, x, y);
        }
    }
}