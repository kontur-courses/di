using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.TextRenderers
{
    public interface ITextRenderer
    {
        void PrintWords(Bitmap image, Dictionary<string, (RectangleF rectangle, Font font)> info, ImageSettings imageSettings);
    }
}
