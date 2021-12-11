using System.IO;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloud.Console
{
    public class ConsoleUI : IConsoleUI
    {
        private readonly IFileReadersResolver fileReadersResolver;
        private readonly IFilterApplyer filterApplyer;
        private readonly IWordsFrequencyAnalyzer frequencyAnalyzer;
        private readonly IBitmapSaver saver;
        private readonly IVisualizer visualizer;
        private readonly IWordsConverter wordsConverter;

        public ConsoleUI(IFileReadersResolver fileReadersResolver,
            IWordsConverter wordsConverter,
            IFilterApplyer filterApplyer,
            IWordsFrequencyAnalyzer frequencyAnalyzer,
            IVisualizer visualizer,
            IBitmapSaver saver)
        {
            this.fileReadersResolver = fileReadersResolver;
            this.wordsConverter = wordsConverter;
            this.filterApplyer = filterApplyer;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public void Run(IAppSettings settings)
        {
            var content = fileReadersResolver.Get(Path.GetExtension(settings.InputPath)).ReadWords(settings.InputPath);
            var convertedWords = wordsConverter.Convert(content);
            var filteredWords = filterApplyer.Apply(convertedWords);
            var freqDict = frequencyAnalyzer.GetWordsFrequency(filteredWords);
            using var visualization = visualizer.Visualize(freqDict);
            saver.Save(visualization, settings.OutputPath);
        }
    }
}