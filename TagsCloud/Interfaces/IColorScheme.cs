using System.Drawing;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization.Interfaces
{
    public interface IColorScheme
    {
        Color Process(PositionedElement element);
    }
}