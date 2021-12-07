using System;
using CloudTagContainer.ImageSavers;

namespace CloudTagContainer.CUI
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
        
        public void Run()
        {
            var pathToRead = "input.txt";
            var pathToSave = "result.png";
            var inStream = fileStreamFactory.Open(pathToRead);
            var words = wordsReader.Read(inStream);
            var preprocessesWords = preprocessor.Preprocess(words);
            
            var image = visualizer.Visualize(preprocessesWords);
            
            var oStream = fileStreamFactory.Open(pathToSave);
            imageSaver.Save(image, oStream);
        }
    }
}