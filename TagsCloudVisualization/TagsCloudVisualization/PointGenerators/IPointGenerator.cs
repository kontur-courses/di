using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.PointGenerators
{
    public interface IPointGenerator
    {
        Point GetNextPoint(IPointGeneratorSettingsProvider settings);
    }
}