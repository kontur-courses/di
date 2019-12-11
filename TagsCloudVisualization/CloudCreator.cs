using System;
using System.Drawing;
using System.Text;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.PathFinders;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization
{
    public class CloudCreator
    {
        private readonly ITextReader textReader;
        private readonly WordsExtractor wordsExtractor;
        private readonly WordPreprocessor wordPreprocessor;
        private readonly ICloudPainter<Tuple<string, Rectangle>> cloudPainter;
        private readonly TagCloudVisualizer tagCloudVisualizer;
        private readonly ILayouter layouter;

        public CloudCreator(ITextReader textReader, WordsExtractor wordsExtractor, WordPreprocessor wordPreprocessor,
            ICloudPainter<Tuple<string, Rectangle>> cloudPainter, TagCloudVisualizer tagCloudVisualizer,
            ILayouter layouter)
        {
            this.textReader = textReader;
            this.wordsExtractor = wordsExtractor;
            this.wordPreprocessor = wordPreprocessor;
            this.cloudPainter = cloudPainter;
            this.tagCloudVisualizer = tagCloudVisualizer;
            this.layouter = layouter;
        }

        public Bitmap GetCloud(string textName)
        {
            var text = textReader.ReadText(PathFinder.GetTextsPath(textName), Encoding.UTF8);
            var words = wordsExtractor.GetWords(text);
            var preprocessedWords = wordPreprocessor.GetPreprocessedWords(words);
            var image = tagCloudVisualizer.GetVisualization(preprocessedWords, layouter, cloudPainter);
            return image;
        }
    }
}