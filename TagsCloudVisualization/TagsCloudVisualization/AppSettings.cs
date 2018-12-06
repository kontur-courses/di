using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class AppSettings : IPointGeneratorSettingsProvider, IWordsExtractorSettingsProvider
    {
        public WordsExtractorSettings WordsExtractor { get; set; }
        public PointGeneratorSettings PointGenerator { get; set; }
    }
}
