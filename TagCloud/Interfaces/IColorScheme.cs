using System.Drawing;
using TagCloud.IntermediateClasses;

namespace TagCloud.Interfaces
{
    public interface IColorScheme
    {
        Color Process(PositionedElement element);
    }
}