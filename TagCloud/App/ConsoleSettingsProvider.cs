using System;
using System.Drawing;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.App
{
    public class ConsoleSettingsProvider : ISettingsProvider
    {
        private readonly Options options;
        private readonly PictureConfig pictureConfig;

        public ConsoleSettingsProvider(Options options, PictureConfig pictureConfig)
        {
            this.options = options;
            this.pictureConfig = pictureConfig;
        }

        public AppSettings GetSettings()
        {
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
            return settings;
        }
    }
}
