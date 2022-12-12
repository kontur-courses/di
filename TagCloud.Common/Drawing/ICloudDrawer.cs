using TagCloud.Common.Options;

namespace TagCloud.Common.Drawing;

public interface ICloudDrawer
{
    void DrawCloud(IEnumerable<Tag> tags, VisualizationOptions visualizationOptions);
}