using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Drawing
{
    public interface IPalette
    {
        Color BackgroundColor { get; }
        Color GetNextColor();
        IPalette WithWordColors(List<Color> colors);
        IPalette WithBackGroundColor(Color color);
    }
}