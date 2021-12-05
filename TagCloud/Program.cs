using System.Collections.Generic;
using System.Drawing;
using TagCloud.UI;
using TagCloud.Readers;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.UI.Console;
using TagCloud.Visualizers;
using TagCloud.Writers;


namespace TagCloud
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var fileReader = new TextReader();
            var textAnalyzer = new TextAnalyzer();
            var frequencyAnalyzer = new FrequencyAnalyzer();
            var tagCreatorFactory = new TagCreatorFactory();
            var layouterFactory = new CircularCloudLayouterFactory();

            var visualizer = new CloudVisualizer();
            var writer = new BitmapWriter();
            IUserInterface client = new ConsoleUI(fileReader, 
                textAnalyzer, 
                frequencyAnalyzer, 
                layouterFactory, 
                visualizer, 
                writer,
                tagCreatorFactory);
            client.Run(args);
        }
    }
}
