using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface IColorScheme
    {
        Color Process(PositionedElement element);
    }
}