using System.Drawing;
using TagCloud.IntermediateClasses;

namespace TagCloud.Interfaces
{
    public interface IFontScheme
    {
        Font Process(PositionedElement element);
    }
}