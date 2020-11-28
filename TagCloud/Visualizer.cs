using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloud.Layout;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private Dictionary<string, double> Frequencies;
        private ILayouter Layouter;
        private ICanvas Canvas;
        public Visualizer(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter, ICanvas canvas)
        {
            //TODO: add fontFamily and coloring algoritm
            Frequencies = frequencyAnalyzer.GetFrequencyDictionary();
            Layouter = layouter;
            Canvas = canvas;
        }

        public void Visualize()
        {
            var bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            var graphics = Graphics.FromImage(bitmap);
            
            var orderedPairs = Frequencies.OrderByDescending(pair => pair.Value);
            foreach (var pair in orderedPairs)
            {
                var height = (int)Math.Round(Canvas.Height * pair.Value);
                var width = (int)Math.Round((double)height * pair.Key.Length / 2);
                var rectangle = Layouter.PutNextRectangle(new Size(width, height));
                DrawAndFillRectangle(graphics, rectangle);
                graphics.DrawString(pair.Key, new Font("Arial", height/2), new SolidBrush(Color.Black), rectangle);
            }
            var path = GetNewPngPath();
            bitmap.Save(path);
        }

        private static string GetNewPngPath()
        {
            //TODO: Move to new class pathFinder
            var workingDirectory = Directory.GetCurrentDirectory();
            var index = workingDirectory.IndexOf("TagCloud");
            var tagCloudPath = workingDirectory.Substring(0, index);
            return tagCloudPath + "MyPng" +  DateTime.Now.Millisecond + ".png";
        }

        private static void DrawAndFillRectangle(Graphics graphics, Rectangle rectangle)
        {
            var brushColor = Color.FromArgb(Math.Abs(rectangle.X) % 255,
                Math.Abs(rectangle.Y) % 255, 100);
            var brush = new SolidBrush(brushColor);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.FillRectangle(brush, rectangle);
        }
    }
}