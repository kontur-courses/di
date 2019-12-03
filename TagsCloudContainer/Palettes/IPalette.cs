using System.Drawing;

namespace TagsCloudContainer.Palettes
{
    interface IPalette
    {
        Font Font { get; }
        Brush Brush { get; }
    }
}
