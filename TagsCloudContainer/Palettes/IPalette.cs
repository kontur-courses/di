using System.Drawing;
using TagsCloudContainer.PaintersWords;

namespace TagsCloudContainer.Palettes
{
    interface IPalette
    {
        Font Font { get; }
        IPainterWords PainterWords { get; }
    }
}
