using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.CloudLayouter.VectorsGenerator;
using TagsCloudVisualization.Drawable.Tags.Settings;
using TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization.CLI
{
    public static class OptionsExtensions
    {
        private static string DictionariesDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Dictionaries");

        internal static TagsCloudVisualisationSettings ToDrawerSettings(this Options options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            return new TagsCloudVisualisationSettings
            {
                WordsFile = options.WordsFile,
                BoringWords = GetExcludedWordsFromFile(options.ExcludingWordsFile),
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
                        Family = options.FontFamily,
                        MaxSize = options.MaxFontSize
                    }
                },
                Layouter = GetLayouter(options),
                ImageSaveService = GetSaveServiceFromName(options.Extension),
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

        private static IImageSaveService GetSaveServiceFromName(string extension)
        {
            return extension switch
            {
                "png"  => new PngSaveService(),
                "jpeg" => new JpegSaveService(),
                "bmp"  => new BmpSaveService(),
                _      => throw new ArgumentException($"Cannot save file with {extension} extension")
            };
        }

        private static ILayouter GetLayouter(Options options)
        {
            return options.Algorithm switch
            {
                "circular" => new NonIntersectedLayouter(Point.Empty, new CircularVectorsGenerator(0.005, 360)),
                "random" => new NonIntersectedLayouter(Point.Empty,
                    new RandomVectorsGenerator(new Random(),
                        Size.Round(new SizeF(options.Width * 0.5f, options.Height * 0.5f)))),
                _ => throw new ArgumentException($"Layouter {options.Algorithm} not defined")
            };
        }

        private static Color ParseBackgroundColor(string color) => Color.FromName(color);

        private static ITagColorGenerator GetTagsColorGeneratorFromName(string name)
        {
            switch (name)
            {
                case "random":
                    return new RandomTagColorGenerator(new RandomColorGenerator(new Random()));
                case "rainbow":
                    return new RandomTagColorGenerator(new RainbowColorGenerator(new Random()));
            }

            if (Enum.TryParse(name, true, out KnownColor color))
                return new StrengthAlphaTagColorGenerator(Color.FromKnownColor(color));
            throw new ArgumentException($"Color {name} not defined");
        }
    }
}