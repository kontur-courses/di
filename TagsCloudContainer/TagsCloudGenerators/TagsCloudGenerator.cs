using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Visualizers;
using TagsCloudContainer.Readers;
using System.Drawing;
using TagsCloudContainer.WordPreprocessors;
using System;
using System.Linq;

namespace TagsCloudContainer.TagsCloudGenerators
{
    public class TagsCloudGenerator
    {
        private IWordCounter wordCounter;
        private IVisualizer visualizer;
        private IReader reader;
        private IWordPreprocessor wordPreprocessor;

        public TagsCloudGenerator(
            IWordCounter wordCounter,
            IVisualizer visualizer,
            IReader reader,
            IWordPreprocessor wordPreprocessor)
        {
            this.wordCounter = wordCounter;
            this.visualizer = visualizer;
            this.reader = reader;
            this.wordPreprocessor = wordPreprocessor;
        }

        public Bitmap CreateTagCloud()
        {
            var text = reader.ReadAllLines();
            var preprocessedWords = wordPreprocessor.WordPreprocessing(text);
            var wordTokens = wordCounter.CountWords(preprocessedWords);
            var bitmap = visualizer.VisualizeCloud(wordTokens);
            return bitmap;
        }
    }
}
