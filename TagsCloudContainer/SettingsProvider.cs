using System.Drawing;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public static class SettingsProvider
    {
        public static Settings GetSettings()
        {
            return new Settings
            {
                Palette = palette,
                TextFont = font,
                ImageSize = imageSize,
                Preprocessors = preprocessors
            };
        }

        private static Palette palette = new Palette
        {
            BackgroundColor = Color.Black,
            WordColors = new Dictionary<WordType, Color>
            {
                { WordType.Default, Color.Orange }
            }
        };

        private static Font font = new Font(FontFamily.GenericMonospace, 20);

        private static Size imageSize = new Size(1000, 1000);

        private static IPreprocessor[] preprocessors = new IPreprocessor[]
        {
            new TrimPreprocessor(),
            new LowercasePreprocessor(),
            new WordFilterPreprocessor()
        };
    }
}
