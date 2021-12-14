using System;
using System.Drawing;
using Visualization.ImageSavers;
using Visualization.Preprocessors;
using Visualization.Readers;

namespace Visualization
{
    public class VisualizerProcessor
    {
        public Bitmap VisualizedImage { get; private set; }
        
        private readonly Visualizer visualizer;
        private readonly IImageSaver imageSaver;
        private readonly IFileStreamFactory fileStreamFactory;
        private readonly IFileReader fileReader;
        private readonly IWordsParser wordsParser;
        private readonly IWordsPreprocessor preprocessor;

        public VisualizerProcessor(
            Visualizer visualizer,
            IImageSaver imageSaver,
            IFileStreamFactory fileStreamFactory,
            IWordsParser wordsParser,
            IWordsPreprocessor preprocessor,
            IFileReader fileReader)
        {
            this.visualizer = visualizer;
            this.imageSaver = imageSaver;
            this.fileStreamFactory = fileStreamFactory;
            this.wordsParser = wordsParser;
            this.preprocessor = preprocessor;
            this.fileReader = fileReader;
        }

        public void Visualize(string pathToRead)
        {
            using var inStream = fileStreamFactory.OpenOnReading(pathToRead);
            var content = fileReader.ReadToEnd(inStream);
            var words = wordsParser.Parse(content);
            var preprocessesWords = preprocessor.Preprocess(words);

            VisualizedImage = visualizer.Visualize(preprocessesWords);
        }

        public void Save(string pathToSave)
        {
            if (VisualizedImage == null)
                throw new InvalidOperationException("Image should be visualized before saving");
            
            using var oStream = fileStreamFactory.OpenOnWriting(pathToSave);
            imageSaver.Save(VisualizedImage, oStream);
        }
    }
}