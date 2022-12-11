using System;
using TagCloud.AppConfig;
using TagCloud.CloudLayouter;
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
        private readonly ICloudLayouter cloudLayouter;
        private readonly IImageSettings imageSettings;
        private readonly CloudImageGenerator cloudImageGenerator; // переделать на интерфейс ICloudImageGenerator

        public ConsoleApp(IFileReader fileReader, 
                          ITextParser textParser, 
                          IConvertersExecutor convertersExecutor, 
                          IFiltersExecutor filtersExecutor, 
                          IWordsFrequencyAnalyzer wordsFrequencyAnalyzer, 
                          ICloudLayouter cloudLayouter, 
                          IImageSettings imageSettings, 
                          CloudImageGenerator cloudImageGenerator)
        {
            this.fileReader = fileReader;
            this.textParser = textParser;
            this.convertersExecutor = convertersExecutor;
            this.filtersExecutor = filtersExecutor;
            this.wordsFrequencyAnalyzer = wordsFrequencyAnalyzer;
            this.cloudLayouter = cloudLayouter;
            this.imageSettings = imageSettings;
            this.cloudImageGenerator = cloudImageGenerator;
        }

        public void Run(IAppConfig appConfig)
        {
            var text = new TxtFileReader().ReadAllText(appConfig.inputTextFilePath);

            var words = new TextParser().GetWords(text);

            var convertedWords = convertersExecutor.Convert(words);

            var filteredWords = filtersExecutor.Filter(convertedWords);

            var frequencies = wordsFrequencyAnalyzer.GetFrequencies(filteredWords);

            var bitmap = cloudImageGenerator.GenerateBitmap(frequencies);

            ImageSaver.SaveBitmapInSolutionSubDirectory(bitmap, "TagCloudImages", "GradientWordCloud.png");
        }
    }
}
