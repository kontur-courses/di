using System;
using System.Drawing;
using System.IO;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.ImageSavior;
using TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider;
using TagsCloudVisualization.WordCounter;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedClouds");
            var provider = new WordsFromFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "words.txt"));
            var preprocessor = new CombinedPreprocessor(new ToLowerCasePreprocessor(),
                                                        new RemoveBoredPreprocessor(Array.Empty<string>()));
            var imageSettings = new ImageSettingsProvider
            {
                BackgroundColor = Color.Gray,
                ImageSize = new Size(1000, 1000)
            };
            var drawerSettings = new TagsCloudDrawerSettingsProvider
            {
                Font = new Font(FontFamily.GenericMonospace, 14f),
                ColorGenerator = new RandomColorGenerator(new Random())
            };
            var drawer = new TagsCloudDrawer.TagsCloudDrawer(drawerSettings);
            var savior = new PngSavior();
            var creator = new TagsCloudImageCreator(drawer, savior, imageSettings);
            var layouter = new CircularLayouter(Point.Empty);
            var transformer = new LayoutWordsTransformer(layouter);
            var wordCounter = new DictionaryWordCounter();

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var words = provider.GetWords();
            var processedWords = preprocessor.Process(words);
            var countedWords = wordCounter.Count(processedWords);
            var tags = transformer.Transform(countedWords);
            creator.Create(Path.Combine(directory, GenerateFileName()), tags);
        }

        private static string GenerateFileName() => DateTime.Now.Ticks.ToString();
    }
}
#pragma warning restore CA1416