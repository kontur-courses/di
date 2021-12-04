using System.Collections.Generic;
using System.Drawing;
using TagCloud.UI;
using TagCloud.Readers;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.Visualizers;
using TagCloud.Writers;


namespace TagCloud
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var fileReader = new TextReader();
            var textAnalyzer = new TextAnalyzer(new HashSet<string> {"me", "you"});
            var frequencyAnalyzer = new FrequencyAnalyzer();
            var drawingSettings = new DrawingSettings(Color.Black, Color.White, 1200, 1200,
                new Font(FontFamily.GenericSansSerif, 8));
            var tagCreator = new TagCreator(drawingSettings);
            var layouter = new CircularCloudLayouter(new Point(600, 600));

            var visualizer = new CloudVisualizer(drawingSettings);
            var writer = new BitmapWriter();
            IUserInterface client = new ConsoleUI(fileReader, 
                textAnalyzer, 
                frequencyAnalyzer, 
                layouter, 
                visualizer, 
                writer, 
                tagCreator);
            client.Run("test3.txt");
        }
    }
}
