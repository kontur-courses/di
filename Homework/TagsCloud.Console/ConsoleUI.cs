using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloud.Console
{
    public class ConsoleUI : IConsoleUI
    {
        private readonly IFileReadersResolver fileReadersResolver;
        private readonly IWordsConverter wordsConverter;
        private readonly IFilterApplyer filterApplyer;
        private readonly IWordsFrequencyAnalyzer frequencyAnalyzer;
        private readonly IVisualizer visualizer;
        private readonly IBitmapSaver saver;

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