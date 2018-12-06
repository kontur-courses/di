using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IColorScheme
    {
        Color Process(PositionedElement element);
    }
}