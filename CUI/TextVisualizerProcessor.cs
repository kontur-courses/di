using Visualization;
using Visualization.ImageSavers;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class TextVisualizerProcessor
    {
        private readonly Visualizer visualizer;
        private readonly IImageSaver imageSaver;
        private readonly IFileStreamFactory fileStreamFactory;
        private readonly IWordsReader wordsReader;
        private readonly IWordsPreprocessor preprocessor;

        public TextVisualizerProcessor(
            Visualizer visualizer,
            IImageSaver imageSaver,
            IFileStreamFactory fileStreamFactory,
            IWordsReader wordsReader,
            IWordsPreprocessor preprocessor)
        {
            this.visualizer = visualizer;
            this.imageSaver = imageSaver;
            this.fileStreamFactory = fileStreamFactory;
            this.wordsReader = wordsReader;
            this.preprocessor = preprocessor;
        }

        public void Run(Options options)
        {
            var inStream = fileStreamFactory.OpenOnReading(options.InputTextPath);
            var words = wordsReader.Read(inStream);
            var preprocessesWords = preprocessor.Preprocess(words);

            var image = visualizer.Visualize(preprocessesWords);

            var oStream = fileStreamFactory.OpenOnWriting(options.PathToSaveImage);
            imageSaver.Save(image, oStream);
        }
    }
}