using System.Drawing;

namespace TagsCloudVisualization.Visualizer
{
    public interface IVisualizer<T>
    {
        Bitmap Draw();
    }
}