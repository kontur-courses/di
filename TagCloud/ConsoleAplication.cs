using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class ConsoleAplication : IAplication
    {
        private readonly ITextParcer parser;
        private readonly CircularCloudLayouter cloud;
        private readonly Visualiser visualiser;

        public ConsoleAplication(ITextParcer textParser, CircularCloudLayouter cloud,
            Visualiser visualiser)
        {
            this.visualiser = visualiser;
            this.cloud = cloud;
            this.parser = textParser;
        }

        public void Run()
        {
            var words = parser.TryGetWordsFromText();
            if (words == null)
                return;
            var parsedWords = parser.ParseWords(words);
            cloud.AddWordsFromDictionary(parsedWords);
            visualiser.RenderCurrentConfig(cloud);
        }
    }
}