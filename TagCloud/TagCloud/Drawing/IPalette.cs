using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Drawing
{
    public interface IPalette
    {
        Color GetNextColor();
        IPalette WithColors(List<Color> colors);
    }
}