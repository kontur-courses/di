using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizing.ColorHandling
{
    public interface IColorHandler
    {
        Color GetColorFor(string word, Rectangle rectangle);

        Color BackgroundColor { get; }

        void SetColorsToUse(List<Color> colorsToUse);
    }
}