using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Visualizers;
using TagsCloudContainer.Readers;
using System.Drawing;

namespace TagsCloudContainer.TagsCloudGenerators
{
    class TagsCloudGenerator
    {
        private IWordCounter wordCounter;
        private IVisualizer visualizer;
        private IReader reader;

        public TagsCloudGenerator(
            IWordCounter wordCounter,
            IVisualizer visualizer,
            IReader reader)
        {
            this.wordCounter = wordCounter;
            this.visualizer = visualizer;
            this.reader = reader;
        }

        public Bitmap CreateTagCloud()
        {
            var words = reader.ReadAllLines();
            var wordTokens = wordCounter.CountWords(words);
            var bitmap = visualizer.VisualizeCloud(wordTokens);
            return bitmap;
        }
    }
}
