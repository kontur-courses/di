using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class SettingsManager
    {
        public AppSettings Load()
        {
            return new AppSettings
            {
                PointGenerator = new PointGeneratorSettings(),
                WordsExtractor = new WordsExtractorSettings()
            };
        }

    }
}
