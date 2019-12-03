using System;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using TagsCloudTextPreparation;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Styling.WordSizeCalculators;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var wordsToExclude = File.ReadAllLines(Path.Combine( projectDirectory,"Example", "wordsToExclude.txt"));
            var wordsInput = File.ReadAllLines(Path.Combine( projectDirectory,"Example", "wordsInput.txt"));
            var textPreparer = new TextPreparer(new TextPreparerConfig().Excluding(wordsToExclude));
            var words = textPreparer.GetWordsByFrequency(wordsInput);
            var fontProperties = new FontProperties("Arial Black", 32);
            var style = new Style(new GrayTheme(), fontProperties, new WordSizeCalculatorLogarithmic());
            var visualizer = new TextNoRectanglesVisualizer();
            var layouter = new SpiralCloudLayouter(new Spiral(new Point(350, 300), 0.1f, 0.1f));
            var cloud = new Cloud(words, style, visualizer, layouter);

            cloud.Visualize(900, 600).Save(Path.Combine(projectDirectory,"Example","result.png"));
        }
    }
}