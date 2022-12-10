using System.Drawing;

namespace TagsCloudVisualization.CloudDrawer;

public interface ICloudDrawer
{
    void Draw(List<TextLabel> wordsInPoint);
}