using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.PathFinders;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordConverters;
using TagsCloudVisualization.PathFinders;

namespace TagsCloudVisualization
{
    public class CloudCreator
    {
        private readonly ITextReader textReader;
        private readonly WordsProvider wordsProvider;
        private readonly WordPreprocessor wordPreprocessor;
        private readonly TagCloudVisualizer cloudVisualizer;
        private readonly CloudPainter cloudPainter;
        private readonly ILayouter cloudLayouter;
            
        public CloudCreator(ITextReader textReader, WordsProvider wordsProvider, WordPreprocessor wordPreprocessor,
            TagCloudVisualizer cloudVisualizer, CloudPainter cloudPainter, ILayouter cloudLayouter)
        {
            this.textReader = textReader;
            this.wordsProvider = wordsProvider;
            this.wordPreprocessor = wordPreprocessor;
            this.cloudVisualizer = cloudVisualizer;
            this.cloudPainter = cloudPainter;
            this.cloudLayouter = cloudLayouter;
        }
        
        public Bitmap GetCloud(Color backgroundColor, Color textColor, Font font, Size imageSize, string textName)
        {
            var text = textReader.ReadText(PathFinder.GetTextsPath(textName));
            var words = wordsProvider.GetWords(text);
            var preprocessedWords = wordPreprocessor.GetPreprocessedWords(words);
            var image = cloudVisualizer.GetVisualization(preprocessedWords, cloudLayouter, cloudPainter);
            return image;
        }
    }
}