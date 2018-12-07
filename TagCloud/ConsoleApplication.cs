using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class ConsoleApplication : IApplication
    {
        private readonly ITextParcer parser;
        private readonly ICloudLayouter cloud;
        private readonly Visualizer visualizer;

        public ConsoleApplication(ITextParcer textParser, ICloudLayouter cloud,
            Visualizer visualizer)
        {
            this.visualizer = visualizer;
            this.cloud = cloud;
            this.parser = textParser;
        }

        public void Run(string input, string output)
        {
            var words = parser.TryGetWordsFromText(input);
            if (words == null)
                return;
            var parsedWords = parser.ParseWords(words);
            cloud.AddWordsFromDictionary(parsedWords);
            visualizer.RenderCurrentConfig(cloud, output);
        }
    }
}