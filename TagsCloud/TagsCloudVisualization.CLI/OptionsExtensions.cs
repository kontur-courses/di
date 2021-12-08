using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization.CLI
{
    public static class OptionsExtensions
    {
        private static string DictionariesDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Dictionaries");

        internal static TagsCloudDrawerModuleSettings ToDrawerSettings(this Options options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            return new TagsCloudDrawerModuleSettings
            {
                WordsFile = options.WordsFile,
                BoredWords = GetExcludedWordsFromFile(options.ExcludingWordsFile),
                ImageSettingsProvider = new ImageSettingsProvider
                {
                    BackgroundColor = ParseBackgroundColor(options.BackgroundColor),
                    ImageSize = new Size(options.Width, options.Height)
                },
                TagDrawableSettingsProvider = new TagDrawableSettingsProvider
                {
                    ColorGenerator = GetTagsColorGeneratorFromName(options.TagsColor),
                    Font = new FontSettings
                    {
                        Family = "Arial",
                        MaxSize = options.MaxFontSize
                    }
                },
                Layouter = GetLayouterFromName(options.Algorithm),
                ImageSavior = GetSaviorFromName(options.Extension),
                WordsPreprocessors = GetWordPreprocessors(options.Languages.DefaultIfEmpty("en"))
            };
        }

        private static IEnumerable<IWordsPreprocessor> GetWordPreprocessors(IEnumerable<string> optionsLanguages) =>
            optionsLanguages.Distinct().Select(language =>
            {
                try
                {
                    return CreateInfinitiveFormProcessorFromDictionary(language);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Language '{language}' not supported.");
                }
            });

        private static ToInfinitiveFormProcessor CreateInfinitiveFormProcessorFromDictionary(string language)
        {
            using var dictionaryStream = File.OpenRead(Path.Combine(DictionariesDirectory, language, "index.dic"));
            using var affixStream = File.OpenRead(Path.Combine(DictionariesDirectory, language, "index.aff"));
            return new ToInfinitiveFormProcessor(dictionaryStream, affixStream);
        }

        private static IEnumerable<string> GetExcludedWordsFromFile(string filename) =>
            !File.Exists(filename) ? Array.Empty<string>() : File.ReadLines(filename);

        private static Func<IImageSavior> GetSaviorFromName(string extension)
        {
            return extension switch
            {
                "png" => () => new PngSavior(),
                _     => throw new ArgumentException($"Cannot save file with {extension} extension")
            };
        }

        private static ILayouter GetLayouterFromName(string algorithm)
        {
            return algorithm switch
            {
                "circular" => new CircularLayouter(Point.Empty),
                _          => throw new ArgumentException($"Layouter {algorithm} not defined")
            };
        }

        private static Color ParseBackgroundColor(string color) => Color.FromName(color);

        private static ITagColorGenerator GetTagsColorGeneratorFromName(string name)
        {
            if (name == "random")
                return new RandomTagColorGenerator(new RandomColorGenerator(new Random()));
            if (Enum.TryParse(name, true, out KnownColor color))
                return new StrengthAlphaTagColorGenerator(Color.FromKnownColor(color));
            throw new ArgumentException($"Color {name} not defined");
        }
    }
}