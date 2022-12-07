using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var path = @"..\..\Text.txt";
            var text = TextReader.Read(path);
            var settings = new ProcessingSettings();
            var words = WordProcessor.Process(text, settings);
            var wordFrequency = FrequencyCounter.GetFrequency(words);
            var center = new Point(400, 400);
            var layouter = new CircularCloudLayouter(new Spiral(center));

            var visualizer = new CloudVisualizer(center, layouter, @"..\..\", "TestCloud", wordFrequency);
            visualizer.CreateImage();
        }
    }
}
