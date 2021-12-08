using CloudTagContainer;
using CloudTagContainer.ImageSavers;

namespace CUI
{
    public class ConsoleInterface
    {
        private readonly Visualizer visualizer;
        private readonly IImageSaver imageSaver;
        private readonly IFileStreamFactory fileStreamFactory;
        private readonly WordsReader wordsReader;
        private readonly IWordsPreprocessor preprocessor;

        public ConsoleInterface(
            Visualizer visualizer, 
            IImageSaver imageSaver, 
            IFileStreamFactory fileStreamFactory,
            WordsReader wordsReader,
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