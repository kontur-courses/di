using System;
using TagCloud.Layout;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new OneWordInLineParser("input.txt");
            var freqAnalyzer = new FrequencyAnalyzer(parser);
            var canvas = new Canvas(1000, 800);
            var layouter = new Layouter(new Spiral(canvas));
            
            var vizualizer = new Visualizer(freqAnalyzer, layouter, canvas);
            vizualizer.Visualize();
        }
    }
}