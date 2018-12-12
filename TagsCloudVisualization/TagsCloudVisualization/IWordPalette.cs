using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IWordPalette
    {
        Image GetBackground(Size size);
        void ColorWords(IEnumerable<GraphicWord> words);
    }
}
