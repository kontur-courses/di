using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.TextRenderers
{
    public interface ITextRenderer
    {
        void PrintWords(int width, int height, Dictionary<string, Rectangle> info, ImageSettings imageSettings);
        Size GetRectangleSize(ImageSettings imageSettings, KeyValuePair<string, int> wordInfo);
    }
}
