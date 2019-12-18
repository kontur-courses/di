using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloud.App
{
    public class ConsoleSettingsProvider : ISettingsProvider
    {
        private readonly Options options;
        private readonly PictureConfig pictureConfig;
        private AppSettings savedSettings;

        public ConsoleSettingsProvider(Options options, PictureConfig pictureConfig)
        {
            this.options = options;
            this.pictureConfig = pictureConfig;
        }

        public AppSettings GetSettings()
        {
            if (savedSettings != null)
                return savedSettings;
            var settings = new AppSettings
            {
                InputFilePath = options.InputFilePath,
                OutputFilePath = options.OutputFilePath,
                PictureConfig = pictureConfig
            };
            settings.PictureConfig.Size = new Size(
                Math.Max(PictureConfig.MinSize.Width, options.Width),
                Math.Max(PictureConfig.MinSize.Height, options.Height));
            if (options.FontFamily != null)
                settings.PictureConfig.FontFamily = new FontFamily(options.FontFamily);
            if (options.Background != null)
                settings.PictureConfig.Palette.BackgroundColor = Color.FromName(options.Background);
            var colors = options.WordsColors.ToList();
            if (colors.Count != 0)
                settings.PictureConfig.Palette.WordsColors = options.WordsColors.Select(Color.FromName).ToArray();
            var wordClasses = options.WordClasses.ToList();
            settings.WordClassSettings = wordClasses.Count == 0
                ? new WordClassSettings()
                : new WordClassSettings(
                    ParseWordClasses(wordClasses).ToHashSet(), false);
            settings.WordPainterAlgorithmName = options.WordPainter ?? "index";
            savedSettings = settings;
            return settings;
        }

        private static IEnumerable<WordClass> ParseWordClasses(IEnumerable<string> wordClasses)
        {
            foreach (var wordClass in wordClasses)
            {
                if (Enum.TryParse(wordClass, true, out WordClass result))
                    yield return result;
            }
        }
    }
}
