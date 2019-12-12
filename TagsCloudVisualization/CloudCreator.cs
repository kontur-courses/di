using System;
using System.Drawing;
using System.Text;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordSizing;

namespace TagsCloudVisualization
{
    public class CloudCreator
    {
        private readonly ITextReader textReader;
        private readonly WordsExtractor wordsExtractor;
        private readonly WordPreprocessor wordPreprocessor;
        private readonly ICloudPainter<Tuple<SizedWord, Rectangle>> cloudPainter;
        private readonly TagCloudSizedVisualizer tagCloudVisualizer;
        private readonly ILayouter layouter;

        public CloudCreator(ITextReader textReader, WordsExtractor wordsExtractor, WordPreprocessor wordPreprocessor,
            ICloudPainter<Tuple<SizedWord, Rectangle>> cloudPainter, TagCloudSizedVisualizer tagCloudVisualizer,
            ILayouter layouter)
        {
            this.textReader = textReader;
            this.wordsExtractor = wordsExtractor;
            this.wordPreprocessor = wordPreprocessor;
            this.cloudPainter = cloudPainter;
            this.tagCloudVisualizer = tagCloudVisualizer;
            this.layouter = layouter;
        }

        public Bitmap GetCloud(string textPath)
        {
            var text = textReader.ReadText(textPath, Encoding.UTF8);
            var words = wordsExtractor.GetWords(text);
            var preprocessedWords = wordPreprocessor.GetPreprocessedWords(words);
            var image = tagCloudVisualizer.GetVisualization(preprocessedWords, layouter, cloudPainter);
            return image;
        }
    }
}