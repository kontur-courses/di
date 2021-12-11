using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public static class CloudSettingsProvider
    {
        public static CloudSettings GetSettings()
        {
            return new CloudSettings
            {
                Painter = painter,
                Spiral = spiral
            };
        }

        private static Settings settings = SettingsProvider.GetSettings();

        private static ITagPainter painter = new PrimaryTagPainter(settings);

        private static ISpiral spiral = new ArchimedeanSpiral(settings);
    }
}
