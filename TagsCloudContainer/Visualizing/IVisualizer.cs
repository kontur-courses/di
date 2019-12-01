using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizing
{
    public interface IVisualizer
    {
        Bitmap GetLayoutBitmap(Dictionary<string, Rectangle> wordsInRectangles);
    }
}