using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Layout;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private IFrequencyAnalyzer FrequencyAnalyzer;
        private ILayouter Layouter;
        private ICanvas Canvas;
        private IPathCreater Creater;
        public Visualizer(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter, ICanvas canvas, IPathCreater pathCreator)
        {
            //TODO: add fontFamily and coloring algoritm
            FrequencyAnalyzer = frequencyAnalyzer;
            Layouter = layouter;
            Canvas = canvas;
            Creater = pathCreator;
        }

        public void Visualize(string filename)
        {
            var frequencies = FrequencyAnalyzer.GetFrequencyDictionary(filename);
            var bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            var graphics = Graphics.FromImage(bitmap);
            
            var orderedPairs = frequencies.OrderByDescending(pair => pair.Value);
            foreach (var pair in orderedPairs)
            {
                var height = (int)Math.Round(Canvas.Height * pair.Value);
                var width = (int)Math.Round((double)height * pair.Key.Length / 2);
                var rectangle = Layouter.PutNextRectangle(new Size(width, height));
                DrawAndFillRectangle(graphics, rectangle);
                graphics.DrawString(pair.Key, new Font("Arial", height/2), new SolidBrush(Color.Black), rectangle);
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