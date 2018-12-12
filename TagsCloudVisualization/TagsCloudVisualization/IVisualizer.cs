using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IVisualizer
    {
        Image Render(IEnumerable<GraphicWord> words, int width, int height, IWordPalette palette);
    }
}
