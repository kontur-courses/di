using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private Dictionary<string, double> Frequencies;
        private ILayouter Layouter;
        public Visualizer(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter)
        {
            Frequencies = frequencyAnalyzer.GetFrequencyDictionary();
            Layouter = layouter;
        }

        public void Visualize()
        {
            var orderedPairs = Frequencies.OrderBy(pair => pair.Value);
            //TODO: собрать картинку записать в файл
        }

        private static string GetNewPngPath()
        {
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