using Visualization;
using Visualization.ImageSavers;
using Visualization.Preprocessors;
using Visualization.Readers;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class TextVisualizerProcessor
    {
        private readonly Visualizer visualizer;
        private readonly IImageSaver imageSaver;
        private readonly IFileStreamFactory fileStreamFactory;
        private readonly IFileReader fileReader;
        private readonly IWordsParser wordsParser;
        private readonly IWordsPreprocessor preprocessor;
        public TextVisualizerProcessor(
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

        public void Run(Options options)
        {
            var inStream = fileStreamFactory.OpenOnReading(options.InputTextPath);
            var content = fileReader.ReadToEnd(inStream);
            var words = wordsParser.Read(content);
            var preprocessesWords = preprocessor.Preprocess(words);

            var image = visualizer.Visualize(preprocessesWords);

            var oStream = fileStreamFactory.OpenOnWriting(options.PathToSaveImage);
            imageSaver.Save(image, oStream);
        }
    }
}