using System.Drawing.Imaging;
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
                Font = font,
                ImageSize = imageSize,
                Preprocessors = preprocessors,
                Filters = filters,
                Format = format
            };
        }

        private static Palette palette = new Palette
        {
            Background = Color.Black,
            Primary = Color.Orange,
        };

        private static Font font = new Font(FontFamily.GenericMonospace, 20);

        private static Size imageSize = new Size(1000, 1000);

        private static IPreprocessor[] preprocessors = new IPreprocessor[]
        {
            new TrimPreprocessor(),
            new MarkPreprocessor(),
            new LowercasePreprocessor()
        };

        private static IFilter[] filters = new IFilter[]
        {
            new ExcludeFilter()
        };

        private static ImageFormat format = ImageFormat.Png;
    }
}
