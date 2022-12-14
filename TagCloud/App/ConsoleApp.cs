using TagCloud.AppConfig;
using TagCloud.FileReader;
using TagCloud.FrequencyAnalyzer;
using TagCloud.ImageProcessing;
using TagCloud.TextParsing;
using TagCloud.WordConverter;
using TagCloud.WordFilter;

namespace TagCloud.App
{
    public class ConsoleApp : IApp
    {
        private readonly IFileReader fileReader;
        private readonly ITextParser textParser;
        private readonly IConvertersExecutor convertersExecutor;
        private readonly IFiltersExecutor filtersExecutor;
        private readonly IWordsFrequencyAnalyzer wordsFrequencyAnalyzer;
        private readonly ICloudImageGenerator cloudImageGenerator;

        public ConsoleApp(IFileReader fileReader, 
                          ITextParser textParser, 
                          IConvertersExecutor convertersExecutor, 
                          IFiltersExecutor filtersExecutor, 
                          IWordsFrequencyAnalyzer wordsFrequencyAnalyzer, 
                          ICloudImageGenerator cloudImageGenerator)
        {
            this.fileReader = fileReader;
            this.textParser = textParser;
            this.convertersExecutor = convertersExecutor;
            this.filtersExecutor = filtersExecutor;
            this.wordsFrequencyAnalyzer = wordsFrequencyAnalyzer;
            this.cloudImageGenerator = cloudImageGenerator;
        }

        public void Run(IAppConfig appConfig)
        {
            var text = fileReader.ReadAllText(appConfig.InputTextFilePath);

            var words = textParser.GetWords(text);

            var convertedWords = convertersExecutor.Convert(words);

            var filteredWords = filtersExecutor.Filter(convertedWords);

            var frequencies = wordsFrequencyAnalyzer.GetFrequencies(filteredWords);

            var bitmap = cloudImageGenerator.GenerateBitmap(frequencies);

            ImageSaver.SaveBitmap(bitmap, appConfig.OutputImageFilePath);
        }
    }
}
