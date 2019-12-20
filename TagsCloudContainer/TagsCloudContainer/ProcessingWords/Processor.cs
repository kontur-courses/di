using CloudDrawing;
using TagsCloudContainer.PreprocessingWords;
using TagsCloudContainer.Reader;

namespace TagsCloudContainer.ProcessingWords
{
    public class Processor : IProcessor
    {
        private readonly IReaderLinesFromFile readerLinesFromFile;
        private readonly IPreprocessingWords preprocessingWords;
        private readonly ICircularCloudDrawing circularCloudDrawing;

        public Processor(
            IReaderLinesFromFile readerLinesFromFile,
            IPreprocessingWords preprocessingWords,
            ICircularCloudDrawing circularCloudDrawing)
        {
            this.readerLinesFromFile = readerLinesFromFile;
            this.preprocessingWords = preprocessingWords;
            this.circularCloudDrawing = circularCloudDrawing;
        }

        public void Run(string pathToFile, string pathToSaveFile, ImageSettings imageSettings,
            WordDrawSettings wordDrawSettings)
        {
            circularCloudDrawing.SetOptions(imageSettings);
            var processedWord = preprocessingWords.Preprocessing(readerLinesFromFile.GetWordsSet(pathToFile));
            circularCloudDrawing.DrawWords(CountingWords.GetWordAndNumberOfRepetitions(processedWord),
                wordDrawSettings);
            circularCloudDrawing.SaveImage(pathToSaveFile);
        }
    }
}