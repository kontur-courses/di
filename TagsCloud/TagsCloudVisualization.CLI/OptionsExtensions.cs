using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudDrawer.ImageSavior;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator;

namespace TagsCloudVisualization.CLI
{
    public static class OptionsExtensions
    {
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
                ImageSavior = GetSaviorFromName(options.Extension)
            };
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