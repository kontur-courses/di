using TagsCloudContainer.Options;

namespace TagsCloudContainer.Drawing;

public interface ICloudDrawer
{
    void DrawCloud(IEnumerable<Tag> tags, VisualizationOptions visualizationOptions);
}