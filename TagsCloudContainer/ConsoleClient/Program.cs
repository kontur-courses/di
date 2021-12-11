using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Factories;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.TextHandlers;
using TagsCloudVisualization.TextPreparers;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.Visualization.Configurator;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var spiral = new Spiral(new Point(400, 300));
            var layouter = new CircularCloudLayouter(spiral);
            var factory = new WordsVisualizingTokenFactory();
            var configurator = new WordsVisualizingConfigurator(factory, _ => Color.Aqua, 11);
            var config = new ScreenConfig
            {
                Size = new Size(800, 600),
                BackgroundColor = Color.Black
            };
            var visualizer = new WordsVisualizer(layouter, configurator, config);
            var filters = new List<Func<string, bool>> {s => s.Length < 3};
            var preparers = new List<Func<string, string>>() {s => s.ToLower()};
            var preparer = new TextPreparer(filters, preparers);
            var parser = new TxtParser();
            var handler = new TextHandler(preparer, parser);
            var creator = new TagCloudCreator(visualizer, handler);

            var img = creator.CreateFromFile
                (@"C:\Users\jexin\Desktop\di\TagsCloudContainer\ConsoleClient\words.txt");
            
            img.Save("working.png");
        }
    }
}