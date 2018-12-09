using System.Drawing;

namespace TagsCloudVisualization.Visualizer
{
    public interface IVisualizer
    {
        ISaver Draw();
    }
}