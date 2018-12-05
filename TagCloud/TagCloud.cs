using System;

using TagCloud.Visualizers;
using TagCloud.WordsProcessors;
using TagCloud.WordsReaders;

namespace TagCloud
{
    public class TagCloud
    {
        private readonly IWordsReader wordsReader;
        private readonly AbstractWordsProcessor wordsProcessor;
        private readonly AbstractTagCloudVisualizer visualizer;

        public TagCloud(IWordsReader wordsReader, AbstractWordsProcessor wordsProcessor, AbstractTagCloudVisualizer visualizer)
        {
            this.wordsReader = wordsReader;
            this.wordsProcessor = wordsProcessor;
            this.visualizer = visualizer;
        }

        public void MakeTagCloud(string pathToText, string pathForImage)
        {
            var words = wordsReader.ReadFrom(pathToText);
            var tagStats = wordsProcessor.Process(words);
            visualizer.Render(tagStats, pathForImage);
        }
    }
}
