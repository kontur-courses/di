using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.ImageSavior;
using TagsCloudVisualization.TagsCloudDrawer;
using TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider;
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
            var drawerSettings = new DrawerSettingsProvider
            {
                Font = new FontSettings
                {
                    Family = "Arial",
                    MaxSize = 50
                },
                ColorGenerator = new RandomColorGenerator(new Random())
            };
            var drawer = new Drawer();
            var savior = new PngSavior();
            var layouter = new CircularLayouter(Point.Empty);
            var transformer = new LayoutWordsTransformer();
            var creator = new ImageCreator.ImageCreator(drawer, savior, imageSettings);

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var words = provider.GetWords();
            var processedWords = preprocessor.Process(words);
            var tags = transformer.Transform(processedWords);
            var drawables = tags.OrderByDescending(tag => tag.Weight).Select(tag =>
            {
                var height = tag.Weight * drawerSettings.Font.MaxSize;
                var size = Size.Round(new SizeF(height * tag.Word.Length, height));
                return new TagDrawable(tag, layouter.PutNextRectangle(size), drawerSettings);
            });
            creator.Create(Path.Combine(directory, GenerateFileName()), drawables);
        }

        private static string GenerateFileName() => DateTime.Now.Ticks.ToString();
    }
}
#pragma warning restore CA1416