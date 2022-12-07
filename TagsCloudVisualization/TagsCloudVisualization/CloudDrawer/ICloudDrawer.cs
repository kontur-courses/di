using System.Drawing;

namespace TagsCloudVisualization.CloudDrawer;

public interface ICloudDrawer
{
    void Draw(Dictionary<string, Point> wordsInPoint);
}