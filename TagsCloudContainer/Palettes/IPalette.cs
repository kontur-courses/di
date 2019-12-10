using System.Drawing;
using TagsCloudContainer.PaintersWords;

namespace TagsCloudContainer.Palettes
{
    public interface IPalette
    {
        Font Font { get; }
        IPainterWords PainterWords { get; }
    }
}
