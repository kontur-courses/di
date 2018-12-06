using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public interface IPointGeneratorSettingsProvider
    {
        PointGeneratorSettings PointGenerator { get; set; }
    }
}
