using System.Drawing;
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
        private readonly WordsProvider wordsProvider;
        private readonly WordPreprocessor wordPreprocessor;
        private readonly CloudPainter cloudPainter;

        public CloudCreator(ITextReader textReader, WordsProvider wordsProvider, WordPreprocessor wordPreprocessor,
            CloudPainter cloudPainter)
        {
            this.textReader = textReader;
            this.wordsProvider = wordsProvider;
            this.wordPreprocessor = wordPreprocessor;
            this.cloudPainter = cloudPainter;
        }

        public Bitmap GetCloud(VisualisingOptions visualisingOptions, string textName)
        {
            var cloudVisualizer = new TagCloudVisualizer(visualisingOptions);
            var center = new Point(visualisingOptions.ImageSize.Width / 2, visualisingOptions.ImageSize.Height / 2);
            var layouter = new CircularCloudLayouter(new Spiral(center));
            var text = textReader.ReadText(PathFinder.GetTextsPath(textName));
            var words = wordsProvider.GetWords(text);
            var preprocessedWords = wordPreprocessor.GetPreprocessedWords(words);
            var image = cloudVisualizer.GetVisualization(preprocessedWords, layouter, cloudPainter);
            return image;
        }
    }
}