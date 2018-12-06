using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IVisualizer<in T>
    {
        Bitmap Draw(T elements);
    }
}