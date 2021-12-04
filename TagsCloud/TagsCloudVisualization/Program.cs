using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudDrawer.Drawer;
using TagsCloudDrawer.ImageCreator;
using TagsCloudDrawer.ImageSavior;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.Drawable;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

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
            var drawerSettings = new TagDrawableSettingsProvider
            {
                Font = new FontSettings
                {
                    Family = "Arial",
                    MaxSize = 50
                },
                ColorGenerator = new StrengthAlphaTagColorGenerator(Color.Red)
            };
            var drawer = new Drawer();
            var savior = new PngSavior();
            var layouter = new CircularLayouter(Point.Empty);
            var transformer = new LayoutWordsTransformer();
            var creator = new ImageCreator(drawer, savior, imageSettings);

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