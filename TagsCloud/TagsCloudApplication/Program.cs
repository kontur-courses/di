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
            var textInput = File.ReadAllText(Path.Combine( projectDirectory,"Example", "wordsInput.txt"));
            var textPreparer = new TextPreparer(new TextPreparerConfig().Excluding(wordsToExclude));
            var words = textPreparer.GetWordsByFrequency(textInput);
            var fontProperties = new FontProperties("Arial", 36);
            var style = new Style(new GrayTheme(), fontProperties, new WordSizeCalculatorLogarithmic());
            var visualizer = new TextNoRectanglesVisualizer();
            var layouter = new SpiralCloudLayouter(new Spiral(new Point(600, 700), 0.1f, 0.1f));
            var cloud = new Cloud(words, style, visualizer, layouter);

            cloud.Visualize(1500, 1500).Save(Path.Combine(projectDirectory,"Example","result.png"));
        }
    }
}